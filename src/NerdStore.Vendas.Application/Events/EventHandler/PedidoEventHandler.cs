using MediatR;
using NerdStore.Core.Communication.Mediator;
using NerdStore.Core.Messages.CommonMessages.IntegrationEvents;
using NerdStore.Vendas.Application.Commands;

namespace NerdStore.Vendas.Application.Events.EventHandler;

public class PedidoEventHandler :
    INotificationHandler<PedidoRascunhoIniciadoEvent>,
    INotificationHandler<PedidoAtualizadoEvent>,
    INotificationHandler<PedidoItemAdicionadoEvent>,
    INotificationHandler<PedidoEstoqueRejeitadoEvent>,
    INotificationHandler<PagamentoRealizadoEvent>,
    INotificationHandler<PagamentoRecusadoEvent>
{
    private readonly IMediatorHandler _mediatorHandler;

    public PedidoEventHandler(IMediatorHandler mediatorHandler)
    {
        _mediatorHandler = mediatorHandler;
    }

    public Task Handle(PedidoAtualizadoEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public Task Handle(PedidoItemAdicionadoEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public Task Handle(PedidoRascunhoIniciadoEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
    
    public Task Handle(PedidoEstoqueRejeitadoEvent notification, CancellationToken cancellationToken)
    {
        // cancelar o rpocessamento do pedido - retornar erro para o cliente
        return Task.CompletedTask;
    }

    public async Task Handle(PagamentoRealizadoEvent mensagem, CancellationToken cancellationToken)
    {
        await _mediatorHandler.EnviarComando(new FinalizarPedidoCommand(mensagem.PedidoId, mensagem.ClienteId));
    }

    public async Task Handle(PagamentoRecusadoEvent mensagem, CancellationToken cancellationToken)
    {
        await _mediatorHandler.EnviarComando(
            new CancelarProcessamentoPedidoEstornarEstoqueCommand(
                mensagem.PedidoId, mensagem.ClienteId));        
    }

}