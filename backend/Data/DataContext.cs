using System.Text.Json;
using backend.models;
using backend.models.models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace backend.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }
    public virtual DbSet<Customer> Customers { get; set; }
    public virtual DbSet<Roles> Roles { get; set; }
    public virtual DbSet<CustomerProduct> CustomerProducts { get; set; }
    public virtual DbSet<CustomerCollection> CustomerCollections { get; set; }
    public virtual DbSet<Order> Orders { get; set; }
    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<ProductType> ProductTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Save List to DB using serialization
        modelBuilder.Entity<CustomerProduct>()
        .Property(cp => cp.Products)
        .HasConversion(
            v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
            v => JsonSerializer.Deserialize<List<Product>>(v, (JsonSerializerOptions)null),
            new ValueComparer<ICollection<Product>>(
                (c1, c2) => c1.SequenceEqual(c2),
                c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                c => (ICollection<Product>)c.ToList()
            )
        );

        // Default Account and Role
        modelBuilder.Entity<Roles>()
        .HasData(
            new Roles(1, "Admin"),
            new Roles(2, "Customer")
        );

        modelBuilder.Entity<ProductType>()
        .HasData(
            new ProductType(1, "Beverages"),
            new ProductType(2, "Grains")
        );

        modelBuilder.Entity<Customer>()
        .HasData(new Customer(
            Guid.NewGuid(), "John", "Doe", "john@mail.com", "doe100", true, 1, "555-555-5555"
        )
        );
    }
}
