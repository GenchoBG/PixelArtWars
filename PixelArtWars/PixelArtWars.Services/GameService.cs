using System.Linq;
using Microsoft.EntityFrameworkCore;
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
                IsActive = true,
                PlayersCount = playersCount
            };

            game.Players.Add(new GameUser() { UserId = userId });

            this.db.Games.Add(game);
            this.db.SaveChanges();
        }

        public IQueryable<Game> GetAll(string search) => this.db.Games.Where(g => g.Theme.Contains(search));

        public Game Get(int id)
            => this.db
            .Games
            .Include(g => g.Players)
            .ThenInclude(pg => pg.User)
            .FirstOrDefault(g => g.Id == id);

        public void JoinGame(string userId, int gameId)
        {
            var game = this.db
                .Games
                .Include(g => g.Players)
                .ThenInclude(pg => pg.User)
                .FirstOrDefault(g => g.Id == gameId);

            if (game != null)
            {
                game.Players.Add(new GameUser() { UserId = userId });

                this.db.SaveChanges();
            }
        }

        public void LeaveGame(string userId, int gameId)
        {
            var game = this.db
                .Games
                .Include(g => g.Players)
                .ThenInclude(pg => pg.User)
                .FirstOrDefault(g => g.Id == gameId);

            if (game != null)
            {
                var playerGame = game.Players.First(pg => pg.UserId == userId);

                game.Players.Remove(playerGame);

                this.db.SaveChanges();
            }
        }
    }
}
