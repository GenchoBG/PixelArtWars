using System.Linq;
using PixelArtWars.Data;
using PixelArtWars.Data.Models;
using PixelArtWars.Data.Models.Enums;
using PixelArtWars.Services.Interfaces;

namespace PixelArtWars.Services
{
    public class ReportService : IReportService
    {
        private readonly PixelArtWarsDbContext db;
        private readonly IGameService gameService;

        public ReportService(PixelArtWarsDbContext db, IGameService gameService)
        {
            this.db = db;
            this.gameService = gameService;
        }

        public void CreateReport(string userId, int gameId)
        {
            var gameUser = this.gameService.GetGameUser(userId, gameId);

            if (gameUser == null || !gameUser.HasDrawn)
            {
                return;
            }

            var report = new Report()
            {
                GameId = gameId,
                UserId = userId,
                ImageUrl = gameUser.ImageUrl
            };
            gameUser.Game.Status = GameStauts.Reported;

            this.db.Reports.Add(report);

            this.db.SaveChanges();
        }
    }
}
