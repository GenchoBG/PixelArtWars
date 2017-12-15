using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace PixelArtWars.Services.Interfaces
{
    public interface IImageService
    {
        Task Save(string imageData, string imagePath);
        Task<string> SaveProfilePictureAsync(IFormFile file, string userName);
        Task<string> SaveProfilePictureAsync(Stream stream, string userName);
        void SaveDrawing(string imageData, string imagePath);
    }
}
