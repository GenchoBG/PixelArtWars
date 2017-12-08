using System.Linq;
using PixelArtWars.Data.Models;
using PixelArtWars.Data.Models.Relations;

namespace PixelArtWars.Services.Interfaces
{
    public interface IGameService
    {
        void Create(string theme, int playersCount, string userId);
        IQueryable<Game> GetAll(string search = "");
        Game Get(int id);
        void JoinGame(string userId, int gameId);
        void LeaveGame(string userId, int gameId);
        void SelectWinner(string userId, int gameId);
        GameUser GetGameUser(string userId, int gameId);
    }
}
