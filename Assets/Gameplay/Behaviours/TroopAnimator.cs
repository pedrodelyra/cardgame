using Gameplay.Behaviours.UI;
using Gameplay.Core;
using UnityEngine;

namespace Gameplay.Behaviours
{
    public class TroopAnimator : MonoBehaviour
    {
        [SerializeField] Animator animator;
        [SerializeField] HealthBarBehaviour healthBarBehaviour;

        static readonly int StateHash = Animator.StringToHash(name: "State");

        enum State
        {
            Walk,
            Attack,
            None,
        }

        MovementBehaviour _movement;
        AttackBehaviour _attack;
        TeamBehaviour _team;

        void Awake()
        {
            _movement = GetComponent<MovementBehaviour>();
            _attack = GetComponent<AttackBehaviour>();
            _team = GetComponent<TeamBehaviour>();

            if (healthBarBehaviour)
            {
                healthBarBehaviour.Setup(GetComponent<DamageableBehaviour>(), transform);
            }

            _team.OnUpdateTeam += OnUpdateTeam;
        }

        void OnDestroy()
        {
            _team.OnUpdateTeam -= OnUpdateTeam;
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
