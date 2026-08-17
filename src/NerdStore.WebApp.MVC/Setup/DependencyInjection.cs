using MediatR;
using NerdStore.Catalogo.Application.Services;
using NerdStore.Catalogo.Data.Repository;
using NerdStore.Catalogo.Domain;
using NerdStore.Catalogo.Domain.Events;
using NerdStore.Core.Communication.Mediator;
using NerdStore.Core.Messages.CommonMessages.IntegrationEvents;
using NerdStore.Core.Messages.CommonMessages.Notifications;
using NerdStore.Pagamentos.AntiCorruption;
using NerdStore.Pagamentos.Business;
using NerdStore.Pagamentos.Business.Events;
using NerdStore.Pagamentos.Data.Repository;
using NerdStore.Vendas.Application.Commands;
using NerdStore.Vendas.Application.Commands.Handler;
using NerdStore.Vendas.Application.Events;
using NerdStore.Vendas.Application.Events.EventHandler;
using NerdStore.Vendas.Application.Queries;
using NerdStore.Vendas.Data.Repository;
using NerdStore.Vendas.Domain;
using ConfigurationManager = NerdStore.Pagamentos.AntiCorruption.ConfigurationManager;
using IConfigurationManager = NerdStore.Pagamentos.AntiCorruption.IConfigurationManager;

namespace NerdStore.WebApp.MVC.Setup;

public static class DependencyInjection
{
    public static void RegisterServices(this IServiceCollection services)
    {
        // (Mediator)
        services.AddScoped<IMediatorHandler, MediatorHandler>();

        // Notifications
        services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

        // Catalogo
        services.AddScoped<IProdutoRepository, ProdutoRepository>();
        services.AddScoped<IProdutoAppService, ProdutoAppService>();
        services.AddScoped<IEstoqueService, EstoqueService>();
        // services.AddScoped<CatalogoApplicationContext>();

        services.AddScoped<INotificationHandler<ProdutoAbaixoEstoqueEvent>, ProdutoEventHandler>();

        // Vendas
        services.AddScoped<IPedidoRepository, PedidoRepository>();
        services.AddScoped<IPedidoQueries, PedidoQueries>();

        services.AddScoped<IRequestHandler<AdicionarItemPedidoCommand, bool>, PedidoCommandHandler>();
        services.AddScoped<IRequestHandler<AtualizarItemPedidoCommand, bool>, PedidoCommandHandler>();
        services.AddScoped<IRequestHandler<RemoverItemPedidoCommand, bool>, PedidoCommandHandler>();
        services.AddScoped<IRequestHandler<AplicarVoucherPedidoCommand, bool>, PedidoCommandHandler>();
        services.AddScoped<IRequestHandler<IniciarPedidoCommand, bool>, PedidoCommandHandler>();
        services.AddScoped<IRequestHandler<FinalizarPedidoCommand, bool>, PedidoCommandHandler>();
        services.AddScoped<IRequestHandler<CancelarProcessamentoPedidoCommand, bool>, PedidoCommandHandler>();
        services.AddScoped<IRequestHandler<CancelarProcessamentoPedidoEstornarEstoqueCommand, bool>, PedidoCommandHandler>();

        services.AddScoped<INotificationHandler<PedidoRascunhoIniciadoEvent>, PedidoEventHandler>();
        services.AddScoped<INotificationHandler<PedidoEstoqueRejeitadoEvent>, PedidoEventHandler>();
        services.AddScoped<INotificationHandler<PagamentoRealizadoEvent>, PedidoEventHandler>();
        services.AddScoped<INotificationHandler<PagamentoRecusadoEvent>, PedidoEventHandler>();
        
        // Pagamentos
        services.AddScoped<IPagamentoRepository, PagamentoRepository>();
        services.AddScoped<IPagamentoService, PagamentoService>();
        services.AddScoped<IPagamentoCartaoCreditoFacade, PagamentoCartaoCreditoFacade>();
        services.AddScoped<IPayPalGateway, PayPalGateway>();
        services.AddScoped<IConfigurationManager, ConfigurationManager>();
        
        services.AddScoped<INotificationHandler<PedidoEstoqueConfirmadoEvent>, PagamentoEventHandler>();
    }
}