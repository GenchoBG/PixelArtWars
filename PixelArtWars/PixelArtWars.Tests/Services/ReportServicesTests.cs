using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moq;
using PixelArtWars.Data;
using PixelArtWars.Data.Models;
using PixelArtWars.Data.Models.Enums;
using PixelArtWars.Data.Models.Relations;
using PixelArtWars.Services;
using PixelArtWars.Services.Interfaces;
using PixelArtWars.Tests.Helpers;
using Xunit;

namespace PixelArtWars.Tests.Services
{
    public class ReportServicesTests
    {
        private readonly PixelArtWarsDbContext db;
        private readonly IReportService reportService;

        public ReportServicesTests()
        {
            this.db = DbGenerator.GetDbContext();
            DbSeeder.SeedNormalUsers(this.db);
            DbSeeder.SeedGames(this.db);
            this.reportService = new ReportService(this.db, new GameService(this.db));
        }

        [Fact]
        public void TestCreateReportNonExistingGameDoesNothing()
        {
            // act
            this.reportService.CreateReport(-1, string.Empty);

            // assert
            Assert.Equal(0, this.db.Reports.Count());
        }

        [Fact]
        public async Task TestCreateReportInvalidGameDoesNothing()
        {
            // assert
            const int gameId = 1;
            var game = new Game()
            {
                Id = gameId,
                Status = GameStauts.Finished
            };
            this.db.Games.Add(game);
            await this.db.SaveChangesAsync();

            // act
            this.reportService.CreateReport(gameId, string.Empty);

            // assert
            Assert.Equal(0, this.db.Reports.Count());
        }

        [Fact]
        public async Task TestCreateReportValidDataCreatesReportAndModifiesGame()
        {
            // arrange
            var game = await this.db.Games.FirstAsync();
            var reporter = await this.db.Users.FirstAsync();
            game.Status = GameStauts.PendingForEvaluation;
            await this.db.SaveChangesAsync();

            // act
            this.reportService.CreateReport(game.Id, reporter.Id);

            // assert
            Assert.Equal(1, this.db.Reports.Count());
            var report = await this.db.Reports.FirstAsync();
            Assert.Equal(game.Id, report.GameId);
            Assert.Equal(reporter.Id, report.ReporterId);
            Assert.Equal(GameStauts.Reported, game.Status);
        }

        [Fact]
        public async Task TestGetAllReturnsAllReports()
        {
            // arrange
            for (int i = 200; i < 220; i++)
            {
                this.db.Reports.Add(new Report() { Id = i });
            }
            await this.db.SaveChangesAsync();
            var gamesBase = await this.db.Reports.ToArrayAsync();

            // act
            var games = await this.reportService.All().ToArrayAsync();

            // assert
            Assert.True(gamesBase.SequenceEqual(games));
        }

        [Fact]
        public async Task TestGetReturnsReport()
        {
            // arrange
            var legitReport = this.GetLegitReport();
            this.db.Reports.Add(legitReport);
            await this.db.SaveChangesAsync();

            // act
            var result = this.reportService.Get(legitReport.Id);

            // assert
            Assert.Equal(legitReport, result);
        }

        [Fact]
        public async Task TestCloseReport()
        {
            // arrange
            var report = this.GetLegitReport();
            this.db.Reports.Add(report);
            await this.db.SaveChangesAsync();

            // act
            this.reportService.Close(report.Id);

            // assert
            Assert.Equal(ReportStatus.Closed, report.Status);
            Assert.Equal(GameStauts.Finished, report.Game.Status);
        }

        private Report GetLegitReport()
        {
            var legitReport = new Report()
            {
                Id = 1,
                Game = new Game()
                {
                    Id = 1872,
                    Players = new List<GameUser>() { new GameUser() { User = new User() } }
                },
                Reporter = new User()
            };

            return legitReport;
        }
    }
}
