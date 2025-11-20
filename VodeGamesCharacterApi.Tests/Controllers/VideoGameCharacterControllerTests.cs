using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using VodeGamesCharacterApi.Controllers;
using VodeGamesCharacterApi.Dtos;
using VodeGamesCharacterApi.Models;
using VodeGamesCharacterApi.Services;

namespace VodeGamesCharacterApi.Tests.Controllers
{
    public class VideoGameCharacterControllerTests
    {
        private readonly Mock<IVideoGameCharacterService> _mockService;
        private readonly VideoGameCharacterController _controller;

        public VideoGameCharacterControllerTests()
        {
            _mockService = new Mock<IVideoGameCharacterService>();
            _controller = new VideoGameCharacterController(_mockService.Object);
        }

        [Fact]
        public async Task GetCharacters_ShouldReturnOkWithListOfCharacters()
        {
            // Arrange
            var characters = new List<CharacterResponse>
            {
                new CharacterResponse { Name = "Mario", Game = "Super Mario Bros.", Role = "Protagonist" },
                new CharacterResponse { Name = "Link", Game = "The Legend of Zelda", Role = "Hero" }
            };
            _mockService.Setup(s => s.GetAllCharactersAsync()).ReturnsAsync(characters);

            // Act
            var result = await _controller.GetCharacters();

            // Assert
            var okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            var returnedCharacters = okResult.Value.Should().BeAssignableTo<List<CharacterResponse>>().Subject;
            returnedCharacters.Should().HaveCount(2);
            returnedCharacters.Should().Contain(c => c.Name == "Mario");
        }

        [Fact]
        public async Task GetCharacters_ShouldReturnEmptyList_WhenNoCharactersExist()
        {
            // Arrange
            _mockService.Setup(s => s.GetAllCharactersAsync()).ReturnsAsync(new List<CharacterResponse>());

            // Act
            var result = await _controller.GetCharacters();

            // Assert
            var okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            var returnedCharacters = okResult.Value.Should().BeAssignableTo<List<CharacterResponse>>().Subject;
            returnedCharacters.Should().BeEmpty();
        }

        [Fact]
        public async Task GetCharacter_ShouldReturnOkWithCharacter_WhenCharacterExists()
        {
            // Arrange
            var character = new CharacterResponse { Name = "Mario", Game = "Super Mario Bros.", Role = "Protagonist" };
            _mockService.Setup(s => s.GetCharacterByIdAsync(1)).ReturnsAsync(character);

            // Act
            var result = await _controller.GetCharacter(1);

            // Assert
            var okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            var returnedCharacter = okResult.Value.Should().BeAssignableTo<CharacterResponse>().Subject;
            returnedCharacter.Name.Should().Be("Mario");
        }

        [Fact]
        public async Task GetCharacter_ShouldReturnNotFound_WhenCharacterDoesNotExist()
        {
            // Arrange
            _mockService.Setup(s => s.GetCharacterByIdAsync(999)).ReturnsAsync((CharacterResponse?)null);

            // Act
            var result = await _controller.GetCharacter(999);

            // Assert
            var notFoundResult = result.Result.Should().BeOfType<NotFoundObjectResult>().Subject;
            notFoundResult.Value.Should().Be("character not found");
        }

        [Fact]
        public async Task AddCharacter_ShouldReturnCreatedAtActionWithCharacter()
        {
            // Arrange
            var characterDto = new CharacterResponse { Name = "Sonic", Game = "Sonic the Hedgehog", Role = "Speedster" };
            var createdCharacter = new Character { Id = 1, Name = "Sonic", Game = "Sonic the Hedgehog", Role = "Speedster" };
            _mockService.Setup(s => s.AddCharacterAsync(characterDto)).ReturnsAsync(createdCharacter);

            // Act
            var result = await _controller.AddCharacter(characterDto);

            // Assert
            var createdResult = result.Result.Should().BeOfType<CreatedAtActionResult>().Subject;
            createdResult.ActionName.Should().Be(nameof(_controller.GetCharacter));
            createdResult.RouteValues.Should().ContainKey("id");
            createdResult.Value.Should().BeEquivalentTo(createdCharacter);
        }

        [Fact]
        public async Task UpdateCharacter_ShouldReturnNoContent_WhenUpdateIsSuccessful()
        {
            // Arrange
            var character = new Character { Name = "Mario Updated", Game = "Super Mario World", Role = "Hero" };
            _mockService.Setup(s => s.UpdateCharacterAsync(1, character)).ReturnsAsync(true);

            // Act
            var result = await _controller.UpdateCharacter(1, character);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task UpdateCharacter_ShouldReturnNotFound_WhenCharacterDoesNotExist()
        {
            // Arrange
            var character = new Character { Name = "Non-existent", Game = "Some Game", Role = "Some Role" };
            _mockService.Setup(s => s.UpdateCharacterAsync(999, character)).ReturnsAsync(false);

            // Act
            var result = await _controller.UpdateCharacter(999, character);

            // Assert
            var notFoundResult = result.Should().BeOfType<NotFoundObjectResult>().Subject;
            notFoundResult.Value.Should().Be("character not found");
        }

        [Fact]
        public async Task DeleteCharacter_ShouldReturnNoContent_WhenDeleteIsSuccessful()
        {
            // Arrange
            _mockService.Setup(s => s.DeleteCharacterAsync(1)).ReturnsAsync(true);

            // Act
            var result = await _controller.DeleteCharacter(1);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task DeleteCharacter_ShouldReturnNotFound_WhenCharacterDoesNotExist()
        {
            // Arrange
            _mockService.Setup(s => s.DeleteCharacterAsync(999)).ReturnsAsync(false);

            // Act
            var result = await _controller.DeleteCharacter(999);

            // Assert
            var notFoundResult = result.Should().BeOfType<NotFoundObjectResult>().Subject;
            notFoundResult.Value.Should().Be("character not found");
        }
    }
}
