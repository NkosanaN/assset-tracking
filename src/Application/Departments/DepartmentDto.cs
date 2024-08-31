namespace Application.Departments;
public class DepartmentDto
{
    public Guid DepartmentId { get; set; }
    public string DepartmentName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}

