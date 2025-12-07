using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Lead> Leads { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Lead>(eb =>
            {
                eb.HasKey(x => x.Id);
                eb.Property(x => x.FirstName).HasMaxLength(100).IsRequired();
                eb.Property(x => x.LastName).HasMaxLength(100);
                eb.Property(x => x.Suburb).HasMaxLength(100);
                eb.Property(x => x.Category).HasMaxLength(100);
                eb.Property(x => x.Email).HasMaxLength(200);
                eb.Property(x => x.Phone).HasMaxLength(50);
                eb.Property(x => x.Price).HasColumnType("decimal(10,2)");
                eb.Property(x => x.Description).HasColumnType("nvarchar(max)");
                eb.Property(x => x.Status).HasConversion<string>();
            });
        }
    }
}
