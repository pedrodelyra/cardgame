using System;
using Gameplay.Core;
using Gameplay.Core.Cards;
using UnityEngine;

namespace Vendor.SerializableDictionary
{
    [Serializable]
    public class CardPrefabDict : SerializableDictionary<CardType, GameObject> {}

    [Serializable]
    public class ColorsByTeamDict : SerializableDictionary<Team, Color> {}
}