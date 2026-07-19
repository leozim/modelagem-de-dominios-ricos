using NerdStore.Core.DomainObjects;

namespace NerdStore.Catalogo.Domain;

// Interface of aggregation: Aggregate Root
public class Produto : Entity, IAgregateRoot
{
    public Guid CategoriaId { get; private set; }
    public string Nome { get; private set; }
    public string Descricao { get; private set; }
    public bool Ativo { get; private set; }
    public decimal Valor { get; private set; }
    public DateTime DataCadastro { get; private set; }
    public string Imagem { get; private set; }
    public int QuantidadeEstoque { get; private set; }

    public Categoria Categoria { get; private set; }

    public Produto(Guid categoriaId,
                   string nome, 
                   string descricao, 
                   bool ativo, 
                   decimal valor, 
                   DateTime dataCadastro, 
                   string imagem)
    {
        CategoriaId = categoriaId;
        Nome = nome;
        Descricao = descricao;
        Ativo = ativo;
        Valor = valor;
        DataCadastro = dataCadastro;
        Imagem = imagem;
    }

    public void Ativar() => Ativo = true;
    public void Desativar() => Ativo = false;

    public void AlterarCategoria(Categoria categoria)
    {
        Categoria = categoria;
        CategoriaId = categoria.Id;
    }
}

public class Categoria : Entity
{
}