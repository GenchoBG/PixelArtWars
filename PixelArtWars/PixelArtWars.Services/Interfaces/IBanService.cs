using System.Linq;
using PixelArtWars.Data.Models;

namespace PixelArtWars.Services.Interfaces
{
    public interface IBanService
    {
        void Ban(string banId, string reporterId);
        void Unban(string id);
        IQueryable<User> GetBannedUsers();
    }
}
