namespace EventBus.Messages.Events;

public abstract class IntegrationBaseEvent
{
    protected IntegrationBaseEvent()
    {
        Id = Guid.NewGuid();
        CreationDate = DateTime.UtcNow;
    }

    protected IntegrationBaseEvent(Guid id, DateTime createDate)
    {
        Id = id;
        CreationDate = createDate;
    }

    public Guid Id { get; private set; }

    public DateTime CreationDate { get; private set; }
}