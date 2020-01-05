using HotPoint.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace HotPoint.Data
{
    public class HotPointDbContext : IdentityDbContext<AppUser>
    {
        public HotPointDbContext(DbContextOptions<HotPointDbContext> options)
            : base(options)
        {
        }        
        public DbSet<Category> FoodCategories { get; set; }

        public DbSet<AppUser> AppUsers { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderProduct> OrderProducts { get; set; }

        public DbSet<OrderStatus> OrderStatuses { get; set; }

        public DbSet<Recipe> Recipes { get; set; }

        public DbSet<IngredientType> IngredientTypes { get; set; }

        public DbSet<Ingredient> Ingredients { get; set; }

        public DbSet<RecipeIngredient> RecipeIngredient { get; set; }

        public DbSet<Supplier> Suppliers { get; set; }

        public DbSet<SupplierType> SupplierTypes { get; set; }

        public DbSet<ProductStatus> ProductStatuses { get; set; }

        public DbSet<Package> Packages { get; set; }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<Category>().HasKey(c => c.Id);
            mb.Entity<Category>().Property(c => c.Id).ValueGeneratedNever();
            mb.Entity<Category>().HasIndex(c => c.Name).IsUnique();
            mb.Entity<Category>().Property(c => c.Name).IsRequired().HasMaxLength(256);
            
            mb.Entity<Product>().HasKey(p => p.Id);
            mb.Entity<Product>().HasOne(p => p.Recipe).WithOne(r => r.Product).HasForeignKey<Product>(p => p.RecipeId).OnDelete(DeleteBehavior.Restrict);
            mb.Entity<Product>().HasOne(p => p.Category).WithMany(c => c.Products).HasForeignKey(p => p.CategoryId).OnDelete(DeleteBehavior.Restrict);
            mb.Entity<Product>().HasOne(p => p.Supplier).WithMany(s => s.Products).HasForeignKey(p => p.SupplierId).OnDelete(DeleteBehavior.Restrict);
            mb.Entity<Product>().HasOne(p => p.Status).WithMany(ps => ps.Products).OnDelete(DeleteBehavior.Restrict);
            mb.Entity<Product>().Property(o => o.Price).HasColumnType("decimal(5,2)").IsRequired();
            mb.Entity<Product>().Property(o => o.AvailableQty).HasColumnType("decimal(5,2)").IsRequired().HasDefaultValue(0);
            mb.Entity<Product>().Property(o => o.PhotoName).HasMaxLength(256);

            mb.Entity<Order>().HasKey(o => o.Id);
            mb.Entity<Order>().HasOne(o => o.Status).WithMany(os => os.Orders).HasForeignKey(s => s.StatusId).IsRequired();
            mb.Entity<Order>().HasOne(o => o.Customer).WithMany(c => c.Orders).HasForeignKey(o => o.CustomerId).OnDelete(DeleteBehavior.SetNull);
            mb.Entity<Order>().Property(o => o.AmountTotal).HasColumnType("decimal(5,2)");

            mb.Entity<OrderProduct>().HasKey(op => new { op.OrderId, op.ProductId, op.PackageId });
            mb.Entity<OrderProduct>().Property(op => op.Count).IsRequired().HasDefaultValue(1);

            mb.Entity<OrderStatus>().HasKey(os => os.Id);
            mb.Entity<OrderStatus>().Property(os => os.Id).ValueGeneratedNever();
            mb.Entity<OrderStatus>().HasIndex(os => os.Description).IsUnique();
            mb.Entity<OrderStatus>().Property(os => os.Description).IsRequired().HasMaxLength(256);

            mb.Entity<Recipe>().HasKey(r => r.Id);
            mb.Entity<Recipe>().HasIndex(r => r.Name).IsUnique();
            mb.Entity<Recipe>().Property(r => r.Name).IsRequired().HasMaxLength(256);

            mb.Entity<Ingredient>().HasKey(i => i.Id);
            mb.Entity<Ingredient>().HasIndex(i => i.Name).IsUnique();
            mb.Entity<Ingredient>().Property(i => i.Name).IsRequired().HasMaxLength(256);

            mb.Entity<IngredientType>().HasKey(it => it.Id);
            mb.Entity<IngredientType>().Property(it => it.Id).ValueGeneratedNever();
            mb.Entity<IngredientType>().HasIndex(it => it.Name).IsUnique();
            mb.Entity<IngredientType>().Property(it => it.Name).IsRequired().HasMaxLength(256);

            mb.Entity<RecipeIngredient>().HasKey(ri => new { ri.RecipeId, ri.IngredientId });

            mb.Entity<Supplier>().HasKey(s => s.Id);
            mb.Entity<Supplier>().HasIndex(s => s.Name).IsUnique();
            mb.Entity<Supplier>().HasOne(s => s.Type).WithMany(st => st.Suppliers).HasForeignKey(s => s.TypeId);
            mb.Entity<Supplier>().Property(s => s.Name).IsRequired().HasMaxLength(256);

            mb.Entity<SupplierType>().HasKey(st => st.Id);
            mb.Entity<SupplierType>().Property(st => st.Id).ValueGeneratedNever();
            mb.Entity<SupplierType>().HasIndex(st => st.Name).IsUnique();
            mb.Entity<SupplierType>().Property(st => st.Name).IsRequired().HasMaxLength(256);

            mb.Entity<ProductStatus>().HasKey(ps => ps.Id);
            mb.Entity<ProductStatus>().Property(ps => ps.Id).ValueGeneratedNever();
            mb.Entity<ProductStatus>().HasIndex(ps => ps.Name).IsUnique();
            mb.Entity<ProductStatus>().Property(ps => ps.Name).IsRequired().HasMaxLength(256);

            mb.Entity<Package>().HasKey(p => p.Id);
            mb.Entity<Package>().Property(p => p.Name).IsRequired().HasMaxLength(256);
            mb.Entity<Package>().Property(p => p.Material).HasMaxLength(256);
            mb.Entity<Package>().Property(p => p.Volume).HasColumnType("decimal(5,2)");

            base.OnModelCreating(mb);
        }
    }
}
