using FluentValidation;
using NerdStore.Core.Messages;

namespace NerdStore.Vendas.Application.Commands;

public class AplicarVoucherPedidoCommand : Command
{
    public Guid ClienteId { get; private set; }
    public Guid PedidoId { get; private set; }
    public string CodigoVoucher { get; private set; }

    public AplicarVoucherPedidoCommand(Guid clienteId, Guid pedidoId, string codigoVoucher)
    {
        ClienteId = clienteId;
        PedidoId = pedidoId;
        CodigoVoucher = codigoVoucher;
    }

    public override bool EhValido()
    {
        ValidationResult = new AplicarVoucherPedidoValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}

public class AplicarVoucherPedidoValidation : AbstractValidator<AplicarVoucherPedidoCommand>
{
    public AplicarVoucherPedidoValidation()
    {
        RuleFor(c => c.ClienteId)
            .NotEqual(Guid.Empty)
            .WithMessage("Cliente Id invalido");
        
        RuleFor(c => c.PedidoId)
            .NotEqual(Guid.Empty)
            .WithMessage("Pedido Id invalido");
        
        RuleFor(c => c.CodigoVoucher)
            .NotEmpty()
            .WithMessage("Codigo Voucher invalido ou nao pode ser vazio");
    }
}