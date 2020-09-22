using Gameplay.Core;
using UnityEngine;

namespace Gameplay.Behaviours
{
    public class TroopAnimator : BaseBehaviour
    {
        [SerializeField] Animator animator;

        static readonly int StateHash = Animator.StringToHash(name: "State");

        enum State
        {
            Walk,
            Attack,
            None,
        }

        MovementBehaviour _movement;
        AttackBehaviour _attack;

        protected override void Awake()
        {
            base.Awake();
            _movement = GetComponent<MovementBehaviour>();
            _attack = GetComponent<AttackBehaviour>();

            Entity.OnUpdateTeam += OnUpdateTeam;
        }

        void OnDestroy()
        {
            Entity.OnUpdateTeam -= OnUpdateTeam;
        }

        void Update()
        {
            animator.SetInteger(id: StateHash, value: (int)GetState());
        }

        State GetState()
        {
            if (_attack.IsAttacking)
            {
                return State.Attack;
            }

            if (_movement.IsMoving)
            {
                return State.Walk;
            }

            return State.None;
        }

        void OnUpdateTeam(Team team)
        {
            var t = animator.transform;
            var r = t.localRotation.eulerAngles;
            r.y = team == Team.Home ? 90f : -90f;
            t.localRotation = Quaternion.Euler(r);
        }
    }
}
