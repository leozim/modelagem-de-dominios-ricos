using FluentValidation;
using NerdStore.Core.Messages;

namespace NerdStore.Vendas.Application.Commands;

public class RemoverItemPedidoCommand : Command
{
    public Guid ClienteId { get; private set; }
    public Guid PedidoId { get; private set; }
    public Guid ProdutoId { get; private set; }

    public RemoverItemPedidoCommand(Guid clienteId, Guid pedidoId, Guid produtoId)
    {
        ClienteId = clienteId;
        PedidoId = pedidoId;
        ProdutoId = produtoId;
    }

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
        RuleFor(c => c.ClienteId)
            .NotEqual(Guid.Empty)
            .WithMessage("Cliente Id invalido");
        
        RuleFor(c => c.PedidoId)
            .NotEqual(Guid.Empty)
            .WithMessage("Pedido Id invalido");
        
        RuleFor(c => c.ProdutoId)
            .NotEqual(Guid.Empty)
            .WithMessage("Produto Id invalido");
    }
}