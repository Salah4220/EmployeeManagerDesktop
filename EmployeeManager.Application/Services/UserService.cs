using BCrypt.Net;
using EmployeeManager.Application.Interfaces;
using EmployeeManager.Domain.Entities;
using EmployeeManager.Shared;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.Application.Services
{
    public class UserService : IUserService
    {

   
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;

        public UserService( IUserRepository userRepository, IJwtService jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
        }
        public async Task<UserDTO> CreateAsync(RegisterDto dto)
        {
            var user = new User
            {
                UserName = dto.UserName,
                PassWordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Role = dto.Role
            };
            
            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();

            return new UserDTO  
            {
                UserName = user.UserName,
                Password = user.PassWordHash,
                Role = user.Role
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
                return false;

            await _userRepository.DeleteAsync(user);
            await _userRepository.SaveChangesAsync();

            return true;
        }

        public async Task<List<UserDTO>> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();

            return users.Select(t => new UserDTO
            {
                Password = t.PassWordHash,
                Role = t.Role,
                UserName = t.UserName
            }).ToList();
        }

        public async Task<UserDTO?> GetByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
                return null;

            return new UserDTO
            {
                Password = user.PassWordHash,
                Role = user.Role,
                UserName = user.UserName

            };
        }

        public async Task<bool> UpdateAsync(int id, RegisterDto dto)
        {
            var existingUser = await _userRepository.GetByIdAsync(id);
            if (existingUser == null)
                return false;

            existingUser.PassWordHash = dto.Password;
            existingUser.UserName = dto.UserName;
            existingUser.Role = dto.Role;
            await _userRepository.SaveChangesAsync();
            return true;
        }
        public async Task<LoginResult> LoginAsync(LoginDto dto)
        {
            var user = await _userRepository.GetByUserNameAsync(dto.UserName);

            if (user == null)
            {
                return new LoginResult
                {
                    Success = false,
                    Message = "Identifiants incorrects",
                    Token = null
                };
            }

            var passwordIsValid = BCrypt.Net.BCrypt.Verify(dto.Password, user.PassWordHash);

            if (!passwordIsValid)
            {
                return new LoginResult
                {
                    Success = false,
                    Message = "Identifiants incorrects",
                    Token = null
                };
            }

            var tokenString = _jwtService.GenerateToken(user);

            return new LoginResult
            {
                Success = true,
                Message = "Connexion réussie",
                Token = tokenString,
                Role = user.Role
            };
        }
    }
}
