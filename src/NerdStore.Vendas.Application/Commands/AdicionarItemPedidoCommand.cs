using FluentValidation;
using NerdStore.Core.Messages;

namespace NerdStore.Vendas.Application.Commands;

public class AdicionarItemPedidoCommand : Command
{
    public AdicionarItemPedidoCommand(Guid clientId, Guid produtoId, string nome, int quantidade, decimal valorUnitario)
    {
        ClientId = clientId;
        ProdutoId = produtoId;
        Nome = nome;
        Quantidade = quantidade;
        ValorUnitario = valorUnitario;
    }

    public Guid ClientId { get; }
    public Guid ProdutoId { get; }
    public string Nome { get; }
    public int Quantidade { get; }
    public decimal ValorUnitario { get; }

    public override bool EhValido()
    {
        ValidationResult = new AdicionarItemPedidoValidation().Validate(this);
        return ValidationResult.IsValid;
    }

    public class AdicionarItemPedidoValidation : AbstractValidator<AdicionarItemPedidoCommand>
    {
        public AdicionarItemPedidoValidation()
        {
            RuleFor(c => c.ClientId)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do cliente inválido");

            RuleFor(c => c.ProdutoId)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do cliente inválido");

            RuleFor(c => c.Nome)
                .NotEmpty()
                .WithMessage("O nome do produto não foi informado");

            RuleFor(c => c.Quantidade)
                .GreaterThan(0)
                .WithMessage("A quantidade mínima de um item é 1");

            RuleFor(c => c.ValorUnitario)
                .LessThan(15)
                .WithMessage("A quantidade máxima de um item é 15");

            RuleFor(c => c.ValorUnitario)
                .GreaterThan(0)
                .WithMessage("O valor do item precisa ser maior que 0");
        }
    }
}