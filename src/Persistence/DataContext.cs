using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class DataContext : IdentityDbContext<AppUser>
{
    public DataContext(DbContextOptions options) : base(options) { }
    public DbSet<Item> Items { get; set; }
    public DbSet<ItemImage> ItemImages { get; set; }
    public DbSet<UserPhoto> UserPhotos { get; set; }
    public DbSet<ItemEmployeeAssignment> ItemEmployeeAssignments { get; set; }
    public DbSet<ShelveType> ShelveTypes { get; set; }
    public DbSet<Transferhistory> TransferHistory { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }

    #region OnModelCreating
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Relationships and foreign keys
        builder.Entity<ItemEmployeeAssignment>()
            .HasOne(iea => iea.IssuerBy)
            .WithMany()  // Adjust this if AppUser has a collection of ItemEmployeeAssignments
            .HasForeignKey(iea => iea.IssuerById)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<ItemEmployeeAssignment>()
           .HasOne(iea => iea.ReceiverBy)
           .WithMany()  // Adjust this if AppUser has a collection of ItemEmployeeAssignments
           .HasForeignKey(iea => iea.ReceiverById)
           .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<ItemEmployeeAssignment>()
           .HasOne(iea => iea.Item)
           .WithMany()  // Adjust this if Item has a collection of ItemEmployeeAssignments
           .HasForeignKey(iea => iea.ItemId)
           .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Transferhistory>()
            .HasOne(iea => iea.Item)
            .WithMany()  // Adjust this if Item has a collection of ItemEmployeeAssignments
            .HasForeignKey(iea => iea.ItemById)
            .OnDelete(DeleteBehavior.Restrict);

    }
    #endregion
}
