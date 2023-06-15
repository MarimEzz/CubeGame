using CubeGame.DAL.Data.Models;
using CubeGame.DAL.Data.Models.Cart;
using CubeGame.DAL.Data.Models.wishlist;
using CubeGame.Data.Models.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CubeGame.Data.Context
{
    public class ApplicationDBContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            modelBuilder
                .Entity<Product>().HasMany(P => P.Images).WithOne(A => A.Product)
                .OnDelete(DeleteBehavior.ClientCascade);
        

            modelBuilder.Entity<Cart>().HasQueryFilter(C => C.IsActive == true);

            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Wishlist> wishlists { get; set; }
        public DbSet<wishlistItam> WishlistItams { get; set; }
        public DbSet<Order> orders { get; set; }
    }
}
