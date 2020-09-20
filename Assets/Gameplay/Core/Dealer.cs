using System;
using Gameplay.Core.Cards;
using UnityEngine;

namespace Gameplay.Core
{
    public class Dealer
    {
        const int HandSize = 4;

        public event Action<CardType, Team> OnDealtCard;

        public void DealInitialCards(IPlayer player)
        {
            for (var i = 0; i < HandSize; ++i)
            {
                DealCard(player);
            }            
        }

        public void DealCard(IPlayer player)
        {
            var deck = player.Deck;
            var card = BuyCard(deck);
            player.Hand.AddCard(card);
            Debug.Log("ON DEALT CARD!!!!!!!!!!!!");
            OnDealtCard?.Invoke(card, player.Team);
        }

        CardType BuyCard(PlayerDeck deck)
        {
            if (deck.AmountOfCardsBought % PlayerDeck.DeckSize == 0)
            {
                deck.Shuffle();
            }

            return deck.BuyCard();
        }
    }
}
