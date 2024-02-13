namespace Domain;

public abstract class AuditableEntity<TKey>
{
    public TKey Id { get; set; }
}

public abstract class AuditableEntity : AuditableEntity<int>
{
}
