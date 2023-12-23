using AutoMapper;
using FInanceApp.Data;
using FInanceApp.Dtos.User;
using FInanceApp.Interfaces;
using FInanceApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace FInanceApp.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public UserRepository(DataContext context, IConfiguration configuration, IMapper mapper)
        {
            _context = context;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<int>> Register(RegisterUserDto userToRegister, string password)
        {
            var user = _mapper.Map<User>(userToRegister);
            user.Country = _context.Countries.Where(c => c.Id == userToRegister.countryId).FirstOrDefault();

            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var balance = new Balance
            {
                FundsKZT = 0,
                UserId = user.Id
            };

            _context.Balances.Add(balance);
            await _context.SaveChangesAsync();

            user.BalanceId = balance.Id;
            balance.UserId = user.Id;

            await _context.SaveChangesAsync();

            var response = new ServiceResponse<int>();
            response.Data = user.Id;

            return response;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<ServiceResponse<string>> Login(string username, string password)
        {
            var response = new ServiceResponse<string>();

            var user = await _context.Users.Where(u => u.Username == username).FirstOrDefaultAsync();

            if (user == null)
            {
                response.Success = false;
                response.Message = "User not found";
            }

            else if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                response.Success = false;
                response.Message = "Wrong password";
            }

            else
            {
                response.Data = CreateToken(user);
            }

            return response;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
            };

            // Assign roles based on user ID or any other criteria
            if (user.Id == 32)
            {
                // User with ID 32 gets the "Admin" role
                claims.Add(new Claim(ClaimTypes.Role, "Admin"));
            }

            else
            {
                // Other users get the "Standard" role
                claims.Add(new Claim(ClaimTypes.Role, "Standard"));
            }

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token); //Token
        }

        public async Task<ServiceResponse<List<GetUserDto>>> DeleteUser(int id)
        {
            var userToRemove = _context.Users.Where(u => u.Id == id).FirstOrDefault();

            _context.Users.Remove(userToRemove);
            await _context.SaveChangesAsync();

            var response = new ServiceResponse<List<GetUserDto>>();
            response.Data = _mapper.Map<List<GetUserDto>>(_context.Users.ToList());

            return response;
        }

        public async Task<ServiceResponse<GetUserDto>> GetUser(int userId)
        {
            var user = await _context.Users.Where(u => u.Id == userId).FirstOrDefaultAsync();

            var response = new ServiceResponse<GetUserDto>();
            response.Data = _mapper.Map<GetUserDto>(user);

            return response;
        }

        public async Task<ServiceResponse<List<GetUserDto>>> GetUsers()
        {
            var users = await _context.Users.OrderBy(u => u.Id).ToListAsync();

            var response = new ServiceResponse<List<GetUserDto>>();
            response.Data = _mapper.Map<List<GetUserDto>>(users);

            return response;
        }

        public async Task<ServiceResponse<GetUserDto>> UpdateUser(UpdateUserDto updatedUser)
        {
            var userToUpdate = _context.Users.Where(u => u.Id == updatedUser.Id).FirstOrDefault();

            _mapper.Map(updatedUser, userToUpdate);

            _context.Users.Update(userToUpdate);
            await _context.SaveChangesAsync();

            var response = new ServiceResponse<GetUserDto>();
            response.Data = _mapper.Map<GetUserDto>(userToUpdate);

            return response;
        }

        public async Task<bool> UserExists(int userId)
        {
            var userExistsStatus = await _context.Users.AnyAsync(u => u.Id == userId);

            return userExistsStatus;
        }
    }
}
