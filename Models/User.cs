using System;
using Microsoft.AspNetCore.Identity;

namespace LibrarySystem.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; } // Rename the field to PasswordHash
        public byte[] PasswordSalt { get; set; } // Add the PasswordSalt field
        public double lateFees { get; set; } = 0;
        public bool isAdmin { get; set; }

        //one to many
        public List<Book> books;

        public User()
        {
            books = new List<Book>();
        }

    }
}
