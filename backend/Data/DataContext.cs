using System.Text.Json;
using backend.models;
using backend.models.models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;

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
    public virtual DbSet<Country> Countries { get; set; }
    public virtual DbSet<Shipping> Shippings { get; set; }
    public virtual DbSet<Delivery> Deliveries { get; set; }
    public virtual DbSet<Conversion> UnitConversions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Save List to DB using serialization
        modelBuilder.Entity<CustomerProduct>()
        .Property(cp => cp.Products)
        .HasConversion(
            v => System.Text.Json.JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
            v => System.Text.Json.JsonSerializer.Deserialize<List<ProductQuantity>>(v, (JsonSerializerOptions)null),
            new ValueComparer<ICollection<ProductQuantity>>(
                (c1, c2) => c1.SequenceEqual(c2),
                c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                c => (ICollection<ProductQuantity>)c.ToList()
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

        modelBuilder.Entity<Conversion>()
        .HasData(
            new Conversion(1, "ml"),
            new Conversion(2, "l"),
            new Conversion(3, "g"),
            new Conversion(4, "kg")
        );

        modelBuilder.Entity<Customer>()
        .HasData(new Customer(
            Guid.NewGuid(), "John", "Doe", "john@mail.com", "doe100", true, 1, "555-555-5555"
        ));

        dynamic countries;
        using (StreamReader r = new StreamReader("Resources/JSON/countries.json"))
        {
            string json = r.ReadToEnd();
            countries = JsonConvert.DeserializeObject<List<Country>>(json);
        }
        modelBuilder.Entity<Country>()
        .HasData(
            countries
        );
    }
}
