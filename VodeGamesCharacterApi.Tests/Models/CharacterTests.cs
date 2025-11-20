using FluentAssertions;
using VodeGamesCharacterApi.Models;

namespace VodeGamesCharacterApi.Tests.Models
{
    public class CharacterTests
    {
        [Fact]
        public void Character_ShouldInitializeWithDefaultValues()
        {
            // Arrange & Act
            var character = new Character();

            // Assert
            character.Id.Should().Be(0);
            character.Name.Should().Be(string.Empty);
            character.Game.Should().Be(string.Empty);
            character.Role.Should().Be(string.Empty);
        }

        [Fact]
        public void Character_ShouldSetAndGetProperties()
        {
            // Arrange
            var character = new Character
            {
                Id = 1,
                Name = "Mario",
                Game = "Super Mario Bros.",
                Role = "Protagonist"
            };

            // Act & Assert
            character.Id.Should().Be(1);
            character.Name.Should().Be("Mario");
            character.Game.Should().Be("Super Mario Bros.");
            character.Role.Should().Be("Protagonist");
        }

        [Fact]
        public void Character_ShouldAllowPropertyModification()
        {
            // Arrange
            var character = new Character
            {
                Id = 1,
                Name = "Mario",
                Game = "Super Mario Bros.",
                Role = "Protagonist"
            };

            // Act
            character.Name = "Luigi";
            character.Game = "Luigi's Mansion";
            character.Role = "Hero";

            // Assert
            character.Name.Should().Be("Luigi");
            character.Game.Should().Be("Luigi's Mansion");
            character.Role.Should().Be("Hero");
        }
    }
}
