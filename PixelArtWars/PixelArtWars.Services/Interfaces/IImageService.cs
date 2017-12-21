using System.Threading.Tasks;

namespace PixelArtWars.Services.Interfaces
{
    public interface IImageService
    {
        Task<string> SaveGameDrawing(string userId, int gameId, string imageData);

        Task<string> SaveProfilePictureAsync(string userId, string imageData);
        
        Task<string> GetGameDrawingImageLink(string userId, int gameId);

        Task<string> GetProfilePictureLink(string userId);
    }
}
