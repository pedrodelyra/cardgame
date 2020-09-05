namespace Gameplay.Core.Actions
{
    public class DeployCardAction : IGameAction
    {
        Arena Arena { get; }

        CardType CardType { get; }

        public DeployCardAction(CardType cardType, Arena arena)
        {
            Arena = arena;
            CardType = cardType;
        }

        public void Execute()
        {
            Arena.DeployCard(CardType);
        }
    }
}
