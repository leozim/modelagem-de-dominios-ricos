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

    public PedidoItem(Guid pedidoId, 
                      Guid produtoId, 
                      string produtoNome, 
                      int quantidade, 
                      decimal valorUnitario, 
                      Pedido pedido)
    {
        PedidoId = pedidoId;
        ProdutoId = produtoId;
        ProdutoNome = produtoNome;
        Quantidade = quantidade;
        ValorUnitario = valorUnitario;
        Pedido = pedido;
    }
}