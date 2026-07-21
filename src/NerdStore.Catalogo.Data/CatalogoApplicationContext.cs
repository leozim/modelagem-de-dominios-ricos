using Microsoft.EntityFrameworkCore;
using NerdStore.Catalogo.Domain;
using NerdStore.Core.Data;

namespace NerdStore.Catalogo.Data;

public class CatalogoApplicationContext : DbContext, IUnitOfWork
{
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Categoria> Categorias { get; set; }
    
    public CatalogoApplicationContext(DbContextOptions<CatalogoApplicationContext> options) 
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetProperties()
                     .Where(p => p.ClrType == typeof(string))))
        {
            property.SetColumnType("varchar(100)"); // evita nvarchar MAX
        }
        
        base.OnModelCreating(modelBuilder);
    }

    public async Task<bool> Commit()
    {
        throw new NotImplementedException();
    }
}