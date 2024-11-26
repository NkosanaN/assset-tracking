using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;

[Table("tbSupplier")]
public class Supplier
{
	[Key]
	public Guid SupplierId { get; set; }
	public string SupplierName { get; set; } = string.Empty;
	public string SupplierDescription { get; set; } = string.Empty;
	public DateTime BookingDate { get; set; } = DateTime.Now;
	public DateTime CreatedAt { get; set; }

}

