namespace NerdStore.Core.DomainObjects;

public abstract class Entity
{
    public Guid Id { get; set; }

    protected Entity()
    {
        Id = Guid.NewGuid();
    }

    public override bool Equals(object obj)
    {
        var compareTo = obj as Entity;

        if (ReferenceEquals(this, compareTo)) return true;
        if(ReferenceEquals(null, compareTo)) return false;
        
        return Id.Equals(compareTo.Id);
    }
}