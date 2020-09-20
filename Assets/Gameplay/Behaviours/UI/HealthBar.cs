using Gameplay.Behaviours.Interfaces;
using Gameplay.Core;
using UnityEngine;
using UnityEngine.UI;
using Vendor.SerializableDictionary;

namespace Gameplay.Behaviours.UI
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] Image imageFill;
        [SerializeField] Slider slider;
        [SerializeField] Text labelHealthPoints;
        [SerializeField] ColorsByTeamDict teamColors;

        void UpdateHealthBar(IDamageable damageable)
        {
            slider.minValue = 0;
            slider.maxValue = damageable.MaxHealth;
            slider.value = damageable.CurrentHealth;

            if (labelHealthPoints)
            {
                labelHealthPoints.text = $"{damageable.CurrentHealth}";
            }
        }

        void OnHealthChanged(IDamageable damageable)
        {
            UpdateHealthBar(damageable);
        }

        void SetTeam(Team team)
        {
            imageFill.color = teamColors[team];
        }

        void OnDamageableDied(IDamageable damageable)
        {
            damageable.OnHealthChanged -= OnHealthChanged;
            damageable.OnDie -= OnDamageableDied;
        }

        public void Setup(IDamageable damageable, Team team)
        {
            damageable.OnHealthChanged += OnHealthChanged;
            damageable.OnDie += OnDamageableDied;

            UpdateHealthBar(damageable);
            SetTeam(team);
        }
    }
}
