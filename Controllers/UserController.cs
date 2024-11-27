using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using TodoList1.Data;
using TodoList1.Models;
using Microsoft.EntityFrameworkCore;

namespace TodoList1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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

        //
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

            return Ok("Login successful");
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult SignUP()
        {
            return View();
        }
    }
}
