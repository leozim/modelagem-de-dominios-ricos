using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NerdStore.Vendas.Domain;

namespace NerdStore.Vendas.Data.Mapping;

public class VoucherMapping : IEntityTypeConfiguration<Voucher>
{
    public void Configure(EntityTypeBuilder<Voucher> builder)
    {
        builder.HasKey(p => p.Id);
        
        builder.Property(c => c.Codigo)
            .IsRequired()
            .HasColumnType("varchar(100)");
        
        // 1 : N => Voucher : Pèdidos
        builder.HasMany(c => c.Pedidos)
            .WithOne(p => p.Voucher)
            .HasForeignKey(p => p.VoucherId);
        
        builder.ToTable("Vouchers");
    }
}