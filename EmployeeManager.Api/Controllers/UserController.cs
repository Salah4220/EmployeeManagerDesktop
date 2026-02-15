using EmployeeManager.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace EmployeeManager.Api.Controllers
{
  
    [ApiController]
    [Route("api/users")] 
    public class UserController : ControllerBase
    {
       /* private readonly IConfiguration _configuration;
        public UserController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        private Util _util = new Util();
        private readonly AppDbContext _context;


        // GET: api/users
        [Authorize(Roles = "admin")]
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
                return BadRequest(new { Success = false, Message = "Invalid user data." });

            if (await _context.Users.AnyAsync(u => u.UserName == dto.UserName))
                return BadRequest(new { Success = false, Message = "Username already used." });

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var user = new User
            {
                UserName = dto.UserName,
                PassWordHash = passwordHash,
                Role = dto.Role
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var result = new
            {
                Success = true,
                Message = "Utilisateur créé avec succès"
            };

            return Ok(result);
        }

        // GET: api/users/{id}
        [Authorize(Roles = "admin")]
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
            {
                var result2 = new LoginResult
                {
                    Success = false,
                    Message = "Connexion échouée ❌",
                    Token = null
                };

                return Unauthorized(result2);
            }

            //  Lire les paramètres JWT depuis la config
                    var jwtSettings = _configuration.GetSection("Jwt");
                    var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]);

                    var claims = new[]
                    {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("userId", user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var creds = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(double.Parse(jwtSettings["ExpireMinutes"])),
                signingCredentials: creds
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            var result = new LoginResult
            {
                Success = true,
                Message = "Connexion réussie ✅",
                Token = tokenString
            };

            return Ok(result);
        }

        // DELETE: api/users/{id}
        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return NotFound();

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // ✅ Mise à jour du rôle d’un utilisateur
        [Authorize(Roles = "admin")]
        [HttpPut("{id}/role")]
        public async Task<IActionResult> UpdateUserRole(int id, [FromBody] UpdateRoleDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Role))
                return BadRequest(new { Success = false, Message = "Le rôle ne peut pas être vide." });

            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return NotFound(new { Success = false, Message = "Utilisateur non trouvé." });

            // Vérifie que le rôle est valide
            if (dto.Role != "admin" && dto.Role != "user")
                return BadRequest(new { Success = false, Message = "Rôle invalide. Valeurs possibles : 'admin' ou 'user'." });

            // Mise à jour du rôle
            user.Role = dto.Role;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                Success = true,
                Message = $"Le rôle de {user.UserName} a été mis à jour en '{user.Role}'."
            });
        }
       */
    }
}
