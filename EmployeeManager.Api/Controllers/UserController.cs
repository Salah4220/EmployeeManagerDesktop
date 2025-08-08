using EmployeeManager.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace EmployeeManager.Api.Controllers
{
   
    [ApiController]
    [Route("api/users")] // ✅ route explicite, plus de problème de casse
    public class UserController : ControllerBase
    {

        private Util _util = new Util();
        private readonly AppDbContext _context;
        public UserController(AppDbContext context)
        {
            _context = context;
        }





        // GET: api/users
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _context.Users.ToListAsync();
            return Ok(users);
        }
        // POST: api/users/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.UserName) || string.IsNullOrWhiteSpace(dto.Password))
                return BadRequest("Invalid user data.");

            // Vérifier si l'utilisateur existe déjà
            if (await _context.Users.AnyAsync(u => u.UserName == dto.UserName))
                return BadRequest("Username already used.");

            // Hash avec BCrypt
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var user = new User
            {
                UserName = dto.UserName,
                PassWordHash = passwordHash
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
        }
        // GET: api/users/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }
        

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == dto.UserName);


            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PassWordHash))
                return BadRequest("Wrong username or password.");

            // ⚠️ Ici, on ne retourne pas encore le JWT (prochaine étape)
            return Ok(new { message = "Login successful", userId = user.Id });
        }
    }
}
