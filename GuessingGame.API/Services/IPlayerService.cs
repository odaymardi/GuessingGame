namespace GuessingGame.API.Services
{
    using GuessingGame.API.Models;

    public interface IPlayerService
    {
        Player GetOrCreate(Guid id);
        void Update(Player player);
    }
}
