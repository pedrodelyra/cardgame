using System;

namespace Gameplay.Behaviours.Interfaces
{
    public interface IDamageable
    {
        int Health { get; }

        void ScheduleDamage(int damage);

        event Action OnDie;
    }
}
