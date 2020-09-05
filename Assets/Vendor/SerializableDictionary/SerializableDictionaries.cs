using System;
using Gameplay.Core;
using UnityEngine;

namespace Vendor.SerializableDictionary
{
    [Serializable]
    public class CardPrefabDict : SerializableDictionary<CardType, GameObject> {}
}