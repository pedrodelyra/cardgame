using Gameplay.Core.Cards;
using UnityEngine;

namespace Gameplay.Core
{
    public class GameObjectFactory
    {
        CardPrefabMap CardPrefabMap { get; }

        public GameObjectFactory(CardPrefabMap cardPrefabMap)
        {
            CardPrefabMap = cardPrefabMap;
        }

        public GameObject CreateCard(CardType cardType)
        {
            var prefab = CardPrefabMap.GetPrefab(cardType);
            return Object.Instantiate(prefab);
        }
    }
}
