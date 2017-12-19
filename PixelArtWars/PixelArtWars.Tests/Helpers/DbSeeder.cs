using PixelArtWars.Data;
using PixelArtWars.Data.Models;
using PixelArtWars.Data.Models.Enums;

namespace PixelArtWars.Tests.Helpers
{
    public class DbSeeder
    {
        public static void SeedNormalUsers(PixelArtWarsDbContext db)
        {
            for (int i = 0; i < 20; i++)
            {
                db.Users.Add(new User()
                {
                    UserName = i.ToString(),
                    Email = $"bistra{i}@basheva.tues",
                    IsBanned = false
                });
            }

            db.SaveChanges();
        }

        public static void SeedBannedUsers(PixelArtWarsDbContext db)
        {
            for (int i = 0; i < 20; i++)
            {
                db.Users.Add(new User()
                {
                    UserName = i.ToString(),
                    Email = $"bistra{i}@basheva.tues",
                    IsBanned = true
                });
            }

            db.SaveChanges();
        }

        public static void SeedGames(PixelArtWarsDbContext db)
        {
            for (int i = 0; i < 20; i++)
            {
                db.Games.Add(new Game()
                {
                    Theme = $"StellaBasheva{i}",
                    Status = GameStauts.Active,
                    PlayersCount = 2
                });
            }

            db.SaveChanges();
        }
    }
}
