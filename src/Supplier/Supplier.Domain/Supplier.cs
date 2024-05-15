using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Supplier.Domain;

[Table("tbSupplier")]
public class Supplier
{
    [Key]
    public Guid SupplierId { get; set; }
    public string SupplierName { get; set; } = string.Empty;
    public string SupplierDescription { get; set; } = string.Empty;
    public DateTime BookingDate { get; set; } = DateTime.Now;
}
