using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace PixelArtWars.Web.Infrastructure.Extensions
{
    public static class FormFileExtensions
    {
        public static async Task<string> GetData(this IFormFile formFile)
        {
            using (var memoryStream = new MemoryStream())
            {
                await formFile.CopyToAsync(memoryStream);
                var bytes = memoryStream.ToArray();
                var data = Convert.ToBase64String(bytes);
                return data;
            }
        }
    }
}
