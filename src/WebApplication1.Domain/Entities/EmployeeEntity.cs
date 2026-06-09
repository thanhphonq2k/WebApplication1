using WebApplication1.Domain.Common;

namespace WebApplication1.Domain.Entities;

public class EmployeeEntity : BaseEntity
{
    public int EmployeeId { get; set; }
    public string EmployeeName { get; set; } = string.Empty;
    public string Department { get; set; } = string.Empty;
    public string Position { get; set; } = string.Empty;
    public DateTime? DateOfJoining { get; set; }
    public string PhotoFileName { get; set; } = string.Empty;
}
