using FluentValidation.Results;
using NerdStore.Core.DomainObjects;

namespace NerdStore.Vendas.Domain;

public class Pedido : Entity, IAgregateRoot
{
    private readonly List<PedidoItem> _pedidoItems;

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

    public int Codigo { get; private set; }
    public Guid ClientId { get; private set; }
    public Guid? VoucherId { get; private set; }
    public bool VoucherUtilizado { get; private set; }
    public decimal Desconto { get; private set; }
    public decimal ValorTotal { get; private set; }
    public DateTime DataCadastro { get; private set; }
    public PedidoStatus PedidoStatus { get; private set; }

    public IReadOnlyCollection<PedidoItem> PedidoItems => _pedidoItems.AsReadOnly();

    // EF Rel.
    public Voucher Voucher { get; private set; }

    public ValidationResult AplicarVoucher(Voucher voucher)
    {
        var validationResult = voucher.ValidarSeAplicavel();
        if (!validationResult.IsValid) return validationResult;

        Voucher = voucher;
        VoucherUtilizado = true;
        CalcularValorPedido();

        return validationResult;
    }

    public void CalcularValorPedido()
    {
        ValorTotal = PedidoItems.Sum(p => p.CalcularValor());
        CalcularValorTotalDesconto();
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
                desconto = valor * Voucher.Percentual.Value / 100;
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

    public bool PedidoItemExistente(PedidoItem item)
    {
        return _pedidoItems.Any(p => p.ProdutoId == item.ProdutoId);
    }

    public void AdicionarItem(PedidoItem item)
    {
        if (!item.EhValido()) return;

        item.AssociarPedido(Id);

        if (PedidoItemExistente(item))
        {
            var itemExistente = _pedidoItems.FirstOrDefault(p => p.ProdutoId == item.ProdutoId);
            itemExistente.AdicionarUnidades(item.Quantidade);
            item = itemExistente;

            _pedidoItems.Remove(itemExistente);
        }

        item.CalcularValor();
        _pedidoItems.Add(item);

        CalcularValorPedido();
    }

    public void RemoverItem(PedidoItem item)
    {
        if (!item.EhValido()) return;

        var itemExistente = PedidoItems.FirstOrDefault(p => p.ProdutoId == item.ProdutoId);

        if (itemExistente == null) throw new DomainException("O item não pertence ao pedido");
        _pedidoItems.Remove(itemExistente);

        CalcularValorPedido();
    }

    public void AtualizarItem(PedidoItem item)
    {
        if (!item.EhValido()) return;
        item.AssociarPedido(Id);

        var itemExistente = PedidoItems.FirstOrDefault(p => p.ProdutoId == item.ProdutoId);

        if (itemExistente == null) throw new DomainException("O item não pertece ao pedido");

        _pedidoItems.Remove(itemExistente);
        _pedidoItems.Add(item);

        CalcularValorPedido();
    }

    public void AtualizarUnidades(PedidoItem item, int unidades)
    {
        item.AtualizarUnidades(unidades);
        AtualizarItem(item);
    }

    public void TornarRascunho()
    {
        PedidoStatus = PedidoStatus.Rascunho;
    }

    public void IniciarPedido()
    {
        PedidoStatus = PedidoStatus.Iniciado;
    }

    public void FinalizarPedido()
    {
        PedidoStatus = PedidoStatus.Pago;
    }

    public void CancelarPedido()
    {
        PedidoStatus = PedidoStatus.Cancelado;
    }

    public static class PedidoFactory
    {
        public static Pedido NovoPedidoRascinho(Guid clienteId)
        {
            return new Pedido
            {
                ClientId = clienteId,
                PedidoStatus = PedidoStatus.Rascunho
            };
        }
    }
}