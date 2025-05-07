namespace GuessingGame.API.Services
{
    public class RandomNumberGenerator : IRandomNumberGenerator
    {
        private readonly Random _random = new();
        public int Next() => _random.Next(0, 10);
    }
}
