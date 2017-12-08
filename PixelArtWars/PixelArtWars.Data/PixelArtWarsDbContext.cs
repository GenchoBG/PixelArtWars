using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PixelArtWars.Data.Models;
using PixelArtWars.Data.Models.Relations;

namespace PixelArtWars.Data
{
    public class PixelArtWarsDbContext : IdentityDbContext<User>
    {
        public DbSet<Game> Games { get; set; }

        public DbSet<Report> Reports { get; set; }

        public PixelArtWarsDbContext(DbContextOptions<PixelArtWarsDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.Entity<GameUser>()
                .HasKey(gu => new { gu.UserId, gu.GameId });

            builder
                .Entity<Game>()
                .HasMany(g => g.Players)
                .WithOne(pg => pg.Game)
                .HasForeignKey(pg => pg.GameId);

            builder
                .Entity<User>()
                .HasMany(u => u.Games)
                .WithOne(gu => gu.User)
                .HasForeignKey(gu => gu.UserId);

            builder
                .Entity<Report>()
                .HasOne(r => r.Reporter)
                .WithMany()
                .HasForeignKey(r => r.ReporterId);
        }
    }
}
