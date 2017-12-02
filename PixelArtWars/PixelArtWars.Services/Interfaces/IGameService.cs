using System.Linq;
using PixelArtWars.Data.Models;

namespace PixelArtWars.Services.Interfaces
{
    public interface IGameService
    {
        void Create(string theme, int playersCount, string userId);
        IQueryable<Game> GetAll(string search = null);
        Game Get(int id);
        void JoinGame(string userId, int gameId);
        void LeaveGame(string userId, int gameId);
    }
}
