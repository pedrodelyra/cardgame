using UnityEngine;

namespace Gameplay.Core
{
    public class Arena : MonoBehaviour
    {
        [SerializeField] Transform parent; // TODO: Use lanes
        [SerializeField] CardPrefabMap cardPrefabMap;

        public void DeployCard(CardType cardType)
        {
            var prefab = cardPrefabMap.GetPrefab(cardType);
            var _ = Instantiate(prefab, parent);
        }
    }
}
