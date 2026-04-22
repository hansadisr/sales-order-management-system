using Microsoft.EntityFrameworkCore;
using SalesOrderAPI.Domain.Entities;

namespace SalesOrderAPI.Infrastructure.Data
{
    public class SalesDbContext : DbContext
    {
        public SalesDbContext(DbContextOptions<SalesDbContext> options) : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<SalesOrder> SalesOrders { get; set; }
        public DbSet<SalesOrderDetail> SalesOrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            

            // Explicit table mapping:
            modelBuilder.Entity<Client>().ToTable("Client");
            modelBuilder.Entity<Item>().ToTable("Item");
            modelBuilder.Entity<SalesOrder>().ToTable("SalesOrder");
            modelBuilder.Entity<SalesOrderDetail>().ToTable("SalesOrderDetail");

            // Configure decimal precision for financial values
            modelBuilder.Entity<SalesOrderDetail>()
                .Property(e => e.ExclAmount)
                .HasPrecision(18, 2);
            modelBuilder.Entity<SalesOrderDetail>()
                .Property(e => e.TaxAmount)
                .HasPrecision(18, 2);
            modelBuilder.Entity<SalesOrderDetail>()
                .Property(e => e.InclAmount)
                .HasPrecision(18, 2);

            // Seed data
            modelBuilder.Entity<Client>().HasData(
                new Client
                {
                    ClientId = 1,
                    Name = "Acme Corporation",
                    Address1 = "123 Business Street",
                    Address2 = "Suite 100",
                    Address3 = "",
                    Suburb = "Sydney",
                    State = "NSW",
                    PostCode = "2000"
                },
                new Client
                {
                    ClientId = 2,
                    Name = "Global Industries",
                    Address1 = "456 Commerce Ave",
                    Address2 = "Level 5",
                    Address3 = "",
                    Suburb = "Melbourne",
                    State = "VIC",
                    PostCode = "3000"
                },
                new Client
                {
                    ClientId = 3,
                    Name = "Tech Solutions Pty Ltd",
                    Address1 = "789 Innovation Drive",
                    Address2 = "",
                    Address3 = "",
                    Suburb = "Brisbane",
                    State = "QLD",
                    PostCode = "4000"
                }
            );

            modelBuilder.Entity<Item>().HasData(
                new Item
                {
                    ItemId = 1,
                    ItemCode = "ITEM001",
                    Description = "Professional Consulting Services",
                    Price = 150.00m,
                    TaxRate = 0.10m
                },
                new Item
                {
                    ItemId = 2,
                    ItemCode = "ITEM002",
                    Description = "Software License (Annual)",
                    Price = 1200.00m,
                    TaxRate = 0.10m
                },
                new Item
                {
                    ItemId = 3,
                    ItemCode = "ITEM003",
                    Description = "Technical Support Package",
                    Price = 500.00m,
                    TaxRate = 0.10m
                },
                new Item
                {
                    ItemId = 4,
                    ItemCode = "ITEM004",
                    Description = "Hardware Installation",
                    Price = 350.00m,
                    TaxRate = 0.10m
                },
                new Item
                {
                    ItemId = 5,
                    ItemCode = "ITEM005",
                    Description = "Training and Documentation",
                    Price = 250.00m,
                    TaxRate = 0.10m
                }
            );
        }
    }
}
