using System;
using LibrarySystem.Models;
using LibrarySystem.Repository;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;


namespace LibrarySystem.Services
{
	public class UserService : IUserService
	{
		//dependency injection
        private readonly IUserRepository _userRepository;
        private readonly IBookRepository _bookRepository;
       



        public UserService(IUserRepository userRepository, IBookRepository bookRepository)
		{
			_userRepository = userRepository;
            _bookRepository = bookRepository;
            
		}


		public async Task<User> signIn(LoginViewModel model)
		{
            User foundUser = await _userRepository.GetUserByEmail(model.Email);

            if (foundUser == null)
            {
                throw new Exception("User not found");
            }

            // Hash the password using the stored salt and verify against the stored hashed password
            byte[] hashedPassword = KeyDerivation.Pbkdf2(
                password: model.PasswordHash,
                salt: foundUser.PasswordSalt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8); // Generate a 256-bit hash

            string hashedPasswordString = Convert.ToBase64String(hashedPassword);

            if (!foundUser.PasswordHash.Equals(hashedPasswordString))
            {
                throw new Exception("Wrong password");
            }

            return foundUser;
        }

        public async Task<User> signUp(User user)
        {
            // Ensure the password is not null before hashing
            if (string.IsNullOrWhiteSpace(user.PasswordHash))
            {
                throw new ArgumentException("Password cannot be null or empty.", nameof(user.PasswordHash));
            }

            // Generate a strong salt using a cryptographically secure random number generator
            byte[] salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            // Hash the password using PBKDF2 with the generated salt
            byte[] hashedPassword = KeyDerivation.Pbkdf2(
                password: user.PasswordHash,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8); // Generate a 256-bit hash


            // Set the hashed password and salt in the user object
            user.PasswordHash = Convert.ToBase64String(hashedPassword);
            user.PasswordSalt = salt; // Assuming a field for salt

            User saveduser = await _userRepository.CreateUser(user);
            return saveduser;
        }

        public async Task<User> checkforLateFees(int userId)
        {
            User user = await _userRepository.GetUserByID(userId);

            if (user != null)
            {
                // Calculate late fees for each book borrowed by the user
                foreach (var book in user.books)
                {
                    var daysLate = (DateTime.Now - book.DateBorrowed).TotalDays;
                    if (daysLate > 120)
                    {
                        // Calculate late fees based on your policy
                        double lateFee = (daysLate - 120) * 0.50; // Example late fee calculation

                        // Add late fees to the user's account
                        user.lateFees += lateFee;
                    }
                }

                // Update the user's account in the repository
                _userRepository.UpdateUser(user);
            }
            return user;
        }

        public async Task<User> BorrowBook(int userId, int bookId)
        {
            User user = await _userRepository.GetUserByID(userId);
            Book book = await _bookRepository.GetBookById(bookId);

            if (book == null || user == null)
            {
                throw new InvalidOperationException("User or book not found.");
            }

            if (!book.Available)
            {
                throw new InvalidOperationException("The book is not available for borrowing.");
            }

            // Update book and user entities
            book.Available = false;
            book.DateBorrowed = DateTime.Now;
            user.books.Add(book);

            // Save changes
            await _bookRepository.UpdateBook(book);
            await _userRepository.UpdateUser(user);

            return user;
        }

        public async Task<User> GetUserById(int Id)
        {
            return await _userRepository.GetUserByID(Id);
        }
    }
}

