using GuessingGame.API.Models;

namespace GuessingGame.API.Services
{
public interface IGameService
{
    BetResponse PlaceBet(Guid playerId, BetRequest request);
}

}