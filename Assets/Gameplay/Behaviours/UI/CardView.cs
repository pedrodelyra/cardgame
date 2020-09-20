using System;
using DG.Tweening;
using Gameplay.Core.Cards;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Vendor.SerializableDictionary;

namespace Gameplay.Behaviours.UI
{
    public class CardView : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] RectTransform rect;
        [SerializeField] Image imageCard;
        [SerializeField] CardSpriteDict cardSprites;

        Action<CardView> _onSelectCard;

        Action<CardView> _onReleaseCard;

        Vector2 _initialPosition;

        public RectTransform Rect => rect;

        public CardType Card { get; private set; }

        public CardSlot Slot { get; private set; }

        void Start()
        {
            _initialPosition = rect.anchoredPosition;
        }

        public void Setup(
            CardType card,
            CardSlot slot,
            Action<CardView> onSelectCard,
            Action<CardView> onReleaseCard)
        {
            Card = card;
            Slot = slot;
            imageCard.sprite = cardSprites[Card];
            _onSelectCard = onSelectCard;
            _onReleaseCard = onReleaseCard;
        }

        public void OnPointerDown(PointerEventData _)
        {
            _onSelectCard?.Invoke(this);
        }

        public void OnPointerUp(PointerEventData _)
        {
            _onReleaseCard?.Invoke(this);
        }

        public void Select()
        {
            const float offset = 30f;
            AnimateMovement(_initialPosition.y + offset);
        }

        public void Unselect()
        {
            AnimateMovement(_initialPosition.y);
        }

        public void KillAnimation()
        {
            rect.DOKill();
        }

        void AnimateMovement(float yTarget)
        {
            KillAnimation();
            var target = new Vector2(_initialPosition.x, yTarget);
            rect.DOAnchorPos(endValue: target, duration: 0.5f).SetEase(Ease.OutExpo);
        }
    }
}
