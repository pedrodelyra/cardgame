using Gameplay.Behaviours;
using Gameplay.Core.Cards;
using UnityEngine;
using Utils.Extensions;

namespace Gameplay.Core
{
    public class Deployer
    {
        GameObjectFactory GameObjectFactory { get; }

        Arena Arena { get; }

        public Deployer(GameObjectFactory gameObjectFactory, Arena arena)
        {
            GameObjectFactory = gameObjectFactory;
            Arena = arena;
        }

        public Entity DeployCard(CardType cardType, Team team, int laneIdx)
        {
            var card = GameObjectFactory.CreateCard(cardType);
            card.Team = team;

            var lane = Arena.Lanes[laneIdx];
            lane.AddEntity(card, team);

            var teamBehaviour = card.GetOrAddComponent<TeamBehaviour>();
            teamBehaviour.Team = team;
            return card;
        }
    }
}
