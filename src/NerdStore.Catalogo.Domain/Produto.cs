using NerdStore.Core.DomainObjects;

namespace NerdStore.Catalogo.Domain;

// Interface of aggregation: Aggregate Root
public class Produto : Entity, IAgregateRoot
{
    public string Nome { get; private set; }
    public string Descricao { get; private set; }
    public bool Ativo { get; private set; }
    public decimal Valor { get; private set; }
    public DateTime DataCadastro { get; private set; }
    public string Imagem { get; private set; }
    public int QuantidadeEstoque { get; private set; }
}

public class Categoria : Entity
{
}