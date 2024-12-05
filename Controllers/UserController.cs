using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using TodoList1.Data;
using TodoList1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Session;

namespace TodoList1.Controllers;//standard method

    [ApiController]
    [Route("[controller]/[action]")]
    public class UserController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly IPasswordHasher<UserModel> _passwordHasher;



        // Constructor to initialize ApplicationDbContext
        public UserController(ApplicationDbContext context, IPasswordHasher<UserModel> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;   
           
        }

        
        [HttpPost("Signup")]
        public async Task<IActionResult> PostUser( UserModel user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            // Check for required fields
            if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.PasswordHash))
            {
                return BadRequest("Email and password are required.");
            }

            // Check if the email already exists in the database
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
            if (existingUser != null)
            {
                return Conflict("Email already exists. Please use another email ID.");
            }

            // Hash the password before storing it
            user.PasswordHash = _passwordHasher.HashPassword(user, user.PasswordHash);

            // Add the user to the database
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return Ok("User posted successfully");
        }


        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest("Email and password are required.");
            }

            // Validate the user credentials
            var user = _context.Users.SingleOrDefault(u => u.Email == request.Email);
            if (user == null)
            {
                return Unauthorized("Invalid email or password.");
            }

            // Verify the password
            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                return Unauthorized("Invalid email or password.");
            }

            // Store user information in session
            HttpContext.Session.SetInt32("UserId", user.UserId);
            HttpContext.Session.SetString("Username", user.Username);

            return Ok(new
            {
                Message = "Login successful",
                Username = user.Username // You can return additional data if needed
            });
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Login()
        {
            return View();
            
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)] // Hides this method from Swagger API documentation
        public IActionResult SignUp()
        {
            return View();
        }

        
        public IActionResult Logout()
        {
        HttpContext.Session.Clear();
        HttpContext.Response.Cookies.Delete(".AspNetCore.Session");
        return RedirectToAction("Login", "User"); // Redirect to the Login page
        }



    }
