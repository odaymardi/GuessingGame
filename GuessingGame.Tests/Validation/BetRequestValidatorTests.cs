namespace GuessingGame.Tests.Validation
{
    using GuessingGame.API.Models;
    using GuessingGame.API.Validation;
    using Xunit;

    public class BetRequestValidatorTests
    {
        private readonly BetRequestValidator _validator = new();

        [Theory]
        [InlineData(-1, 100, "Number must be between 0 and 9.")]
        [InlineData(10, 100, "Number must be between 0 and 9.")]
        [InlineData(5, -100, "Points must be positive.")]
        [InlineData(5, 0, "Points must be positive.")]
        public void InvalidRequests_ShouldReturnExpectedErrors(int number, int points, string expectedError)
        {
            var request = new BetRequest { Number = number, Points = points };
            var result = _validator.Validate(request);

            Assert.False(result.IsValid);
            Assert.Contains(expectedError, result.Errors);
        }

        [Fact]
        public void ValidRequest_ShouldPassValidation()
        {
            var request = new BetRequest { Number = 5, Points = 500 };
            var result = _validator.Validate(request);

            Assert.True(result.IsValid);
            Assert.Empty(result.Errors);
        }
    }
}
