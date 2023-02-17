using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class DataContext : IdentityDbContext<AppUser>
{
    public DataContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Item> Items { get; set; }
    public DbSet<ItemImage> ItemImages { get; set; }
    public DbSet<UserPhoto> UserPhotos { get; set; }
    public DbSet<ItemTranfer> ItemsTrackings { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<ItemTranfer>(x => x.HasKey(aa => new { aa.AppUserId, aa.ItemId }));

        builder.Entity<ItemTranfer>()
            .HasOne(u => u.AppUser)
            .WithMany(a => a.ItemTranferHistory)
            .HasForeignKey(aa => aa.AppUserId);

        builder.Entity<ItemTranfer>()
            .HasOne(u => u.Item)
            .WithMany(a => a.ItemsTrackings)
            .HasForeignKey(aa => aa.ItemId);
    }
}
