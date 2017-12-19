using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PixelArtWars.Data;
using PixelArtWars.Services;
using PixelArtWars.Services.Interfaces;
using PixelArtWars.Tests.Helpers;
using Xunit;

namespace PixelArtWars.Tests.Services
{
    public class BanServiceTests
    {
        private readonly PixelArtWarsDbContext db;
        private readonly IBanService banService;

        public BanServiceTests()
        {
            this.db = DbGenerator.GetDbContext();
            DbSeeder.SeedNormalUsers(this.db);
            this.banService = new BanService(this.db);
        }

        [Fact]
        public async Task TestBanWithValidIdBansUser()
        {
            // arrange
            var user = await this.db.Users.FirstOrDefaultAsync();
            var reporter = await this.db.Users.Except(new[] { user }).FirstOrDefaultAsync();

            // act
            this.banService.Ban(user.Id, reporter.Id);

            // assert
            Assert.True(user.IsBanned);
        }

        [Fact]
        public async Task TestBanWithValidReporterAddsKarma()
        {
            // arrange
            var user = await this.db.Users.FirstOrDefaultAsync();
            var reporter = await this.db.Users.Except(new[] { user }).FirstOrDefaultAsync();
            var initialKarma = reporter.TotalKarma;
           
            // act
            this.banService.Ban(user.Id, reporter.Id);

            // assert
            Assert.True(reporter.TotalKarma > initialKarma);
        }

        [Fact]
        public async Task TestUnbanOnBannedUser()
        {
            // arrange
            DbSeeder.SeedBannedUsers(this.db);
            var user = await this.db.Users.FirstOrDefaultAsync(u => u.IsBanned);

            // act
            this.banService.Unban(user.Id);

            // assert
            Assert.False(user.IsBanned);
        }

        [Fact]
        public async Task TestGetBannedUsersReturnsAllBannedUsers()
        {
            // arrange
            DbSeeder.SeedBannedUsers(this.db);
            var baseUsers = await this.db.Users.Where(u => u.IsBanned).ToArrayAsync();

            // act
            var users = await this.banService.GetBannedUsers().ToArrayAsync();

            // assert
            var result = baseUsers.SequenceEqual(users);
            Assert.True(result);
        }
    }
}
