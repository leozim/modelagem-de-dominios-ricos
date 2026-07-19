using NerdStore.Core.DomainObjects;

namespace NerdStore.Catalogo.Domain;

// Interface of aggregation: Aggregate Root
public class Produto : Entity, IAgregateRoot
{
}

public class Categoria : Entity
{
}