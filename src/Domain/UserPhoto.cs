using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    [Table("tbluserphoto")]
    public class UserPhoto
    {
        public string Id { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public bool IsMain { get; set; }
    }
}
