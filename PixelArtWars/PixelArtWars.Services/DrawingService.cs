using PixelArtWars.Data;
using PixelArtWars.Services.Interfaces;
using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using PixelArtWars.Data.Models.Enums;

namespace PixelArtWars.Services
{
    public class DrawingService : IDrawingService
    {
        private readonly PixelArtWarsDbContext db;
        private readonly IHostingEnvironment host;

        public DrawingService(PixelArtWarsDbContext db, IHostingEnvironment host)
        {
            this.db = db;
            this.host = host;
        }

        public void Save(string userId, int gameId, string imageData)
        {
            var playerGame = this.db
                .Games
                .Include(g => g.Players)
                .Where(g => g.Id == gameId)
                .SelectMany(g => g.Players)
                .FirstOrDefault(p => p.UserId == userId);

            if (playerGame != null && !playerGame.HasDrawn)
            {
                var imagePath = $@"\images\drawings\{userId}_{gameId}.jpeg";
                var fileNameWithPath = this.host.WebRootPath + imagePath;

                using (var fs = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    using (var bw = new BinaryWriter(fs))
                    {
                        var data = Convert.FromBase64String(imageData);
                        bw.Write(data);
                        bw.Close();
                    }
                }

                playerGame.HasDrawn = true;
                playerGame.ImageUrl = imagePath;


                this.db.Update(playerGame);
                this.db.SaveChanges();

                this.UpdateGameStatus(gameId);
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
