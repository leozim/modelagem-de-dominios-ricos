using NerdStore.Vendas.Application.Queries.DTO;

namespace NerdStore.Vendas.Application.Queries;

public interface IPedidoQueries
{
    Task<CarrinhoDto> ObterCarrinhoCliente(Guid clienteId);
    Task<IEnumerable<PedidoDto>> ObterPedidosCliente(Guid clienteId);
}