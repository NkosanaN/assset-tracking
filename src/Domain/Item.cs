namespace Domain;

/*
 * This model is design to hold Item 
 *  A)  FK => ShelveType
 *  B)  FK => TrackedItems
 */
public class Item
{
    public Guid Id { get; set; }
    public DateTime TimeStamp { get; set; } = DateTime.Now;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? Serialno { get; set; }
    public string? ItemTag { get; set; }
    public float Cost { get; set; }
    public float Qty { get; set; }
    public DateTime DatePurchased { get; set; }
    public ShelveType Shelve { get; set; }
    public ICollection<ItemTranfer> ItemsTrackings { get; set; } = new List<ItemTranfer>();

}
