using System;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Models
{
    //track and manage entities/models

    //enable you perform CRUD operations on models
    public class LibraryDBContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookGenre> BookGenres { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<BookCart> BookCarts { get; set; }
        public DbSet<Cart> Carts { get; set; }

        public LibraryDBContext(DbContextOptions<LibraryDBContext> options)
            : base(options)
        {
        }
    }
}
// Many-to-many relationship table


//protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            optionsBuilder.UseSqlServer("Server=localhost<default>;Database=librarytest;User Id=SA;Password=claimacademy101");
//        }
//    }
//}

