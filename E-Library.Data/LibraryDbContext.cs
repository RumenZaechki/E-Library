using E_Library.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Library.Data
{
    public class LibraryDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<BookCategory> BookCategories { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options)
            : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=aspnet-E_Library-13B03D18-5F75-4004-A318-3D5BB8B0A953;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookCategory>(bc =>
            {
                bc.HasKey(k => new { k.CategoryId, k.BookId });
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}