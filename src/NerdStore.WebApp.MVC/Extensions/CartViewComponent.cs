using Microsoft.AspNetCore.Mvc;
using NerdStore.Vendas.Application.Queries;

namespace NerdStore.WebApp.MVC.Extensions;

public class CartViewComponent : ViewComponent
{
    private readonly IPedidoQueries _pedidoQueries;
    
    // TODO: Obter cliente logado
    protected Guid ClienteId = Guid.Parse("244f74bf-158e-4778-b058-8372df7107a9");

    public CartViewComponent(IPedidoQueries pedidoQueries)
    {
        _pedidoQueries = pedidoQueries;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var carrinho = await _pedidoQueries.ObterCarrinhoCliente(ClienteId);
        var itens = carrinho?.Items.Count ?? 0;

        return View(itens);
    }
}