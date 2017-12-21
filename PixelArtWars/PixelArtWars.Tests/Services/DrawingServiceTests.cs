using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moq;
using PixelArtWars.Data;
using PixelArtWars.Data.Models.Relations;
using PixelArtWars.Services;
using PixelArtWars.Services.Interfaces;
using PixelArtWars.Tests.Helpers;
using Xunit;

namespace PixelArtWars.Tests.Services
{
    public class DrawingServiceTests
    {
        private readonly PixelArtWarsDbContext db;
        private readonly IDrawingService drawingService;

        private const string ImageUrl = "pesho.com";

        public DrawingServiceTests()
        {
            this.db = DbGenerator.GetDbContext();
            DbSeeder.SeedNormalUsers(this.db);
            DbSeeder.SeedGames(this.db);

            var imageService = new Mock<IImageService>();

            imageService
                .Setup(s => s.SaveGameDrawing(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>()))
                .Returns(() => Task.FromResult(ImageUrl));

            this.drawingService = new DrawingService(this.db, imageService.Object);
        }

        [Fact]
        public async Task TestSaveWithInvalidDataDoesNothing()
        {
            // arrange
            var user = await this.db.Users.FirstOrDefaultAsync();
            var game = await this.db.Games.FirstOrDefaultAsync();
            var gu = new GameUser() { Game = game, GameId = game.Id, User = user, UserId = user.Id, HasDrawn = true };
            game.Players.Add(gu);
            await this.db.SaveChangesAsync();

            // act
            await this.drawingService.SaveAsync(user.Id, game.Id, "PEsho");

            // assert
            Assert.True(string.IsNullOrWhiteSpace(gu.ImageUrl));
        }

        [Fact]
        public async Task TestSaveWithValidDataSavesImage()
        {
            // arrange
            var user = await this.db.Users.FirstOrDefaultAsync();
            var game = await this.db.Games.FirstOrDefaultAsync();
            var gu = new GameUser() { Game = game, GameId = game.Id, User = user, UserId = user.Id, HasDrawn = false };
            game.Players.Add(gu);
            await this.db.SaveChangesAsync();

            // act
            await this.drawingService.SaveAsync(user.Id, game.Id, "PEsho");

            // assert
            Assert.Equal(ImageUrl, gu.ImageUrl);
            Assert.True(gu.HasDrawn);
        }
    }
}
