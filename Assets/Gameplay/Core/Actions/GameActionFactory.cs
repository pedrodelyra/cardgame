
using Gameplay.Core.Cards;

namespace Gameplay.Core.Actions
{
    public class GameActionFactory
    {
        Deployer Deployer { get; }

        public GameActionFactory(Deployer deployer)
        {
            Deployer = deployer;
        }

        public DeployCardAction CreateDeployCardAction(CardType cardType, Team team, int laneIdx)
        {
            return CreateGameAction<DeployCardAction, DeployCardActionData>(new DeployCardActionData
            {
                Deployer = Deployer,
                CardType = cardType,
                Team = team,
                LaneIdx = laneIdx,
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
