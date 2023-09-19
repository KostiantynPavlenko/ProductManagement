namespace ProductManagement.Domain.Common;

public class BaseEntity : AuditableEntity
{
    public Guid Id { get; set; }
}