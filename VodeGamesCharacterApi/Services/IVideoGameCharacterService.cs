using VodeGamesCharacterApi.Dtos;
using VodeGamesCharacterApi.Models;

namespace VodeGamesCharacterApi.Services
{
    public interface IVideoGameCharacterService
    {
        Task<List<CharacterResponse>> GetAllCharactersAsync();
        Task<CharacterResponse?> GetCharacterByIdAsync(int id);
        Task<Character> AddCharacterAsync(CharacterResponse character);
        Task<bool> UpdateCharacterAsync(int id,Character character);
        Task<bool> DeleteCharacterAsync(int id);
    }
}
