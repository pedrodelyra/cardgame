using System;
using Gameplay.Behaviours.Interfaces;
using UnityEngine;

namespace Gameplay.Behaviours
{
    public class DamageableBehaviour : MonoBehaviour, IDamageable
    {
        [SerializeField] int health;

        public int Health => health;

        public event Action OnDie;

        int _scheduledDamage;

        public void ScheduleDamage(int damage)
        {
            _scheduledDamage += damage;
        }

        void LateUpdate()
        {
            health = Mathf.Max(health - _scheduledDamage, 0);
            if (health == 0)
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
