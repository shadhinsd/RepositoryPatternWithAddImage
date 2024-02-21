namespace RepositoryWithAddImage.Models.Base;

public class AuditableEntity<TId>:BaseEntity
{
    public AuditableEntity() => IsDeleted = false;  
    public DateTimeOffset CreatedDate { get; set; }
    public long CreatedBy { get; set; }
    public DateTimeOffset ModifyidDate { get; set; }
    public long ModifyidBy { get; set;}
    public bool IsDeleted { get; set; }
}
public class AuditableEntity: AuditableEntity<long> { }