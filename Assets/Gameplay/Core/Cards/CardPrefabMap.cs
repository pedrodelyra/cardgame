using UnityEngine;
using Vendor.SerializableDictionary;

namespace Gameplay.Core.Cards
{
    public class CardPrefabMap : MonoBehaviour
    {
        [SerializeField] CardPrefabDict cardPrefabDict;

        public GameObject GetPrefab(CardType cardType)
        {
            cardPrefabDict.TryGetValue(cardType, out var prefab);
            return prefab;
        }
    }
}
