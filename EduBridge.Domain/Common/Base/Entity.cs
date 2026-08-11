namespace EduBridge.Domain.Common.Base;

public abstract class Entity
{
    public Guid Id { get; private set; }
    protected Entity()
    {
        Id = Guid.NewGuid();
    }
}