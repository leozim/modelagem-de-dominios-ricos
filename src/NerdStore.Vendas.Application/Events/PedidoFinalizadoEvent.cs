using NerdStore.Core.Messages;

namespace NerdStore.Vendas.Application.Events;

public class PedidoFinalizadoEvent : Event
{
    public PedidoFinalizadoEvent(Guid pedidoId)
    {
        AggregateId = pedidoId;
        PedidoId = pedidoId;
    }
    
    public Guid PedidoId { get; private set; }

}