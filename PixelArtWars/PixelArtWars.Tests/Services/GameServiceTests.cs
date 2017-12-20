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

        [Fact]
        public async Task TestLeaveGameWithValidDataLeavesGame()
        {
            // arrange
            DbSeeder.SeedGames(this.db);
            var game = await this.db.Games.FirstOrDefaultAsync();
            var user = await this.db.Users.FirstOrDefaultAsync();
            game.Players.Add(new GameUser() { Game = game, GameId = game.Id, User = user, UserId = user.Id });
            await this.db.SaveChangesAsync();

            // act
            this.gameService.LeaveGame(user.Id, game.Id);

            // assert
            var count = game.Players.Count;
            Assert.Equal(0, count);
        }

        [Fact]
        public async Task TestSelectWinnerSelectsWinnerWithValidDataClosesGameGivesScoreDeductsScore()
        {
            // arrange
            DbSeeder.SeedGames(this.db);
            var game = new Game() { PlayersCount = 2 };
            var users = await this.db.Users.Take(2).ToArrayAsync();
            var winner = users[0];
            var initialWinnerScore = winner.TotalScore;
            var loser = users[1];
            var initialLoserScore = loser.TotalScore;
            var first = new GameUser() { Game = game, GameId = game.Id, User = winner, UserId = winner.Id, HasDrawn = true };
            var second = new GameUser() { Game = game, GameId = game.Id, User = loser, UserId = loser.Id, HasDrawn = true };
            game.Players.Add(first);
            game.Players.Add(second);
            this.db.Games.Add(game);
            await this.db.SaveChangesAsync();

            // act
            this.gameService.SelectWinner(winner.Id, game.Id);

            // assert
            Assert.Equal(winner.Id, game.WinnerId);
            Assert.Equal(GameStauts.Finished, game.Status);
            Assert.Equal(game.PlayersCount + initialWinnerScore, winner.TotalScore);
            Assert.Equal(initialLoserScore - 1, loser.TotalScore);
        }

        [Fact]
        public async Task TestGetGameUserWithCorrectDataReturnsCorrectData()
        {
            // arrange
            DbSeeder.SeedGames(this.db);
            var game = await this.db.Games.FirstOrDefaultAsync();
            var user = await this.db.Users.FirstOrDefaultAsync();
            var gu = new GameUser() { Game = game, GameId = game.Id, User = user, UserId = user.Id };
            game.Players.Add(gu);
            await this.db.SaveChangesAsync();

            // act
            var result = this.gameService.GetGameUser(user.Id, game.Id);

            // assert
            Assert.Equal(gu, result);
        }

        [Fact]
        public async Task TestGetGameUserWithIncorrectDataReturnsNull()
        {
            // arrange
            DbSeeder.SeedGames(this.db);
            var game = await this.db.Games.FirstOrDefaultAsync();
            var user = await this.db.Users.FirstOrDefaultAsync();

            // act
            var result = this.gameService.GetGameUser(user.Id, game.Id);

            // assert
            Assert.Null(result);
        }
    }
}
