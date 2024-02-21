using System;
using System.Net;
using LibrarySystem.Models;
using Microsoft.EntityFrameworkCore;


namespace LibrarySystem.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly LibraryDBContext _context;

        public UserRepository(LibraryDBContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> GetUserByID(int Id)
        {
            return await _context.Users.Where(u => u.Id == Id).FirstAsync();
        }

        public async Task<User> CreateUser(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> UpdateUser(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return user;
        }
    }

    }
