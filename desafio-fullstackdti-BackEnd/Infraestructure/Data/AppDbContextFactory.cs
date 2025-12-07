using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Data
{
  
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
          
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            const string connectionString = "Server=localhost,1433;Database=LeadsDB;User Id=sa;Password=SqlServer@123;TrustServerCertificate=True";

            optionsBuilder.UseSqlServer(connectionString);

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}