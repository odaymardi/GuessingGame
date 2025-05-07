namespace GuessingGame.Tests.Services
{
    using GuessingGame.API.Models;
    using GuessingGame.API.Services;
    using GuessingGame.API.Exceptions;
    using Xunit;
    using System;

    public class GameServiceTests
    {
        private readonly IPlayerService _playerService;
        private readonly IRandomNumberGenerator _rng;
        private readonly GameService _gameService;

        public GameServiceTests()
        {
            _playerService = new PlayerService();
            _rng = new FixedRandomNumberGenerator(3); // Always returns 3
            _gameService = new GameService(_playerService, _rng);
        }

        [Fact]
        public void PlaceBet_ShouldReturnWon_WhenBetIsSuccessful()
        {
            var playerId = Guid.NewGuid();
            var betRequest = new BetRequest { Points = 100, Number = 3 };

            var player = _playerService.GetOrCreate(playerId);
            player.Balance = 1000;

            var result = _gameService.PlaceBet(playerId, betRequest);

            Assert.Equal(player.Balance, result.Account);
            Assert.Equal("won", result.Status);
            Assert.Equal("+900", result.Points);
        }

        [Fact]
        public void PlaceBet_ShouldThrowInsufficientFundsException_WhenBalanceIsLow()
        {
            var playerId = Guid.NewGuid();
            var betRequest = new BetRequest { Points = 5000, Number = 3 };

            var player = _playerService.GetOrCreate(playerId);
            player.Balance = 1000;

            var exception = Assert.Throws<InsufficientFundsException>(() => _gameService.PlaceBet(playerId, betRequest));
            Assert.Equal("Player does not have enough funds.", exception.Message);
        }

        private class FixedRandomNumberGenerator : IRandomNumberGenerator
        {
            private readonly int _fixed;
            public FixedRandomNumberGenerator(int number) => _fixed = number;
            public int Next() => _fixed;
        }
    }
}
