/*using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace NerdStore.Vendas.Data;

public class VendasContextFactory : IDesignTimeDbContextFactory<VendasContext>
{
    public VendasContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<VendasContext>();

        var connectionString =
            "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=NerdStoreDb;Integrated Security=True;pooling=True";

        optionsBuilder.UseSqlServer(connectionString);

        return new VendasContext(optionsBuilder.Options);
    }
}*/

