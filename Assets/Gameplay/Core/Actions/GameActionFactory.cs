
using Gameplay.Core.Cards;

namespace Gameplay.Core.Actions
{
    public class GameActionFactory
    {
        Arena Arena { get; }

        public GameActionFactory(Arena arena)
        {
            Arena = arena;
        }

        public DeployCardAction CreateDeployCardAction(CardType cardType)
        {
            return CreateGameAction<DeployCardAction, DeployCardActionData>(new DeployCardActionData
            {
                Arena = Arena,
                CardType = cardType,
            });
        }

        T CreateGameAction<T, U>(U data) where T : GameAction<U>, new()
        {
            var gameAction = new T();
            gameAction.Setup(data);
            return gameAction;
        }
    }
}
