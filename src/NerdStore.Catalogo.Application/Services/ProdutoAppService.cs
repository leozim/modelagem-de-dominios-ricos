using AutoMapper;
using NerdStore.Catalogo.Application.DTOs;
using NerdStore.Catalogo.Domain;
using NerdStore.Core.DomainObjects;

namespace NerdStore.Catalogo.Application.Services;

public class ProdutoAppService : IProdutoAppService
{
    private readonly IProdutoRepository _produtoRepository;
    private readonly IMapper _mapper;
    private readonly IEstoqueService _estoqueService;

    public ProdutoAppService(IProdutoRepository produtoRepository, 
                             IMapper mapper, 
                             IEstoqueService estoqueService)
    {
        _produtoRepository = produtoRepository;
        _mapper = mapper;
        _estoqueService = estoqueService;
    }

    public void Dispose()
    {
        _produtoRepository?.Dispose();
        _estoqueService?.Dispose();
    }

    public async Task<IEnumerable<ProdutoDto>> ObterPorCategoria(int codigo)
    {
        return _mapper.Map<IEnumerable<ProdutoDto>>(await _produtoRepository.ObterPorCategoria(codigo));
    }

    public async Task<ProdutoDto> ObterPorId(Guid id)
    {
        return _mapper.Map<ProdutoDto>(await _produtoRepository.ObterPorId(id));
    }

    public async Task<IEnumerable<ProdutoDto>> ObterTodos()
    {
        return _mapper.Map<IEnumerable<ProdutoDto>>(await _produtoRepository.ObterTodos());
    }

    public async Task<IEnumerable<CategoriaDto>> ObterCategorias()
    {
        return _mapper.Map<IEnumerable<CategoriaDto>>(await _produtoRepository.ObterCategorias());
    }

    public async Task AdicionarProduto(ProdutoDto produtoDto)
    {
        var produto = _mapper.Map<Produto>(produtoDto);
        _produtoRepository.Adicionar(produto);

        await _produtoRepository.UnitOfWork.Commit();
    }

    public async Task AtualizarProduto(ProdutoDto produtoDto)
    {
        var produto = _mapper.Map<Produto>(produtoDto);
        _produtoRepository.Atualizar(produto);
        
        await _produtoRepository.UnitOfWork.Commit();
    }

    public async Task<ProdutoDto> DebitarEstoque(Guid id, int quantidade)
    {
        if (!_estoqueService.DebitarEstoque(id, quantidade).Result)
        {
            throw new DomainException("Falha ao debitar estoque");
        }

        return _mapper.Map<ProdutoDto>(await _produtoRepository.ObterPorId(id));
    }

    public async Task<ProdutoDto> ReporEstoque(Guid id, int quantidade)
    {
        if (!_estoqueService.ReporEstoque(id, quantidade).Result)
        {
            throw new DomainException("Falha ao debitar estoque");
        }

        return _mapper.Map<ProdutoDto>(await _produtoRepository.ObterPorId(id));
    }
}