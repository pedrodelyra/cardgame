using Gameplay.Core.Cards;

namespace Gameplay.Core.Actions
{
    public class DeployCardAction : GameAction<DeployCardActionData>
    {
        public override bool Validate()
        {
            return true;
        }

        public override void Execute()
        {
            var arena = Data.Arena;
            var cardType = Data.CardType;
            arena.DeployCard(cardType);
        }
    }

    public struct DeployCardActionData
    {
        public Arena Arena;

        public CardType CardType;
    }
}
