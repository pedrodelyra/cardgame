using System;
using Gameplay.Behaviours.Interfaces;
using UnityEngine;

namespace Gameplay.Behaviours
{
    public class DamageableBehaviour : MonoBehaviour, IDamageable
    {
        [SerializeField] int maxHealth;

        public int CurrentHealth { get; set; }

        public int MaxHealth => maxHealth;

        public event Action OnDie;

        int _scheduledDamage;

        public void ScheduleDamage(int damage)
        {
            _scheduledDamage += damage;
        }

        void Awake()
        {
            CurrentHealth = MaxHealth;
        }

        void LateUpdate()
        {
            CurrentHealth = Mathf.Max(CurrentHealth - _scheduledDamage, 0);
            if (CurrentHealth == 0)
            {
                Die();
            }
            _scheduledDamage = 0;
        }

        void Die()
        {
            Debug.Log($"{name} DIE!");
            OnDie?.Invoke();
            Destroy(gameObject);
        }
    }
}
