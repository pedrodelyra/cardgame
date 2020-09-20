using System.Collections.Generic;
using System.Linq;
using Gameplay.Core.Cards;
using UnityEngine.Assertions;
using Utils;

namespace Gameplay.Core
{
    public class PlayerDeck
    {
        public const int DeckSize = 8;

        LinkedList<CardType> Cards { get; set; } = new LinkedList<CardType>();

        public int AmountOfCardsBought { get; private set; }

        public PlayerDeck(List<CardType> cards)
        {
            Assert.AreEqual(expected: DeckSize, actual: cards.Count, message: $"Deck should have {DeckSize} cards");
            foreach (var card in cards.Shuffle())
            {
                Cards.AddLast(card);
            }
        }

        public void Shuffle()
        {
            Cards = new LinkedList<CardType>(Cards.Shuffle());
        }

        public CardType BuyCard()
        {
            Assert.IsTrue(Cards.Count > 0, message: "Deck shouldn't be empty");
            var topCard = Cards.First.Value;
            Cards.RemoveFirst();
            Cards.AddLast(topCard);
            ++AmountOfCardsBought;
            return topCard;
        }

        // TODO: Dummy method that should be removed once we have a way to choose decks
        public static PlayerDeck GetDummyDeck()
        {
            var cards = new List<CardType>();
            foreach (var card in Extensions.GetEnumValues<CardType>())
            {
                cards.Add(card);
            }

            while (cards.Count < PlayerDeck.DeckSize)
            {
                cards.Add(CardType.Warrior);
            }
            return new PlayerDeck(cards);
        }
    }
}
