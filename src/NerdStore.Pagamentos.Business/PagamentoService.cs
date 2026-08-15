using NerdStore.Core.Communication.Mediator;
using NerdStore.Core.DomainObjects.DTO;
using NerdStore.Core.Messages.CommonMessages.IntegrationEvents;
using NerdStore.Core.Messages.CommonMessages.Notifications;

namespace NerdStore.Pagamentos.Business;

public class PagamentoService : IPagamentoService
{
    private readonly IPagamentoCartaoCreditoFacade _pagamentoCartaoCreditoFacade;
    private readonly IPagamentoRepository _pagamentoRepository;
    private readonly IMediatorHandler _mediatorHandler;

    public PagamentoService(IPagamentoCartaoCreditoFacade pagamentoCartaoCreditoFacade, IPagamentoRepository pagamentoRepository, IMediatorHandler mediatorHandler)
    {
        _pagamentoCartaoCreditoFacade = pagamentoCartaoCreditoFacade;
        _pagamentoRepository = pagamentoRepository;
        _mediatorHandler = mediatorHandler;
    }

    public async Task<Transacao> RealizarPagamentoPedido(PagamentoPedidoDTO pagamentoPedidoDto)
    {
        var pedido = new Pedido
        {
            Id = pagamentoPedidoDto.PedidoId,
            Valor = pagamentoPedidoDto.Total
        };

        var pagamento = new Pagamento
        {
            Valor = pagamentoPedidoDto.Total,
            NomeCartao = pagamentoPedidoDto.NomeCartao,
            NumeroCartao = pagamentoPedidoDto.NumeroCartao,
            ExpiracaoCartao = pagamentoPedidoDto.ExpiracaoCartao,
            CvvCartao = pagamentoPedidoDto.CvvCartao,
            PedidoId = pagamentoPedidoDto.PedidoId
        };

        var transacao = _pagamentoCartaoCreditoFacade.RealizarPagamento(pedido, pagamento);

        if (transacao.StatusTransacao == StatusTransacao.Pago)
        {
            pagamento.AdicionarEvento(new PagamentoRealizadoEvent(pedido.Id, pagamentoPedidoDto.ClienteId, transacao.PagamentoId, transacao.Id, pedido.Valor));
            
            _pagamentoRepository.Adicionar(pagamento);
            _pagamentoRepository.AdicionarTransacao(transacao);

            await _pagamentoRepository.UnitOfWork.Commit();
            return transacao;
        }

        await _mediatorHandler.PublicarNotificacao(new DomainNotification("pagamento", "A operadora recusou o pagamento"));
        await _mediatorHandler.PublicarEvento(new PagamentoRecusadoEvent(pedido.Id, pagamentoPedidoDto.ClienteId, transacao.PagamentoId, transacao.Id, pedido.Valor));
        
        return transacao;
    }
}