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

    protected Pedido() { }
    public Pedido(int codigo, 
                  Guid clientId, 
                  Guid? voucherId, 
                  bool voucherUtilizado, 
                  decimal desconto, 
                  decimal valorTotal, 
                  DateTime dataCadastro, 
                  PedidoStatus pedidoStatus)
    {
        Codigo = codigo;
        ClientId = clientId;
        VoucherId = voucherId;
        VoucherUtilizado = voucherUtilizado;
        Desconto = desconto;
        ValorTotal = valorTotal;
        DataCadastro = dataCadastro;
        PedidoStatus = pedidoStatus;
    }
}