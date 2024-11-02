using E_commerce.Models;
using Microsoft.EntityFrameworkCore;

namespace E_commerce.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Review> Reviews { get; set; }                          
        public DbSet<ShippingAddress> ShippingAddresses { get; set; }
        public DbSet<WishList> WishLists { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Revenue> Revenues { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<AdminHistory> Histories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Sales configuration
            modelBuilder.Entity<Sale>()
               .HasOne(s => s.User)
               .WithMany()
               .HasForeignKey(s => s.UserId)
               .OnDelete(DeleteBehavior.Restrict);

            // Configure decimal precision for MySQL
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)")
                .HasPrecision(18, 2);

            modelBuilder.Entity<OrderDetail>()
                .Property(od => od.Price)
                .HasColumnType("decimal(18,2)")
                .HasPrecision(18, 2);          

            modelBuilder.Entity<Sale>()
                .Property(s => s.TotalAmount)
                .HasColumnType("decimal(18,2)")
                .HasPrecision(18, 2);        

            modelBuilder.Entity<Revenue>()
                .Property(r => r.TotalRevenue)
                .HasColumnType("decimal(18,2)")
                .HasPrecision(18, 2);        

            // Inventory configuration
            modelBuilder.Entity<Inventory>()
               .HasOne(i => i.Product)
               .WithMany()
               .HasForeignKey(i => i.ProductId)
               .OnDelete(DeleteBehavior.Cascade);

            // Configure default character set and collation for MySQL
            modelBuilder.HasCharSet("utf8mb4")
                       .UseCollation("utf8mb4_unicode_ci");

            // Configure indices for better performance
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Product>()
                .HasIndex(p => p.ProductName);

            modelBuilder.Entity<Order>()
                .HasIndex(o => o.OrderDate);
        }
    }
}