using MediatR;
using NerdStore.Core.DomainObjects.DTO;
using NerdStore.Core.Messages.CommonMessages.IntegrationEvents;

namespace NerdStore.Pagamentos.Business.Events;

public class PagamentoEventHandler : INotificationHandler<PedidoEstoqueConfirmadoEvent>
{
    private readonly IPagamentoService _pagamentoService;

    public PagamentoEventHandler(IPagamentoService pagamentoService)
    {
        _pagamentoService = pagamentoService;
    }

    public async Task Handle(PedidoEstoqueConfirmadoEvent mensagem, CancellationToken cancellationToken)
    {
        var pagamentoPedido = new PagamentoPedidoDTO
        {
            PedidoId = mensagem.PedidoId,
            ClienteId = mensagem.ClienteId,
            Total = mensagem.Total,
            NomeCartao = mensagem.NomeCartao,
            NumeroCartao = mensagem.NumeroCartao,
            ExpiracaoCartao = mensagem.ExpiracaoCartao,
            CvvCartao = mensagem.CvvCartao
        };
        
        await _pagamentoService.RealizarPagamentoPedido(pagamentoPedido);
    }
}