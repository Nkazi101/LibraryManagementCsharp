using System;
using LibrarySystem.Services;
using LibrarySystem.Models;
namespace LibrarySystem.Services
{
	public interface IUserService
	{

		Task<User> signIn(LoginViewModel model);
		Task<User> signUp(User user);
		Task<User> GetUserById(int Id);
		Task<User> checkforLateFees(int userId);
		Task<User> BorrowBook(int userId, int bookId);

    }
}

