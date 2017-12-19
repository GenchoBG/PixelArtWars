using System;
using System.IO;
using System.Threading.Tasks;
using Dropbox.Api;
using Microsoft.AspNetCore.Http;
using PixelArtWars.Services.Interfaces;

namespace PixelArtWars.Services
{
    public class DropboxService : IImageService
    {
        private const string TempDirectory = "./temp/";
        private const string TempFilePath = TempDirectory + "temp.jpeg";

        private readonly DropboxClient client;

        public DropboxService(string token)
        {
            if (!Directory.Exists(TempDirectory))
            {
                Directory.CreateDirectory(TempDirectory);
            }
            if (!File.Exists(TempFilePath))
            {
                File.Create(TempFilePath).Close();
            }
            this.client = new DropboxClient(token);
        }

        public async Task<string> SaveGameDrawing(string userId, int gameId, string imageData)
        {
            var path = this.GetGameDrawingImagePath(userId, gameId);

            //if file exists delete it or else dropbox client throws an exception
            if (await this.CheckIfFileExists(path))
            {
                await this.client.Files.DeleteV2Async(path);
            }

            //generate a temp file from which we read the data
            this.SaveDrawing(imageData, TempFilePath);

            //upload the file
            await this.UploadImageAsync(path, TempFilePath);

            var link = await this.GetImageLink(path);
            return link;
        }

        public async Task<string> SaveProfilePictureAsync(string userId, IFormFile file)
        {
            var path = this.GetUserProfilePicturePath(userId);

            //if file exists delete it or else dropbox client throws an exception
            if (await this.CheckIfFileExists(path))
            {
                await this.client.Files.DeleteV2Async(path);
            }

            //generate a temp file from which we read the data
            await this.SaveProfilePictureAsync(file, TempFilePath);

            //upload the file
            await this.UploadImageAsync(path, TempFilePath);

            var link = await this.GetImageLink(path);
            return link;
        }

        public async Task<string> GetGameDrawingImageLink(string userId, int gameId)
            => await this.GetImageLink(this.GetGameDrawingImagePath(userId, gameId));

        public async Task<string> GetProfilePictureLink(string userId)
            => await this.GetImageLink(this.GetUserProfilePicturePath(userId));

        private string GetUserProfilePicturePath(string userId) => $"/profilepictures/{userId}.jpeg";

        private string GetGameDrawingImagePath(string userId, int gameId) => $"/drawings/{gameId}_{userId}.jpeg";

        private async Task<string> GetImageLink(string path)
        {
            var result = await this.client.Sharing.CreateSharedLinkWithSettingsAsync(path);

            //the url that the api spits out has a query &dl=0 at the end.
            //i replace the 0 with a 1 so that it works as a download link
            var url = result.Url;
            url = url.Remove(url.Length - 1, 1);
            url += "1";
            return url;
        }

        private async Task UploadImageAsync(string path, string localPath)
        {
            using (var fileStream = File.Open(TempFilePath, FileMode.Open))
            {
                await this.client.Files.UploadAsync(path, body: fileStream);
            }
        }

        private async Task<bool> CheckIfFileExists(string path)
        {
            try
            {
                //if file does not exist this method throws an exception.
                await this.client.Files.GetMetadataAsync(path);

                return true;
            }
            catch (Exception)
            {
                //if no exception is found file does not exist
                return false;
            }
        }

        private void SaveDrawing(string imageData, string imagePath)
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

        private async Task SaveProfilePictureAsync(IFormFile formfile, string imagePath)
        {
            using (var fs = new FileStream(imagePath, FileMode.Create))
            {
                await formfile.CopyToAsync(fs);
            }
        }
    }
}
