using Gameplay.Behaviours.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.Behaviours.UI
{
    public class HealthBarBehaviour : MonoBehaviour
    {
        [SerializeField] Transform root;
        [SerializeField] RectTransform innerContainer;
        [SerializeField] Vector2 offset;
        [SerializeField] Image imageFill;

        IDamageable Damageable { get; set; }

        Transform AttachPoint { get; set; }

        Camera _camera;

        void Awake()
        {
            _camera = Camera.main;
        }

        void Update()
        {
            if (AttachPoint)
            {
                root.position = _camera.WorldToScreenPoint(AttachPoint.position);
                innerContainer.anchoredPosition = offset;
            }
            UpdateHealthBar();
        }

        void UpdateHealthBar()
        {
            if (Damageable == null)
            {
                return;
            }

            var percentage = (float) Damageable.CurrentHealth / Damageable.MaxHealth;
            imageFill.fillAmount = percentage;
        }

        public void Setup(IDamageable damageable, Transform attachPoint)
        {
            Damageable = damageable;
            AttachPoint = attachPoint;
        }
    }
}
