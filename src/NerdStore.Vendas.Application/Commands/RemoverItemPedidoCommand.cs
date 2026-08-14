using FluentValidation;
using NerdStore.Core.Messages;

namespace NerdStore.Vendas.Application.Commands;

public class RemoverItemPedidoCommand : Command
{
    public RemoverItemPedidoCommand(Guid clienteId, Guid produtoId)
    {
        ClienteId = clienteId;
        ProdutoId = produtoId;
    }

    public Guid ClienteId { get; }
    public Guid ProdutoId { get; }

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

        RuleFor(c => c.ProdutoId)
            .NotEqual(Guid.Empty)
            .WithMessage("Produto Id invalido");
    }
}