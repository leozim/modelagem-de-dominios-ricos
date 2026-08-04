using MediatR;
using Microsoft.AspNetCore.Mvc;
using NerdStore.Core.Communication.Mediator;
using NerdStore.Core.Messages.CommonMessages.Notifications;

namespace NerdStore.WebApp.MVC.Controllers;

public abstract class ControllerBase : Controller
{
    private readonly DomainNotificationHandler _notifications;
    private readonly IMediatorHandler _mediatorHandler;
    
    protected Guid ClienteId = Guid.Parse("a21563ca-5e00-4302-ae5e-c5496fd5d535");

    protected ControllerBase(INotificationHandler<DomainNotification> notifications, 
                             IMediatorHandler mediatorHandler)
    {
        _notifications = (DomainNotificationHandler)notifications;
        _mediatorHandler = mediatorHandler;
    }

    protected bool OperacaoValida()
    {
        return !_notifications.TemNotificacao();
    }

    protected IEnumerable<string> ObterMensagensErro()
    {
        return _notifications.ObterNotifications().Select(n => n.Value).ToList();
    }

    protected void NotificarErro(string codigo, string mensagem)
    {
        _mediatorHandler.PublicarNotificacao(new DomainNotification(codigo, mensagem));
    }
}