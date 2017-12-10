using PixelArtWars.Data;
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
            throw new System.NotImplementedException();
        }
    }
}
