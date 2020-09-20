using System;
using System.Collections.Generic;
using Gameplay.Behaviours.Interfaces;
using Gameplay.Core;
using Gameplay.Core.Cards;
using UnityEngine;

namespace Gameplay.Behaviours.UI
{
    public class GameplayHUD : MonoBehaviour
    {
        [SerializeField] RectTransform containerHUD;
        [SerializeField] RectTransform containerCards;
        [SerializeField] RectTransform containerDeck;

        [Header("Prefabs")]
        [SerializeField] TroopHealthBar prefabTroopHealthBar;
        [SerializeField] CardSlot prefabCardSlot;
        [SerializeField] CardView prefabCardView;

        List<CardSlot> CardSlots { get; } = new List<CardSlot>();

        List<CardView> CardViews { get; } = new List<CardView>();

        GestureRecognizer GestureRecognizer { get; set; }

        Arena Arena { get; set; }

        CardView _selectedCard;

        public event Action<CardType, int /* laneIdx */> OnUseCard;

        public void Setup(GestureRecognizer gestureRecognizer, Arena arena)
        {
            GestureRecognizer = gestureRecognizer;
            Arena = arena;
            GestureRecognizer.InputMoved += OnInputMoved;
            GestureRecognizer.InputEnded += OnInputEnded;
        }

        public void CreateHealthBar(IDamageable damageable, Team team, Transform attachPoint)
        {
            var healthBar = Instantiate(prefabTroopHealthBar, containerHUD);
            healthBar.Setup(damageable, team, attachPoint);
        }

        public void CreateCardSlots(int numSlots)
        {
            for (var i = 0; i < numSlots; ++i)
            {
                var cardSlot = Instantiate(prefabCardSlot, containerCards);
                CardSlots.Add(cardSlot);
            }
        }

        public void OnCardDealt(CardType card, Team team)
        {
            Debug.Log("[HUD] ON DEALT CARD!!!!!!!!!!!!");
            if (team != Team.Home)
            {
                return;
            }

            for (var i = 0; i < CardSlots.Count; ++i)
            {
                var slot = CardSlots[i];
                Debug.Log($"Add card? {slot.IsEmpty}");
                if (!slot.IsEmpty)
                {
                    continue;
                }

                var cardView = Instantiate(prefabCardView, containerDeck);
                cardView.Setup(card, slot, OnSelectCard, OnReleaseCard);
                slot.AddCard(cardView, animationDelay: 0.1f * i);
                CardViews.Add(cardView);
            }
        }

        void OnDestroy()
        {
            GestureRecognizer.InputMoved -= OnInputMoved;
            GestureRecognizer.InputEnded -= OnInputEnded;
        }

        void OnSelectCard(CardView cardView)
        {
            if (_selectedCard != cardView)
            {
                UnselectAllCards();
                _selectedCard = cardView;
            }
        }

        void OnReleaseCard(CardView cardView)
        {
            cardView.Select();
        }

        void UnselectAllCards()
        {
            foreach (var card in CardViews)
            {
                card.Unselect();
            }
        }

        void OnInputMoved(Vector2 startPosition, Vector2 position, float deltaTime)
        {
            if (_selectedCard)
            {
                _selectedCard.KillAnimation();
                _selectedCard.transform.position = position;
            }
        }

        void OnInputEnded(Vector2 startPosition, Vector2 position, float deltaTime)
        {
            var selectedCard = _selectedCard;
            _selectedCard = null;

            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit, maxDistance: Mathf.Infinity, layerMask: 1 << 10)) {
                for (var i = 0; i < Arena.Lanes.Length; ++i)
                {
                    var lane = Arena.Lanes[i];
                    if (lane.transform == hit.transform)
                    {
                        var card = selectedCard.Card;
                        selectedCard.Slot.RemoveCard();
                        OnUseCard?.Invoke(card, i);
                    }
                }
            }
        }
    }
}
