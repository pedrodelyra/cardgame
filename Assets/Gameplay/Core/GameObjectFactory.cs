using Gameplay.Core.Cards;
using UnityEngine;
using Utils.Extensions;

namespace Gameplay.Core
{
    public class GameObjectFactory
    {
        CardPrefabMap CardPrefabMap { get; }

        public GameObjectFactory(CardPrefabMap cardPrefabMap)
        {
            CardPrefabMap = cardPrefabMap;
        }

        public Entity CreateCard(CardType cardType)
        {
            var prefab = CardPrefabMap.GetPrefab(cardType);
            return GameObjectExtensions.Instantiate<Entity>(prefab);
        }
    }
}
