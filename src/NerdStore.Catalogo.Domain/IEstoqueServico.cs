namespace NerdStore.Catalogo.Domain;

public interface IEstoqueServico : IDisposable
{
    Task<bool> DebitarEstoque(Guid produtoId, int quantidade);
    Task<bool> ReporEstoque(Guid produtoId, int quantidade);
}