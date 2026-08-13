using FluentValidation;
using NerdStore.Core.Messages;

namespace NerdStore.Vendas.Application.Commands;

public class AplicarVoucherPedidoCommand : Command
{


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
        
    }
}