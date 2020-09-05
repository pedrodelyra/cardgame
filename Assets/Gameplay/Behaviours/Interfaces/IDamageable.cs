using System;

namespace Gameplay.Behaviours.Interfaces
{
    public interface IDamageable
    {
        int MaxHealth { get; }

        int CurrentHealth { get; }

        void ScheduleDamage(int damage);

        event Action OnDie;
    }
}
