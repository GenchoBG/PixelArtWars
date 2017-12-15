using System.Collections.Generic;
using PixelArtWars.Data.Models.Relations;

namespace PixelArtWars.Services.Interfaces
{
    public interface IDrawingService
    {
        void Save(string userId, int gameId, string imageData);
    }
}
