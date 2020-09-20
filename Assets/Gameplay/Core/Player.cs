using Gameplay.Behaviours;

namespace Gameplay.Core
{
    public interface IPlayer : IEntity
    {
        PlayerDeck Deck { get; }

        PlayerHand Hand { get; }

        void Setup(PlayerDeck deck);
    }

    public class Player : Behaviour, IPlayer
    {
        public PlayerDeck Deck { get; private set; }

        public PlayerHand Hand { get; } = new PlayerHand();

        public Team Team => Entity.Team;

        public void Setup(PlayerDeck deck)
        {
            Deck = deck;
        }
    }
}
