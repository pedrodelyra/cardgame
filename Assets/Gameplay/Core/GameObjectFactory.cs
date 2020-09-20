using Gameplay.Core.Cards;
using UnityEngine;
using Utils;

namespace Gameplay.Core
{
    public class GameObjectFactory
    {
        CardPrefabMap CardPrefabMap { get; }

        GameObject PlayerPrefab { get; }

        public GameObjectFactory(CardPrefabMap cardPrefabMap, GameObject playerPrefab)
        {
            CardPrefabMap = cardPrefabMap;
            PlayerPrefab = playerPrefab;
        }

        public Entity CreateCard(CardType cardType, Team team)
        {
            var prefab = CardPrefabMap.GetPrefab(cardType);
            var card = Extensions.Instantiate<Entity>(prefab);
            card.Team = team;
            return card;
        }

        public Player CreatePlayer(Team team)
        {
            var player = Extensions.Instantiate<Player>(PlayerPrefab);
            player.Entity.Team = team;
            return player;
        }
    }
}
