using Gameplay.Behaviours.Interfaces;
using Gameplay.Behaviours.UI;
using Gameplay.Core.Cards;
using UnityEngine.Assertions;

namespace Gameplay.Core
{
    public class Deployer
    {
        GameObjectFactory GameObjectFactory { get; }

        Arena Arena { get; }

        GameplayHUD GameplayHUD { get; }

        public Deployer(GameObjectFactory gameObjectFactory, Arena arena, GameplayHUD gameplayHUD)
        {
            GameObjectFactory = gameObjectFactory;
            Arena = arena;
            GameplayHUD = gameplayHUD;
        }

        public Entity DeployCard(CardType cardType, Team team, int laneIdx)
        {
            var card = GameObjectFactory.CreateCard(cardType, team);

            var lane = Arena.Lanes[laneIdx];
            lane.AddEntity(card, team);

            var damageable = card.GetComponent<IDamageable>();
            if (damageable != null)
            {
                GameplayHUD.CreateHealthBar(damageable, team, card.transform);
            }

            return card;
        }
    }
}
