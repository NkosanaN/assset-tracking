using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;

/*
 * This Class is design to hold shelves type
 */
//[Table("tblshelvetype")]
public class ShelveType
{
	[Key]
	public Guid ShelfId { get; set; }
	public string ShelfTag { get; set; } = string.Empty;
}



