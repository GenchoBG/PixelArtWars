using System.Linq;
using PixelArtWars.Data;
using PixelArtWars.Data.Models;
using PixelArtWars.Data.Models.Relations;
using PixelArtWars.Services.Interfaces;

namespace PixelArtWars.Services
{
    public class GameService : IGameService
    {
        private readonly PixelArtWarsDbContext db;

        public GameService(PixelArtWarsDbContext db)
        {
            this.db = db;
        }

        public void Create(string theme, int playersCount, string userId)
        {
            var game = new Game()
            {
                Theme = theme,
                PlayersCount = playersCount
            };

            game.Players.Add(new GameUser() { UserId = userId });

            this.db.Games.Add(game);
            this.db.SaveChanges();
        }

        public IQueryable<Game> GetAll(string search) => this.db.Games.Where(g => g.Theme.Contains(search));
    }
}
