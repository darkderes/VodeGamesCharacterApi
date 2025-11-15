using Microsoft.EntityFrameworkCore;
using VodeGamesCharacterApi.Data;
using VodeGamesCharacterApi.Dtos;
using VodeGamesCharacterApi.Models;

namespace VodeGamesCharacterApi.Services
{
    public class VideoGameCharacterService(AppDbContext context) : IVideoGameCharacterService
    {
     

        //static List<Character> characters = new List<Character> {

        //    new Character{ Id = 1, Name = "Mario", Game = "Super Mario Bros.", Role = "Protagonist" },
        //    new Character{ Id = 2, Name = "Link", Game = "The Legend of Zelda", Role = "Hero" },
        //    new Character{ Id = 3, Name = "Master Chief", Game = "Halo", Role = "Villain" },
        //    new Character{ Id = 4, Name = "Lara Croft", Game = "Tomb Raider", Role = "Adventurer" },
        //};
        public async Task<Character> AddCharacterAsync(CharacterResponse character)
        {
           var entity = new Character { 
           
               Name = character.Name,
                Game = character.Game,
                Role = character.Role

           };
              context.Characters.Add(entity);
            await context.SaveChangesAsync();
              return new Character
              {
                  Id = entity.Id,
                  Name = entity.Name,
                  Game = entity.Game,
                  Role = entity.Role
              };
        }

        public Task<bool> DeleteCharacterAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CharacterResponse>> GetAllCharactersAsync()
         => await context.Characters.Select(c => new CharacterResponse
         {
             Name = c.Name,
             Game = c.Game,
             Role = c.Role
         }).ToListAsync();

        public async Task<CharacterResponse?> GetCharacterByIdAsync(int id)
        {
            var character = await context.Characters.Where(c => c.Id == id).Select(c => new CharacterResponse
            {
                Name = c.Name,
                Game = c.Game,
                Role = c.Role
            }).FirstOrDefaultAsync();

            return character;
        }

        public async Task<bool> UpdateCharacterAsync(int id, Character character)
        {
            return true;

        }
    }
}
