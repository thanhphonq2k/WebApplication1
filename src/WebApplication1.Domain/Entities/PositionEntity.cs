using WebApplication1.Domain.Common;

namespace WebApplication1.Domain.Entities;

public class PositionEntity : BaseEntity
{
    public int PositionId { get; set; }
    public string PositionName { get; set; } = string.Empty;
}
