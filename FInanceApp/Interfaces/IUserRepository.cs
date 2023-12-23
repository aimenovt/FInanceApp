using FInanceApp.Dtos.User;
using FInanceApp.Models;

namespace FInanceApp.Interfaces
{
    public interface IUserRepository
    {
        Task<ServiceResponse<List<GetUserDto>>> GetUsers();
        Task<ServiceResponse<GetUserDto>> GetUser(int userId);
        Task<bool> UserExists(int userId);
        Task<ServiceResponse<int>> Register(RegisterUserDto userToRegister, string password);
        Task<ServiceResponse<string>> Login(string username, string password);
        Task<ServiceResponse<GetUserDto>> UpdateUser(UpdateUserDto updatedUser);
        Task<ServiceResponse<List<GetUserDto>>> DeleteUser(int id);
    }
}
