using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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
    public class GameServiceTests
    {
        private readonly PixelArtWarsDbContext db;
        private readonly IGameService gameService;

        public GameServiceTests()
        {
            this.db = DbGenerator.GetDbContext();
            DbSeeder.SeedNormalUsers(this.db);
            this.gameService = new GameService(this.db);
        }

        [Fact]
        public async Task TestCreateAddsNewActiveGame()
        {
            // arrange
            const string theme = "Ihateunittests";
            const int playerCount = -1;
            var user = await this.db.Users.FirstOrDefaultAsync();

            // act
            this.gameService.Create(theme, playerCount, user.Id);

            // assert
            var gamesCount = await this.db.Games.CountAsync();
            Assert.True(gamesCount > 0);
            var game = await this.db.Games.FirstOrDefaultAsync();
            Assert.Equal(theme, game.Theme);
            Assert.Equal(playerCount, game.PlayersCount);
            Assert.Equal(GameStauts.Active, game.Status);
        }

        [Fact]
        public async Task TestSearchEmptyStringReturnsAllGames()
        {
            // arrange
            DbSeeder.SeedGames(this.db);
            var initialGames = await this.db.Games.ToArrayAsync();

            // act
            var games = await this.gameService.GetAll().ToArrayAsync();

            // assert
            var result = games.SequenceEqual(initialGames);
            Assert.True(result);
        }

        [Fact]
        public async Task TestSearchWithStringReturnsCorrectGames()
        {
            // arrange
            const string searchKeyword = "Stella";
            DbSeeder.SeedGames(this.db);
            var initialGames = await this.db.Games.Where(g => g.Theme.Contains(searchKeyword)).ToArrayAsync();

            // act
            var games = await this.gameService.GetAll(searchKeyword).ToArrayAsync();

            // assert
            var result = games.SequenceEqual(initialGames);
            Assert.True(result);
        }

        [Fact]
        public async Task TestJoinGameWithExistingUserExistingGameNonexistingUserGameCreatesUserGame()
        {
            // arrange
            DbSeeder.SeedGames(this.db);
            var user = await this.db.Users.FirstOrDefaultAsync();
            var game = await this.db.Games.FirstOrDefaultAsync();

            // act
            this.gameService.JoinGame(user.Id, game.Id);

            // arrange
            var userGame = game.Players.First();
            Assert.Equal(user.Id, userGame.UserId);
        }

        [Fact]
        public async Task TestJoinGameWithInvalidDataCreatesNothing()
        {
            // arrange
            DbSeeder.SeedGames(this.db);
            var game = await this.db.Games.FirstOrDefaultAsync();
            var user = await this.db.Users.FirstOrDefaultAsync();
            game.Players.Add(new GameUser() { Game = game, GameId = game.Id, User = user, UserId = user.Id });
            await this.db.SaveChangesAsync();

            // act
            this.gameService.JoinGame(user.Id, game.Id);

            // arrange
            Assert.Equal(1, game.Players.Count);
        }
    }
}
