using FluentValidation;
using NerdStore.Core.Messages;

namespace NerdStore.Vendas.Application.Commands;

public class AplicarVoucherPedidoCommand : Command
{
    public AplicarVoucherPedidoCommand(Guid clienteId, string codigoVoucher)
    {
        ClienteId = clienteId;
        CodigoVoucher = codigoVoucher;
    }

    public Guid ClienteId { get; }
    public string CodigoVoucher { get; }

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

        RuleFor(c => c.CodigoVoucher)
            .NotEmpty()
            .WithMessage("Codigo Voucher invalido ou nao pode ser vazio");
    }
}