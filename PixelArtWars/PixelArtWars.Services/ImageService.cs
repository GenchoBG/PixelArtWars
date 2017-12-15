using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using PixelArtWars.Services.Interfaces;

namespace PixelArtWars.Services
{
    public class ImageService : IImageService
    {
        private readonly IHostingEnvironment host;

        public ImageService(IHostingEnvironment host)
        {
            this.host = host;
        }

        public async Task<string> SaveProfilePictureAsync(IFormFile file, string userName)
        {
            var imagePath = $@"\images\profilepictures\{userName}.jpeg";

            var fileNameWithPath = this.host.WebRootPath + imagePath;

            using (var fs = new FileStream(fileNameWithPath, FileMode.Create))
            {
                await file.CopyToAsync(fs);
            }

            return imagePath;
        }

        //broken
        public async Task<string> SaveProfilePictureAsync(Stream stream, string userName)
        {
            var imagePath = $@"\images\profilepictures\{userName}.jpeg";

            using (var streamReader = new StreamReader(stream))
            {
                var imageData = await streamReader.ReadToEndAsync();

                var fileNameWithPath = this.host.WebRootPath + imagePath;
                
                await this.Save(imageData, fileNameWithPath);
            }

            return imagePath;
        }

        public void SaveDrawing(string imageData, string imagePath)
        {
            using (var fs = new FileStream(imagePath, FileMode.Create))
            {
                using (var bw = new BinaryWriter(fs))
                {
                    var data = Convert.FromBase64String(imageData);
                    bw.Write(data);
                    bw.Close();
                }
            }
        }

        public async Task Save(string imageData, string imagePath)
        {
            using (var fs = new FileStream(imagePath, FileMode.Create))
            {
                using (var sr = new StreamWriter(fs))
                {
                    await sr.WriteAsync(imageData);
                }
            }
        }
    }
}
