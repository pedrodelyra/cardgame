using System.Collections.Generic;
using System.Linq;
using Gameplay.Core.Cards;
using UnityEngine.Assertions;
using Utils;

namespace Gameplay.Core
{
    public class PlayerHand
    {
        public const int HandSize = 4;

        public List<CardType> Cards { get; } = new List<CardType>();

        public int Size => Cards.Count;

        public void AddCard(CardType card)
        {
            Cards.Add(card);
        }

        public void RemoveCard(int idx)
        {
            Cards.RemoveAt(idx);
        }

        public bool HasCard(CardType card) => Cards.Any(c => c == card);
    }
}
