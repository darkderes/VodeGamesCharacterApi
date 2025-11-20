using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using VodeGamesCharacterApi.Data;
using VodeGamesCharacterApi.Dtos;
using VodeGamesCharacterApi.Models;
using VodeGamesCharacterApi.Services;

namespace VodeGamesCharacterApi.Tests.Services
{
    public class VideoGameCharacterServiceTests
    {
        private AppDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new AppDbContext(options);
        }

        [Fact]
        public async Task GetAllCharactersAsync_ShouldReturnEmptyList_WhenNoCharactersExist()
        {
            // Arrange
            using var context = GetInMemoryDbContext();
            var service = new VideoGameCharacterService(context);

            // Act
            var result = await service.GetAllCharactersAsync();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }

        [Fact]
        public async Task GetAllCharactersAsync_ShouldReturnAllCharacters_WhenCharactersExist()
        {
            // Arrange
            using var context = GetInMemoryDbContext();
            context.Characters.AddRange(
                new Character { Id = 1, Name = "Mario", Game = "Super Mario Bros.", Role = "Protagonist" },
                new Character { Id = 2, Name = "Link", Game = "The Legend of Zelda", Role = "Hero" }
            );
            await context.SaveChangesAsync();

            var service = new VideoGameCharacterService(context);

            // Act
            var result = await service.GetAllCharactersAsync();

            // Assert
            result.Should().HaveCount(2);
            result.Should().Contain(c => c.Name == "Mario" && c.Game == "Super Mario Bros." && c.Role == "Protagonist");
            result.Should().Contain(c => c.Name == "Link" && c.Game == "The Legend of Zelda" && c.Role == "Hero");
        }

        [Fact]
        public async Task GetCharacterByIdAsync_ShouldReturnCharacter_WhenCharacterExists()
        {
            // Arrange
            using var context = GetInMemoryDbContext();
            context.Characters.Add(new Character { Id = 1, Name = "Mario", Game = "Super Mario Bros.", Role = "Protagonist" });
            await context.SaveChangesAsync();

            var service = new VideoGameCharacterService(context);

            // Act
            var result = await service.GetCharacterByIdAsync(1);

            // Assert
            result.Should().NotBeNull();
            result!.Name.Should().Be("Mario");
            result.Game.Should().Be("Super Mario Bros.");
            result.Role.Should().Be("Protagonist");
        }

        [Fact]
        public async Task GetCharacterByIdAsync_ShouldReturnNull_WhenCharacterDoesNotExist()
        {
            // Arrange
            using var context = GetInMemoryDbContext();
            var service = new VideoGameCharacterService(context);

            // Act
            var result = await service.GetCharacterByIdAsync(999);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task AddCharacterAsync_ShouldAddCharacterToDatabase()
        {
            // Arrange
            using var context = GetInMemoryDbContext();
            var service = new VideoGameCharacterService(context);
            var characterDto = new CharacterResponse
            {
                Name = "Sonic",
                Game = "Sonic the Hedgehog",
                Role = "Speedster"
            };

            // Act
            var result = await service.AddCharacterAsync(characterDto);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().BeGreaterThan(0);
            result.Name.Should().Be("Sonic");
            result.Game.Should().Be("Sonic the Hedgehog");
            result.Role.Should().Be("Speedster");

            var dbCharacter = await context.Characters.FindAsync(result.Id);
            dbCharacter.Should().NotBeNull();
            dbCharacter!.Name.Should().Be("Sonic");
        }

        [Fact]
        public async Task UpdateCharacterAsync_ShouldUpdateCharacter_WhenCharacterExists()
        {
            // Arrange
            using var context = GetInMemoryDbContext();
            var character = new Character { Id = 1, Name = "Mario", Game = "Super Mario Bros.", Role = "Protagonist" };
            context.Characters.Add(character);
            await context.SaveChangesAsync();

            var service = new VideoGameCharacterService(context);
            var updatedCharacter = new Character
            {
                Name = "Mario Updated",
                Game = "Super Mario World",
                Role = "Hero"
            };

            // Act
            var result = await service.UpdateCharacterAsync(1, updatedCharacter);

            // Assert
            result.Should().BeTrue();

            var dbCharacter = await context.Characters.FindAsync(1);
            dbCharacter.Should().NotBeNull();
            dbCharacter!.Name.Should().Be("Mario Updated");
            dbCharacter.Game.Should().Be("Super Mario World");
            dbCharacter.Role.Should().Be("Hero");
        }

        [Fact]
        public async Task UpdateCharacterAsync_ShouldReturnFalse_WhenCharacterDoesNotExist()
        {
            // Arrange
            using var context = GetInMemoryDbContext();
            var service = new VideoGameCharacterService(context);
            var updatedCharacter = new Character
            {
                Name = "Non-existent",
                Game = "Some Game",
                Role = "Some Role"
            };

            // Act
            var result = await service.UpdateCharacterAsync(999, updatedCharacter);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public async Task DeleteCharacterAsync_ShouldDeleteCharacter_WhenCharacterExists()
        {
            // Arrange
            using var context = GetInMemoryDbContext();
            var character = new Character { Id = 1, Name = "Mario", Game = "Super Mario Bros.", Role = "Protagonist" };
            context.Characters.Add(character);
            await context.SaveChangesAsync();

            var service = new VideoGameCharacterService(context);

            // Act
            var result = await service.DeleteCharacterAsync(1);

            // Assert
            result.Should().BeTrue();

            var dbCharacter = await context.Characters.FindAsync(1);
            dbCharacter.Should().BeNull();
        }

        [Fact]
        public async Task DeleteCharacterAsync_ShouldReturnFalse_WhenCharacterDoesNotExist()
        {
            // Arrange
            using var context = GetInMemoryDbContext();
            var service = new VideoGameCharacterService(context);

            // Act
            var result = await service.DeleteCharacterAsync(999);

            // Assert
            result.Should().BeFalse();
        }
    }
}
