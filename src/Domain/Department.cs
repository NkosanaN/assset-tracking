using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;

[Table("tbldepartment")]
public class Department
{
    public Guid DepartmentId { get; set; }
    public string DepartmentName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}

