using NerdStore.Core.DomainObjects;

namespace NerdStore.Vendas.Domain;

public class PedidoItem : Entity
{
    public Guid PedidoId { get; private set; }
    public Guid ProdutoId { get; private set; }
    public string ProdutoNome { get; private set; }
    public int Quantidade { get; private set; }
    public decimal ValorUnitario { get; private set; }

    // EF Rel.
    public Pedido Pedido { get; set; }
    
    protected PedidoItem() { }

    public PedidoItem(Guid pedidoId, 
                      Guid produtoId, 
                      string produtoNome, 
                      int quantidade, 
                      decimal valorUnitario)
    {
        PedidoId = pedidoId;
        ProdutoId = produtoId;
        ProdutoNome = produtoNome;
        Quantidade = quantidade;
        ValorUnitario = valorUnitario;
    }

    internal void AssociarPedido(Guid pedidoId)
    {
        PedidoId = pedidoId;
    }

    public decimal CalcularValor() => Quantidade * ValorUnitario;

    internal void AdicionarUnidades(int unidade)
    {
        Quantidade += unidade;
    }

    internal void AtualizarUnidades(int unidades)
    {
        Quantidade = unidades;
    }
}