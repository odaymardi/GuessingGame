namespace GuessingGame.API.Models
{
    public class BetResponse
    {
        public int Account { get; set; }
        public string Status { get; set; }
        public string Points { get; set; } // Includes prefix + or -
    }
}
