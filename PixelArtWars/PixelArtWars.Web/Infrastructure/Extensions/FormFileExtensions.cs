using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace PixelArtWars.Web.Infrastructure.Extensions
{
    public static class FormFileExtensions
    {
        public static async Task<string> GetData(this IFormFile file)
        {
            using (var sr = new StreamReader(file.OpenReadStream()))
            {
                var data = await sr.ReadToEndAsync();

                return data;
            }
        }
    }
}
