using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
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

            if (!this.db.Users.Any(u => u.Id == reporterId))
            {
                return;
            }

            var report = new Report()
            {
                GameId = gameId,
                Date = DateTime.UtcNow,
                ReporterId = reporterId
            };
            game.Status = GameStauts.Reported;

            this.db.Reports.Add(report);

            this.db.SaveChanges();
        }

        public IQueryable<Report> All() => this.db.Reports;

        public Report Get(int id)
            => this.db
                .Reports
                .Include(r => r.Game)
                .ThenInclude(r => r.Players)
                .ThenInclude(gu => gu.User)
                .Include(r => r.Reporter)
                .FirstOrDefault(r => r.Id == id);

        public void Close(int id)
        {
            var report = this.Get(id);

            if (report != null)
            {
                report.Status = ReportStatus.Closed;
                report.Game.Status = GameStauts.Finished;
            }

            this.db.SaveChanges();
        }
    }
}
