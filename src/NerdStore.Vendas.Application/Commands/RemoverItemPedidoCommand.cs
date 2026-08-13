using FluentValidation;
using NerdStore.Core.Messages;

namespace NerdStore.Vendas.Application.Commands;

public class RemoverItemPedidoCommand : Command
{


    public override bool EhValido()
    {
        ValidationResult = new RemoverItemPedidoValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}

public class RemoverItemPedidoValidation : AbstractValidator<RemoverItemPedidoCommand>
{
    public RemoverItemPedidoValidation()
    {
        
    }
}