using System;
using LibrarySystem.Models;

namespace LibrarySystem.Repository
{
	public interface IUserRepository
	{
        Task<User> GetUserByEmail(string email);
        Task<User> CreateUser(User user);
        Task<User> GetUserByID(int Id);
        Task<User> UpdateUser(User user);
    }
}

