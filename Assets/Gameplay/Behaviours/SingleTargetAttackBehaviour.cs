using Gameplay.Behaviours.Interfaces;
using UnityEngine;

namespace Gameplay.Behaviours
{
    [RequireComponent(typeof(AttackBehaviour))]
    public class SingleTargetAttackBehaviour : BaseBehaviour, IAttacker
    {
        [SerializeField] int damage = 10;

        public void Attack(IDamageable target)
        {
            target.ScheduleDamage(damage);
        }
    }
}
