using System.Threading.Tasks;

namespace PixelArtWars.Services.Interfaces
{
    public interface IDrawingService
    {
        Task Save(string userId, int gameId, string imageData);
    }
}
