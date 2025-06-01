using Microsoft.EntityFrameworkCore;
using Vinylify.Backend.Models;

namespace Vinylify.Backend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opts)
            : base(opts)
        { }

        public DbSet<Album>          Albums          { get; set; } = null!;
        public DbSet<UserCollection> UserCollections { get; set; } = null!;
        public DbSet<WishListItem>   WishListItems   { get; set; } = null!;
        public DbSet<CollectionItem> CollectionItems { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<UserCollection>()
              .HasKey(uc => new { uc.UserId, uc.AlbumId });

            mb.Entity<WishListItem>()
              .HasKey(w => new { w.UserId, w.DiscogsId });

            mb.Entity<CollectionItem>()
              .HasKey(c => new { c.UserId, c.DiscogsId });
        }
    }
}
