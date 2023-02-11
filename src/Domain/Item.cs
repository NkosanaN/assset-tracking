namespace Domain;

/*
 * This model is design to hold Item having one FK at moment
 *  A)  FK => ShelveType
 *  B)  FK => ...
 */
public class Item
{
    public Guid Id { get; set; }
    public DateTime TimeStamp { get; set; } = DateTime.Now;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public ShelveType Shelve { get; set; }
}
