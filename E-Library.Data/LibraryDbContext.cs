using E_Library.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace E_Library.Data
{
    public class LibraryDbContext : IdentityDbContext<User>
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<BookCart> BookCarts { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookCart>()
                .HasKey(k => new { k.BookId, k.CartId });

            modelBuilder.Entity<User>()
                .HasOne(c => c.Cart)
                .WithOne(u => u.User)
                .HasForeignKey<Cart>(c => c.UserId);

            base.OnModelCreating(modelBuilder);
        }
    }
}