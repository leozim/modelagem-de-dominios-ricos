namespace NerdStore.Pagamentos.Business;

public interface IPagamentoCartaoCreditoFacade
{
    public Transacao RealizarPagamento(Pedido pedido, Pagamento pagamento);
}