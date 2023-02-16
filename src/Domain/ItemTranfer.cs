namespace Domain
{
    public class ItemTranfer
    {
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public Guid ItemId { get; set; }
        public Item? Item { get; set; }
        public bool InOut { get; set; }
    }
}
