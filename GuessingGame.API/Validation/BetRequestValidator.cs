namespace GuessingGame.API.Validation
{
    using GuessingGame.API.Models;

    public class BetRequestValidator
    {
        public ValidationResult Validate(BetRequest request)
        {
            var result = new ValidationResult();

            if (request.Number < 0 || request.Number > 9)
                result.Errors.Add("Number must be between 0 and 9.");

            if (request.Points <= 0)
                result.Errors.Add("Points must be positive.");

            return result;
        }
    }
}
