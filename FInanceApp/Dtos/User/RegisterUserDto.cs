using FInanceApp.Models;

namespace FInanceApp.Dtos.User
{
    public class RegisterUserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int countryId { get; set; }
    }
}
