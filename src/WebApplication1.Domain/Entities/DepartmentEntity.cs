using WebApplication1.Domain.Common;

namespace WebApplication1.Domain.Entities;

public class DepartmentEntity : BaseEntity
{
    public int DepartmentId { get; set; }
    public string DepartmentName { get; set; } = string.Empty;
}
