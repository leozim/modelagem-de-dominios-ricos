using NerdStore.Vendas.Application.Queries.Dto;
using NerdStore.Vendas.Domain;

namespace NerdStore.Vendas.Application.Queries;

public class PedidoQueries : IPedidoQueries
{
    private readonly IPedidoRepository _pedidoRepository;

    public PedidoQueries(IPedidoRepository pedidoRepository)
    {
        _pedidoRepository = pedidoRepository;
    }

    public async Task<CarrinhoDto> ObterCarrinhoCliente(Guid clienteId)
    {
        var pedido = await _pedidoRepository.ObterPedidoRascunhoPorClienteId(clienteId);
        if (pedido == null) return null;

        var carrinho = new CarrinhoDto
        {
            ClienteId = pedido.ClientId,
            ValorTotal = pedido.ValorTotal,
            PedidoId = pedido.Id,
            ValorDesconto = pedido.Desconto,
            SubTotal = pedido.Desconto + pedido.ValorTotal
        };

        if (pedido.VoucherId != null)
        {
            carrinho.VoucherCodigo = pedido.Voucher.Codigo;
        }

        foreach (var item in pedido.PedidoItems)
        {
            carrinho.items.Add(new CarrinhoItemDto
            {
                ProdutoId = item.ProdutoId,
                ProdutoNome = item.ProdutoNome,
                Quantidade = item.Quantidade,
                ValorUnitario = item.ValorUnitario,
                ValorTotal = item.ValorUnitario * item.Quantidade
            });
        }
        
        return carrinho;
    }

    public async Task<IEnumerable<PedidoDto>> ObterPedidosCliente(Guid clienteId)
    {
        throw new NotImplementedException();
    }
}