using Gameplay.Behaviours;
using Gameplay.Core.Cards;
using UnityEngine;

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

        public GameObject DeployCard(CardType cardType, Team team, int laneIdx)
        {
            var card = GameObjectFactory.CreateCard(cardType);
            var lane = Arena.Lanes[laneIdx];
            card.transform.SetParent(lane.transform);
            card.transform.SetAsLastSibling();
            card.transform.position = lane.GetCorner(team).position;

            var teamBehaviour = card.GetComponent<TeamBehaviour>();
            teamBehaviour.Team = team;
            return card;
        }
    }
}
