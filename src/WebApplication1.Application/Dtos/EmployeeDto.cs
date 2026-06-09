namespace WebApplication1.Application.Dtos;

public class EmployeeDto
{
    public int EmployeeId { get; set; }
    public string EmployeeName { get; set; } = string.Empty;
    public string Department { get; set; } = string.Empty;
    public string Position { get; set; } = string.Empty;
    public string DateOfJoining { get; set; } = string.Empty;
    public string PhotoFileName { get; set; } = string.Empty;
}
