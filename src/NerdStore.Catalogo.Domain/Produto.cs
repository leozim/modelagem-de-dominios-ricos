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

    public void AlterarDescicrao(string descricao)
    {
        Descricao = descricao;
    }

    public void DebitarEstoque(int quantidadeEstoque)
    {
        if (quantidadeEstoque <= 0) quantidadeEstoque *= -1;
        QuantidadeEstoque -= quantidadeEstoque;
    }

    public bool PossuiEstoque(int quantidadeEstoque)
    {
        return QuantidadeEstoque >= quantidadeEstoque;
    }

    public void Validar()
    {
        Validacoes.ValidarSeVazio(Nome, "O campo Nome do produto não pode estar vazio");
        Validacoes.ValidarSeVazio(Descricao, "O campo Descricao do produto não pode estar vazio");
        Validacoes.ValidarSeIgual(CategoriaId, Guid.Empty, "O campo CategoriaId do produto não pode estar vazio");
        Validacoes.ValidarSeMenorQue(Valor, 1, "O campo Valor do produto não pode se menor igual a 0");
        Validacoes.ValidarSeVazio(Imagem, "O campo Imagem do produto não pode estar vazio");
    }
}

public class Categoria : Entity
{
    public string Nome { get; private set; }
    public int Codigo { get; private set; }

    public Categoria(string nome, int codigo)
    {
        Nome = nome;
        Codigo = codigo;
    }

    public override string ToString() => $"{Nome} - {Codigo}";
}