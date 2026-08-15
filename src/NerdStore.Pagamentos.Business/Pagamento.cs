using NerdStore.Core.DomainObjects;

namespace NerdStore.Pagamentos.Business;

public class Pagamento : Entity, IAgregateRoot
{
    public Guid PedidoId { get; set; }
    public string Status { get; set; }
    public decimal Valor { get; set; }
    
    public string NomeCartao { get; set; }
    public string NumeroCartaao { get; set; }
    public string ExpiracaoCartao { get; set; }
    public string CvvCartao { get; set; }
    
    // EF. Rel.
    public Transacao Transacao { get; set; }
}