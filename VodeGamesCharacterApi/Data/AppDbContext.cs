using Microsoft.EntityFrameworkCore;
using VodeGamesCharacterApi.Models;

namespace VodeGamesCharacterApi.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Character> Characters => Set<Character>();
    }
}
