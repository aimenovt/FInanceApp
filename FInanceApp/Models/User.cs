namespace FInanceApp.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Balance Balance { get; set; }
        public int BalanceId { get; set; }
        public Country Country { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public List<FinGoal> FinGoals { get; set; }
    }
}
