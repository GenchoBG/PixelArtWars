using System;
using Microsoft.EntityFrameworkCore;
using PixelArtWars.Data;

namespace PixelArtWars.Tests.Helpers
{
    public class DbGenerator
    {
        public static PixelArtWarsDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<PixelArtWarsDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var db = new PixelArtWarsDbContext(options);

            return db;
        }
    }
}
