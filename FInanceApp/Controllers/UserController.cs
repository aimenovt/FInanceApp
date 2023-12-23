using AutoMapper;
using FInanceApp.Dtos.User;
using FInanceApp.Interfaces;
using FInanceApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace FInanceApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserDto userToRegister)
        {
            var response = await _userRepository.Register(userToRegister, userToRegister.Password);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserDto userToLogin)
        {
            var response = await _userRepository.Login(userToLogin.Username, userToLogin.Password);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet("get-users")]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await _userRepository.GetUsers());
        }

        [HttpGet("get-user/{userId}")]
        public async Task<IActionResult> GetUser(int userId)
        {
            return Ok(await _userRepository.GetUser(userId));
        }

        [HttpPut("update-user")]
        public async Task<IActionResult> UpdateUser(UpdateUserDto userToUpdate)
        {
            var response = await _userRepository.UpdateUser(userToUpdate);

            if (response.Data == null)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpDelete("delete-user/{userId}")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            var response = await _userRepository.DeleteUser(userId);

            if (response.Data == null)
            {
                return NotFound(response);
            }

            return Ok(response);
        }
    }
}
