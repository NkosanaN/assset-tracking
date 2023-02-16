using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    [Table("tblitemimage")]
    public class ItemImage
    {
        public string Id { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
