using Domain;
using Microsoft.EntityFrameworkCore;

namespace Application.Contracts.Persistence;

public interface IDataContext
{
	DbSet<Item> Items { get; set; }
	DbSet<ItemImage> ItemImages { get; set; }
	DbSet<UserPhoto> UserPhotos { get; set; }
	DbSet<ItemEmployeeAssignment> ItemEmployeeAssignments { get; set; }
	DbSet<ShelveType> ShelveTypes { get; set; }
	DbSet<Transferhistory> TransferHistory { get; set; }
	DbSet<Department> Departments { get; set; }
	DbSet<Supplier> Suppliers { get; set; }
	Task<int> SaveChangeAsync(CancellationToken cancellationToken);
}
