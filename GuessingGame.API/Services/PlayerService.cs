namespace GuessingGame.API.Services
{
    using GuessingGame.API.Models;

    public class PlayerService : IPlayerService
    {
        private static readonly Dictionary<Guid, Player> _players = new();

        public Player GetOrCreate(Guid id)
        {
            if (!_players.ContainsKey(id))
                _players[id] = new Player { Id = id };
            return _players[id];
        }

        public void Update(Player player)
        {
            _players[player.Id] = player;
        }
    }
}
