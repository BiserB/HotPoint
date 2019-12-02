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

        public DbSet<AppUser> Customers { get; set; }

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

        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<Category>().HasKey(c => c.Id);
            
            mb.Entity<Product>().HasKey(p => p.Id);
            mb.Entity<Product>().HasOne(p => p.Recipe).WithOne(r => r.Product).HasForeignKey<Product>(p => p.RecipeId);
            mb.Entity<Product>().HasOne(p => p.Category).WithMany(c => c.Products).HasForeignKey(p => p.CategoryId);
            mb.Entity<Product>().HasOne(p => p.Supplier).WithMany(s => s.Products).HasForeignKey(p => p.SupplierId);
            mb.Entity<Product>().Property(o => o.Price).HasColumnType("decimal(5,2)");

            mb.Entity<Order>().HasKey(o => o.Id);
            mb.Entity<Order>().HasOne(o => o.Status).WithMany(os => os.Orders).HasForeignKey(s => s.StatusId);
            mb.Entity<Order>().HasOne(o => o.Customer).WithMany(c => c.Orders).HasForeignKey(o => o.CustomerId);
            mb.Entity<Order>().Property(o => o.CustomerId).IsRequired();
            mb.Entity<Order>().Property(o => o.AmountTotal).HasColumnType("decimal(5,2)");

            mb.Entity<OrderProduct>().HasKey(op => new { op.OrderId, op.ProductId });

            mb.Entity<OrderStatus>().HasKey(os => os.Id);

            mb.Entity<Recipe>().HasKey(r => r.Id);

            mb.Entity<Ingredient>().HasKey(i => i.Id);

            mb.Entity<IngredientType>().HasKey(it => it.Id);

            mb.Entity<RecipeIngredient>().HasKey(ri => new { ri.RecipeId, ri.IngredientId });

            mb.Entity<Supplier>().HasKey(s => s.Id);
            mb.Entity<Supplier>().HasOne(s => s.Type).WithMany(st => st.Suppliers).HasForeignKey(s => s.TypeId);

            mb.Entity<SupplierType>().HasKey(st => st.Id);

            base.OnModelCreating(mb);
        }
    }
}
