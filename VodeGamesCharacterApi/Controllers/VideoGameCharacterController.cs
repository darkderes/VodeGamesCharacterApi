using Microsoft.AspNetCore.Mvc;
using VodeGamesCharacterApi.Dtos;
using VodeGamesCharacterApi.Models;
using VodeGamesCharacterApi.Services;

namespace VodeGamesCharacterApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoGameCharacterController(IVideoGameCharacterService service) : ControllerBase
    {
        [HttpGet]     
        public async Task<ActionResult<List<CharacterResponse>>> GetCharacters()
        => Ok(await service.GetAllCharactersAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<CharacterResponse>> GetCharacter(int id)
        {
            var character = await service.GetCharacterByIdAsync(id);
            return character is null ? NotFound("character not found") : Ok(character);
        }

        [HttpPost]
        public async Task<ActionResult<Character>> AddCharacter(CharacterResponse character)
        {
            var createdCharacter = await service.AddCharacterAsync(character);
            return CreatedAtAction(nameof(GetCharacter), new { id = createdCharacter }, createdCharacter);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCharacter(int id, Character character)
        {
            var isUpdated = await service.UpdateCharacterAsync(id, character);
            return isUpdated ? NoContent() : NotFound("character not found");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCharacter(int id)
        {
            var isDeleted = await service.DeleteCharacterAsync(id);
            return isDeleted ? NoContent() : NotFound("character not found");
        }


    }
}
