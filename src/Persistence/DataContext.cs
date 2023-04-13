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
    public DbSet<ItemEmployeeAssignment> ItemEmployeeAssignments { get; set; }
    public DbSet<ShelveType> ShelveTypes { get; set; }
    public DbSet<Transferhistory> TransferHistory { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }

    #region OnModelCreating
    //protected override void OnModelCreating(ModelBuilder builder)
    //{
    //    base.OnModelCreating(builder);

    //    //builder.Entity<Item>(x => x.HasIndex(aa => new { aa.Serialno}).IsUnique());


    //   // builder.Entity<ItemEmployeeAssignment>(x => x.HasKey(aa => new { aa.Id, aa.ItemId }));

    //    //builder.Entity<ItemTranfer>()
    //    //    .HasOne(u => u.AppUser)
    //    //    .WithMany(a => a.ItemTranferHistory)
    //    //    .HasForeignKey(aa => aa.AppUserId);

    //    //builder.Entity<ItemTranfer>()
    //    //    .HasOne(u => u.Item)
    //    //    .WithMany(a => a.ItemsTrackings)
    //    //    .HasForeignKey(aa => aa.ItemId);
    //}
    #endregion
}
