using NerdStore.Core.DomainObjects;

namespace NerdStore.Vendas.Domain;

public class Pedido : Entity, IAgregateRoot
{
    private readonly List<PedidoItem> _pedidoItems;
    
    public int Codigo { get; private set; }
    public Guid ClientId { get; private set; }
    public Guid? VoucherId { get; private set; }
    public bool VoucherUtilizado { get; private set; }
    public decimal Desconto { get; private set; }
    public decimal ValorTotal { get; private set; }
    public DateTime DataCadastro { get; private set; }
    public PedidoStatus PedidoStatus { get; private set; }
    
    public IReadOnlyCollection<PedidoItem> PeidoItems => _pedidoItems.AsReadOnly();
    
    // EF Rel.
    public Voucher Voucher { get; private set; }

    protected Pedido()
    {
        _pedidoItems = new List<PedidoItem>();
    }
    public Pedido(int codigo, 
                  Guid clientId, 
                  bool voucherUtilizado, 
                  decimal desconto, 
                  decimal valorTotal)
    {
        ClientId = clientId;
        VoucherUtilizado = voucherUtilizado;
        Desconto = desconto;
        ValorTotal = valorTotal;
        _pedidoItems = new List<PedidoItem>();
    }

    public void CalcularValorTotalDesconto()
    {
        if (!VoucherUtilizado) return;

        decimal desconto = 0;
        var valor = ValorTotal;

        if (Voucher.TipoDescontoVoucher == TipoDescontoVoucher.Porcentagem)
        {
            if (Voucher.Percentual.HasValue)
            {
                desconto = (valor * Voucher.Percentual.Value) / 100;
                valor -= desconto;
            }
        }
        else
        {
            if (Voucher.Percentual.HasValue)
            {
                desconto = Voucher.ValorDesconto.Value;
                valor -= desconto;
            }
        }

        ValorTotal = valor < 0 ? 0 : valor;
        Desconto = desconto;
    }
    
}