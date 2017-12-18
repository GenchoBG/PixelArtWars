using System;
using PixelArtWars.Data;
using PixelArtWars.Services.Interfaces;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PixelArtWars.Data.Models;
using PixelArtWars.Data.Models.Enums;

namespace PixelArtWars.Services
{
    public class DrawingService : IDrawingService
    {
        private readonly PixelArtWarsDbContext db;
        private readonly UserManager<User> userManager;
        private readonly IHostingEnvironment host;
        private readonly IImageService imageService;

        public DrawingService(PixelArtWarsDbContext db, IHostingEnvironment host, IImageService imageService, UserManager<User> userManager)
        {
            this.db = db;
            this.host = host;
            this.imageService = imageService;
            this.userManager = userManager;
        }

        public async Task Save(string userId, int gameId, string imageData)
        {
            var playerGame = this.db
                .Games
                .Include(g => g.Players)
                .Where(g => g.Id == gameId)
                .SelectMany(g => g.Players)
                .FirstOrDefault(p => p.UserId == userId);

            if (playerGame != null && !playerGame.HasDrawn)
            {
                var imagePath = await this.imageService.SaveGameDrawing(userId, gameId, imageData);

                playerGame.HasDrawn = true;
                playerGame.ImageUrl = imagePath;

                try
                {
                    this.UpdateGameStatus(gameId);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

        private void UpdateGameStatus(int gameId)
        {
            var game = this.db
                .Games
                .Include(g => g.Players)
                .FirstOrDefault(g => g.Id == gameId);

            if (game.Players.Count == game.PlayersCount && game.Players.All(p => p.HasDrawn))
            {
                game.Status = GameStauts.PendingForEvaluation;
            }

            this.db.SaveChanges();
        }
    }
}
