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

        public void CreateReport(int gameId, string reporterId)
        {
            var game = this.gameService.Get(gameId);
            if (game == null || game.Status != GameStauts.PendingForEvaluation)
            {
                return;
            }

            var report = new Report()
            {
                GameId = gameId
            };
            game.Status = GameStauts.Reported;

            this.db.Reports.Add(report);

            this.db.SaveChanges();
        }
    }
}
