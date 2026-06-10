using EmployeeManager.Application.Interfaces;
using EmployeeManager.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EmployeeManager.Api.Controllers
{
  
    [ApiController]
    [Route("api/users")] 
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var currentUserId = int.Parse(User.FindFirst("userId")!.Value);
            var currentRole = User.FindFirst(ClaimTypes.Role)?.Value;

            if (currentRole != "admin" && currentUserId != id)
                return Forbid();

            var user = await _userService.GetByIdAsync(id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> Create(UserDTO dto)
        {
            var createdUser = await _userService.CreateAsync(dto);
            if (createdUser.Success) {
                return CreatedAtAction(nameof(GetById), new { id = createdUser.userDto.Id }, createdUser.userDto);

            }
            else
            {
                return BadRequest(createdUser.Message);

            }
        }

        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] RegisterDto dto)
        {
            if (dto == null)
                return BadRequest("User data is required.");

            var updated = await _userService.UpdateAsync(id, dto);

            if (!updated)
                return NotFound();

            return NoContent();
        }
        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {

            var user = await _userService.GetByIdAsync(id);
            if (user == null)
                return NotFound();

            await _userService.DeleteAsync(id);


            return NoContent();
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var result = await _userService.LoginAsync(dto);

            if (!result.Success)
                return Unauthorized(result);

            return Ok(result);
        }
        [Authorize]
        [HttpPut("{id}/password")]
        public async Task<IActionResult> UpdatePassword(int id, [FromBody] PasswordUpdateDto dto)
           {
            if (dto == null)
                return BadRequest("User data is required.");
            var userId = int.Parse(User.FindFirst("userId")!.Value);
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;

            if (userId != id && userRole != "admin")
                return Forbid("You can only update your own password.");

            var updated = await _userService.UpdatePassword(id, dto);

               if (!updated)
                   return NotFound();

               return NoContent();
           }
        [Authorize(Roles = "admin")]
        [HttpPut("{id}/role")]
        public async Task<IActionResult> UpdateRole(int id, [FromBody] RoleUpdateDto dto)
        {
            if (dto == null)
                return BadRequest("User data is required.");

            var updated = await _userService.UpdateRole(id, dto);

            if (!updated)
                return NotFound();

            return NoContent();
        }
       
    }
}
