namespace FInanceApp.Models
{
    public class Balance
    {
        public int Id { get; set; }
        public decimal FundsKZT { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
    }
}
