
using Microsoft.EntityFrameworkCore;


namespace Mid_Junior_backend
{
    public class vending_machine : DbContext
    {
        private readonly vending_machine _cont;
        public DbSet<User> Users { get; set; }
        public DbSet<product> Products { get; set; }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlServer("your_connection_string");
            }

        // CreateUser example
        
        public User CreateUser(User user)
        {
            _cont.Users.Add(user);
            _cont.SaveChanges();
            return user;
        }
       

    }

    public class ProductRepository
    {
        private readonly vending_machine _context;

        public ProductRepository(vending_machine context)
        {
            _context = context;
        }

        public product CreateProduct(product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return product;
        }

        // ... other CRUD methods
    }
}
