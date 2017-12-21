using System.Threading.Tasks;

namespace PixelArtWars.Services.Interfaces
{
    public interface IDrawingService
    {
        Task SaveAsync(string userId, int gameId, string imageData);
    }
}
