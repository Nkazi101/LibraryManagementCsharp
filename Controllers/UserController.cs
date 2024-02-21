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

namespace LibrarySystem.Controllers
{
    //[ApiController]
    //[Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        //private readonly SignInManager<User> _signInManager;
        private readonly IUserService _userService;
        //private readonly IHttpContextAccessor _httpContextAccessor;


        //public UserController(IUserRepository userRepository, SignInManager<User> signInManager)
        //{
        //    _userRepository = userRepository;
        //    _signInManager = signInManager;
        //}

        public UserController(IUserRepository userRepository, IUserService userService)
        {
            _userRepository = userRepository;
            _userService = userService;
           
        }

        // Login actions
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {

            User user = await _userService.signIn(model);
            //var session = _httpContextAccessor.HttpContext.Session;

            //session.SetString("loggedInUser", user.ToString());
            //var user = await _userRepository.GetUserByEmail(model.Email);

            //// Await the PasswordSignInAsync method to get the SignInResult
            //var signInResult = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

            // Now access the Succeeded property of the SignInResult
            if (user != null)
            {
                var claims = new List<Claim> {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())

            };
                var claimsIdentity = new ClaimsIdentity(claims, "Login");
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                return RedirectToAction("Index", "Home"); // Redirect to home page on successful login
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
                User saveduser = await _userService.signUp(user);
                //await _signInManager.SignInAsync(user, false);
                return RedirectToAction("Index", "Home"); // Redirect to home page on successful signup
            }
            return View(user);
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