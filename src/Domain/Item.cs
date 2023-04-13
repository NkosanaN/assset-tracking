using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;

/*
 * This model is design to hold Item 
 *  A)  FK => ShelveType
 */
[Table("tblitem")]
public class Item
{
    [Key]
    public Guid ItemId { get; set; }
    public string Serialno { get; set; } = string.Empty;
    public DateTime TimeStamp { get; set; } = DateTime.Now;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? ItemTag { get; set; }
    public float Cost { get; set; } = 0.00f;//should be uniqueCost
    public float Qty { get; set; } = 0.00f;
    public DateTime DatePurchased { get; set; }
    public bool DueforRepair { get; set; } 

    [ForeignKey("ShelveBy")]
    public Guid ShelfId { get; set; }
    public ShelveType? ShelveBy { get; set; }

    [ForeignKey("CreatedBy")]
    public string CreatedById { get; set; }
    public AppUser? CreatedBy { get; set; }

    //public ItemImage ItemImage { get; set; }
}
