using NerdStore.Core.DomainObjects.DTO;

namespace NerdStore.Core.Messages.CommonMessages.IntegrationEvents;

public class PedidoProcessamnetoCanceladoEvent : IntegrationEvent
{
    public Guid PedidoId { get; private set; }
    public Guid ClienteId { get; private set; }
    public ListaProdutosPedidoDTO ListaProdutosPedidoDto { get; private set; }

    public PedidoProcessamnetoCanceladoEvent(Guid pedidoId, Guid clienteId, ListaProdutosPedidoDTO listaProdutosPedidoDto)
    {
        PedidoId = pedidoId;
        ClienteId = clienteId;
        ListaProdutosPedidoDto = listaProdutosPedidoDto;
    }
}