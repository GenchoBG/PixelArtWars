using System.Threading.Tasks;

namespace PixelArtWars.Services.Interfaces
{
    public interface IDrawingService
    {
        void Save(string userId, int gameId, string imageData);
    }
}
