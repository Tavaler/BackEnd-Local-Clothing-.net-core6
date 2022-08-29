using Microsoft.EntityFrameworkCore;

namespace Local.Models.Data
{
    public class LocaldbContext : DbContext
    {
        public LocaldbContext(DbContextOptions<LocaldbContext> options):base(options)
        { 
        
        }
        public DbSet<User> Users { get; set; }
        public DbSet<SecurityUser> SecurityUsers { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImg> ProductImgs { get; set; }
        public DbSet<Category> Categories { get; set; }



        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Transportation> Transportations { get; set; }


        public DbSet<AddStock> AddStocks { get; set; }
        public DbSet<ManageStock> ManageStocks { get; set; }
        public DbSet<ManageItem> ManageItems { get; set; }

    }
}
