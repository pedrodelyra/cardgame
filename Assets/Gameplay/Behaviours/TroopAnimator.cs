using Gameplay.Behaviours.UI;
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

        void Awake()
        {
            _movement = GetComponent<MovementBehaviour>();
            _attack = GetComponent<AttackBehaviour>();
            AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
            foreach(AnimationClip clip in clips)
            {
                Debug.Log($"{clip.name} {clip.length}");
            }

            if (healthBarBehaviour)
            {
                healthBarBehaviour.Setup(GetComponent<DamageableBehaviour>(), transform);
            }
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
    }
}
