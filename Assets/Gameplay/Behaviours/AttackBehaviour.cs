using System.Collections.Generic;
using System.Linq;
using Gameplay.Behaviours.Interfaces;
using UnityEngine;
using UnityEngine.Assertions;

namespace Gameplay.Behaviours
{
    [RequireComponent(typeof(ColliderBehaviour))]
    [RequireComponent(typeof(TeamBehaviour))]
    public class AttackBehaviour : MonoBehaviour
    {
        [SerializeField] int damage = 10;
        [SerializeField] ParticleSystem hitParticle;

        const int CooldownInSeconds = 1;

        List<IAttacker> _attackers;

        ColliderBehaviour _collider;

        TeamBehaviour _team;

        IDamageable CurrentTarget { get; set; }

        public bool IsAttacking => HasValidTarget;

        bool HasValidTarget => CurrentTarget != null;

        float _attackTimer = 0;

        void Awake()
        {
            _attackers = GetComponents<IAttacker>().ToList();
            _collider = GetComponent<ColliderBehaviour>();
            _team = GetComponent<TeamBehaviour>();
            _collider.OnAddCollider += OnAddCollider;
            _collider.OnRemoveCollider += OnRemoveCollider;
        }

        void OnDestroy()
        {
            _collider.OnAddCollider -= OnAddCollider;
            _collider.OnRemoveCollider -= OnRemoveCollider;
        }

        void OnAddCollider(GameObject target)
        {
            var opponents = _team.GetCollidingEnemies();
            if (opponents.Any())
            {
                ChooseTarget();
            }
        }

        void OnRemoveCollider(GameObject target) {}

        void FixedUpdate()
        {
            if (!HasValidTarget)
            {
                return;
            }

            _attackTimer += Time.deltaTime;
            if (_attackTimer > CooldownInSeconds)
            {
                Attack();
                _attackTimer = 0f;
            }
        }

        void Attack()
        {
            Assert.IsTrue(HasValidTarget, message: "Attack should only be called with a valid target");
            Debug.Log($"{name} ATTACK: {damage} HEALTH: {CurrentTarget.Health}");
            CurrentTarget.ScheduleDamage(damage);
            _attackers.ForEach(action: attacker => attacker.Attack(CurrentTarget));

            Destroy(Instantiate(hitParticle, transform), 10);
        }

        void ChooseTarget()
        {
            if (HasValidTarget)
            {
                return;
            }

            foreach (var candidate in _team.GetCollidingEnemies())
            {
                var damageable = candidate.GetComponent<IDamageable>();
                if (damageable != null)
                {
                    Debug.Log($"current target: {candidate.name}");
                    SetTarget(damageable);
                    return;
                }
            }            
        }

        void SetTarget(IDamageable damageable)
        {
            CurrentTarget = damageable;
            CurrentTarget.OnDie += ResetTarget;
            _attackTimer = 0;
        }

        void ResetTarget()
        {
            CurrentTarget.OnDie -= ResetTarget;
            CurrentTarget = null;
        }
    }
}
