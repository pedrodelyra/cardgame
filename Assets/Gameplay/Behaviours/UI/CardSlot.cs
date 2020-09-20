using System.Collections;
using DG.Tweening;
using UnityEngine;
using Utils;

namespace Gameplay.Behaviours.UI
{
    public class CardSlot : MonoBehaviour
    {
        [SerializeField] RectTransform rect;

        public bool IsEmpty { get; private set; } = true;

        public void AddCard(CardView cardView, float animationDelay)
        {
            if (!IsEmpty)
            {
                return;
            }

            IsEmpty = false;
            StartCoroutine(MoveCardCoroutine(cardView, animationDelay));
        }

        public void RemoveCard()
        {
            if (IsEmpty)
            {
                return;
            }
            Debug.Log("REMOVE CARD!");
            rect.DestroyChildren();
            IsEmpty = true;
        }

        IEnumerator MoveCardCoroutine(CardView cardView, float animationDelay)
        {
            yield return new WaitForSeconds(animationDelay);

            cardView.transform.SetParent(rect);
            cardView.KillAnimation();
            var cardRect = cardView.Rect;
            cardRect.DOAnchorPos3D(Vector3.zero, 1f);
        }
    }
}
