using PixelArtWars.Data;
using PixelArtWars.Services.Interfaces;
using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

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

        public async Task Save(string userId, int gameId, string imageData)
        {
            var playerGame = await this.db
                .Games
                .Include(g => g.Players)
                .Where(g => g.Id == gameId)
                .SelectMany(g => g.Players)
                .FirstOrDefaultAsync(p => p.UserId == userId);

            if (playerGame != null && !playerGame.HasDrawn)
            {
                var fileNameWithPath = $@"{this.host.WebRootPath}\images\drawings\{userId}_{gameId}.jpeg";

                using (var fs = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    using (var bw = new BinaryWriter(fs))
                    {
                        byte[] data = Convert.FromBase64String(imageData);
                        bw.Write(data);
                        bw.Close();
                    }
                }

                playerGame.HasDrawn = true;
                playerGame.ImageUrl = fileNameWithPath;

                this.db.Update(playerGame);
                this.db.SaveChanges();
            }
        }
    }
}
