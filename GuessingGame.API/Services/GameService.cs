namespace GuessingGame.API.Services
{
    using GuessingGame.API.Models;
    using GuessingGame.API.Exceptions;
    using GuessingGame.API.Validation;

    public class GameService : IGameService
    {
        private readonly IPlayerService _playerService;
        private readonly BetRequestValidator _validator = new();
        private readonly IRandomNumberGenerator _rng;

        // Inject IRandomNumberGenerator in the constructor
        public GameService(IPlayerService playerService, IRandomNumberGenerator rng)
        {
            _playerService = playerService;
            _rng = rng;
        }

        public BetResponse PlaceBet(Guid playerId, BetRequest request)
        {
            var player = _playerService.GetOrCreate(playerId);

            // Validate the bet request
            var validation = _validator.Validate(request);
            if (!validation.IsValid)
                throw new ArgumentException(string.Join("; ", validation.Errors));

            // Check for insufficient funds
            if (request.Points > player.Balance)
            {
                throw new InsufficientFundsException("Player does not have enough funds.");
            }

            // Business logic: Draw a number and determine if the player wins
            int drawn = _rng.Next();
            bool win = drawn == request.Number;
            int delta = win ? request.Points * 9 : -request.Points;

            player.Balance += delta;
            _playerService.Update(player);

            // Return bet result
            return new BetResponse
            {
                Account = player.Balance,
                Status = win ? "won" : "lost",
                Points = win ? $"+{delta}" : delta.ToString()
            };
        }
    }
}
