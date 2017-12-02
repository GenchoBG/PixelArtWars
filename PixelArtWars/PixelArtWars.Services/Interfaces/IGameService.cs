using System.Linq;
using PixelArtWars.Data.Models;

namespace PixelArtWars.Services.Interfaces
{
    public interface IGameService
    {
        void Create(string theme, int playersCount, string userId);
        IQueryable<Game> GetAll(string search = null);
    }
}
