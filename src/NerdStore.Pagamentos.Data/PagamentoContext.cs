using Microsoft.EntityFrameworkCore;
using NerdStore.Core.Communication.Mediator;
using NerdStore.Core.Data;
using NerdStore.Core.DomainObjects;
using NerdStore.Core.Messages;
using NerdStore.Pagamentos.Business;

namespace NerdStore.Pagamentos.Data;

public class PagamentoContext : DbContext, IUnitOfWork
{
    private readonly IMediatorHandler _mediatorhandler;

    public PagamentoContext(DbContextOptions<PagamentoContext> options, IMediatorHandler mediatorhandler)
        : base(options)
    {
        _mediatorhandler = mediatorhandler ?? throw new ArgumentNullException(nameof(mediatorhandler));
    }
    
    public DbSet<Pagamento> Pagamentos { get; set; }
    public DbSet<Transacao> Transacoes { get; set; }

    public async Task<bool> Commit()
    {
        foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataCadastro") != null))
        {
            if (entry.State == EntityState.Added)
            {
                entry.Property("DataCadastro").CurrentValue = DateTime.Now;
            }

            if (entry.State == EntityState.Modified)
            {
                entry.Property("DataCadastro").IsModified = false;
            }
        }

        var sucesso = await base.SaveChangesAsync() > 0;
        if (sucesso) await _mediatorhandler.PublicarEventos(this);

        return sucesso;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
            e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
            property.SetColumnType("varchar(100)");

        modelBuilder.Ignore<Event>();
        
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PagamentoContext).Assembly);

    }
}