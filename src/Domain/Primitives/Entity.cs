namespace Domain.Primitives;

public abstract class Entity
{
	private readonly List<DomainEvent> _domainEvents = new();

	public IReadOnlyCollection<DomainEvent> GetDomainEvents() => _domainEvents.ToList();

	protected void Raise(DomainEvent domainEvent) => _domainEvents.Add(domainEvent);
}

