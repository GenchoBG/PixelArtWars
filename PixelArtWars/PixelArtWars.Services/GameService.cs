﻿using System.Linq;
using Microsoft.EntityFrameworkCore;
using PixelArtWars.Data;
using PixelArtWars.Data.Models;
using PixelArtWars.Data.Models.Enums;
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
                Status = GameStauts.Active,
                PlayersCount = playersCount
            };

            game.Players.Add(new GameUser() { UserId = userId });

            this.db.Games.Add(game);
            this.db.SaveChanges();
        }

        public IQueryable<Game> GetAll(string search = "") => this.db.Games.Where(g => g.Theme.Contains(search));

        public Game Get(int id)
            => this.db
            .Games
            .Include(g => g.Players)
            .ThenInclude(pg => pg.User)
            .FirstOrDefault(g => g.Id == id);

        public void JoinGame(string userId, int gameId)
        {
            if (this.GameUserExists(userId, gameId))
            {
                return;
            }

            var game = this.Get(gameId);

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

        public void SelectWinner(string userId, int gameId)
        {
            var game = this.Get(gameId);

            var playerGame = game?.Players.FirstOrDefault(g => g.UserId == userId);
            if (playerGame == null)
            {
                return;
            }

            game.WinnerId = playerGame.UserId;
            game.Status = GameStauts.Finished;


            foreach (var gamePlayer in game.Players)
            {
                if (gamePlayer.UserId != game.WinnerId)
                {
                    gamePlayer.User.TotalScore -= 1;
                }
                else
                {
                    playerGame.User.TotalScore += game.PlayersCount;
                }
            }


            this.db.SaveChanges();
        }

        public GameUser GetGameUser(string userId, int gameId)
            => this.Get(gameId)?.Players.FirstOrDefault(gu => gu.UserId == userId);

        private bool GameUserExists(string userId, int gameId) => this.GetGameUser(userId, gameId) != null;
    }
}
