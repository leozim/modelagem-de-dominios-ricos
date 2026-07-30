using Microsoft.AspNetCore.Mvc;
using NerdStore.Catalogo.Application.Services;
using NerdStore.Catalogo.Domain;
using NerdStore.Vendas.Application.Commands;

namespace NerdStore.WebApp.MVC.Controllers;

public class CarrinhoController : Controller
{
    private readonly IProdutoAppService _produtoAppService;

    public CarrinhoController(IProdutoAppService produtoAppService)
    {
        _produtoAppService = produtoAppService;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    [Route("meu-carrinho")]
    public async Task<IActionResult> AdicionarItem(Guid id, int quantidade)
    {
        var produto = await _produtoAppService.ObterPorId(id);
        if (produto == null) return BadRequest();

        if (produto.QuantidadeEstoque < quantidade)
        {
            TempData["Erro"] = "Produto com estoque insuficiente";
            return RedirectToAction("ProdutoDetalhe", "Vitrine", new {id});
        }

        var command = new AdicionarItemPedidoCommand(ClienteId, produto.Id, produto.Nome, quantidade, produto.Valor);

        TempData["Erro"] = "Pedido Indisponível";
        return RedirectToAction("ProdutoDetalhe", "Vitrine", new {id});
    }
}