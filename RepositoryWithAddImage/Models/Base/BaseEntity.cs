namespace RepositoryWithAddImage.Models.Base;

public class BaseEntity<TId>
{
    public TId Id { get; set; }
}
public class BaseEntity:BaseEntity<long> { }