using System.Linq;
using PixelArtWars.Data;
using PixelArtWars.Data.Models;
using PixelArtWars.Services.Interfaces;

namespace PixelArtWars.Services
{
    public class BanService : IBanService
    {
        private readonly PixelArtWarsDbContext db;

        public BanService(PixelArtWarsDbContext db)
        {
            this.db = db;
        }

        public void Ban(string banId, string reporterId)
        {
            var userToBan = this.db.Users.Find(banId);

            userToBan.IsBanned = true;

            var reporter = this.db.Users.Find(reporterId);
            reporter.TotalKarma++;

            this.db.SaveChanges();
        }

        public void Unban(string id)
        {
            var user = this.db.Users.Find(id);

            user.IsBanned = false;

            this.db.SaveChanges();
        }

        public IQueryable<User> GetBannedUsers()
            => this.db.Users.Where(u => u.IsBanned);
    }
}
