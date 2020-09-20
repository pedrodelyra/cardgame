using System;
using Gameplay.Behaviours.Interfaces;
using Gameplay.Core;
using UnityEngine;

namespace Gameplay.Behaviours
{
    public class DamageableBehaviour : Behaviour, IDamageable
    {
        [SerializeField] int maxHealth;

        public int CurrentHealth { get; set; }

        public int MaxHealth => maxHealth;

        public event Action<IDamageable> OnDie;
        public event Action<IDamageable> OnHealthChanged;

        int _scheduledDamage;

        public void ScheduleDamage(int damage)
        {
            _scheduledDamage += damage;
        }

        protected override void Awake()
        {
            base.Awake();
            CurrentHealth = MaxHealth;
        }

        void LateUpdate()
        {
            CurrentHealth = Mathf.Max(CurrentHealth - _scheduledDamage, 0);

            if (_scheduledDamage > 0)
            {
                OnHealthChanged?.Invoke(this);
            }

            if (CurrentHealth == 0)
            {
                Die();
            }

            _scheduledDamage = 0;
        }

        void Die()
        {
            OnDie?.Invoke(this);
            Entity.Remove();
        }
    }
}
