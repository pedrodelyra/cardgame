using Gameplay.Core.Cards;
using Utils;

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
            return Extensions.Instantiate<Entity>(prefab);
        }
    }
}
