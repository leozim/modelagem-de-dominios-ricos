using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NerdStore.Vendas.Domain;

namespace NerdStore.Vendas.Data.Mapping;

public class PedidoMapping : IEntityTypeConfiguration<Pedido>
{
    public void Configure(EntityTypeBuilder<Pedido> builder)
    {
        builder.HasKey(c => c.Id);
        
        builder.Property(c => c.Codigo)
            .HasDefaultValueSql("NEXT VALUE FOR MinhaSequencia");
        
        // 1 : N => Pedido : PedidoItems
        builder.HasMany(c => c.PedidoItems)
            .WithOne(p => p.Pedido)
            .HasForeignKey(p => p.PedidoId);

        builder.ToTable("Pedidos");
    }
}