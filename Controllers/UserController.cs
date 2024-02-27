using Microsoft.AspNetCore.Mvc;
using LibrarySystem.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using LibrarySystem.Repository;
using LibrarySystem.Services;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Newtonsoft.Json;


namespace LibrarySystem.Controllers
{
    //[ApiController]
    //[Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        //private readonly SignInManager<User> _signInManager;
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserController(IUserRepository userRepository, IUserService userService, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
           
        }

        private ISession Session => _httpContextAccessor.HttpContext.Session;

        // Login actions
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {

            User user = await _userService.signIn(model);


            if (user != null)
            {
                var claims = new List<Claim> {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())

            };
                var claimsIdentity = new ClaimsIdentity(claims, "Login");
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));



                var loggedinUser = JsonConvert.SerializeObject(user);

                HttpContext.Session.SetString("currentUser", loggedinUser);

                return RedirectToAction("Index", "Home", user); // Redirect to home page on successful login
            }

            ModelState.AddModelError("", "Invalid login credentials.");
            return View(model);
        }


        // Signup actions
        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Signup(User user)
        {
            if (ModelState.IsValid)
            {
                User savedUser = await _userService.signUp(user);
                if (savedUser != null)
                {
                    // Optionally sign in the user after successful signup
                    // await _signInManager.SignInAsync(savedUser, false);
                    return RedirectToAction("Login", "User"); // Redirect to home page on successful signup
                }
                else
                {
                    // Handle error if user signup fails
                    ModelState.AddModelError(string.Empty, "Failed to signup user.");
                    return View(user);
                }
            }
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            // Sign out the user
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // Clear the session
            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Home"); // Redirect to home page after logout
        }

        [HttpGet]
        public async Task<IActionResult> UserProfile()
        {
            // Get the current user's profile
            // Assuming you have a method in your IUserService to retrieve user profile
            //User user = await _userService.GetCurrentUser();

            var currentUserJson = HttpContext.Session.GetString("currentUser");
            User currentUser = null;

            if (currentUserJson != null)
            {
                currentUser = JsonConvert.DeserializeObject<User>(currentUserJson);
            }

            if (currentUser == null)
            {
                return NotFound(); // User not found
            }
            return View(currentUser);
        }

        //public async Task<IActionResult> BorrowedBooks(int Id)
        //{
        //    User foundUser = await _userService.GetUserById(Id);
        //    List<Book> books = foundUser.books;
        //    User user = await _userService.checkforlateFees(books, foundUser);


        //    return View(books);


        //}
    }
}