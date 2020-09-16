using System.Collections.Generic;
using System.Linq;
using Gameplay.Core;
using UnityEngine;
using Utils.Extensions;

namespace Gameplay.Behaviours
{
    [RequireComponent(typeof(TeamBehaviour))]
    public class MovementBehaviour : MonoBehaviour
    {
        public enum Direction
        {
            Left,
            Right,
        }

        [SerializeField] int speed = 1;
        [SerializeField] Direction direction;

        ColliderBehaviour _collider;
        TeamBehaviour _team;

        Vector3 _velocity = Vector3.zero;

        readonly Dictionary<Direction, Vector3> _directionsDict = new Dictionary<Direction, Vector3>
        {
            {Direction.Left,  Vector3.left},
            {Direction.Right, Vector3.right},
        };

        public bool IsMoving => _velocity.magnitude > 0f;

        public int Speed => speed;

        void Awake()
        {
            _collider = GetComponent<ColliderBehaviour>();
            _team = GetComponent<TeamBehaviour>();
            _collider.OnAddCollider += OnAddCollider;
            _collider.OnRemoveCollider += OnRemoveCollider;
            _team.OnUpdateTeam += OnUpdateTeam;
        }

        void OnDestroy()
        {
            _collider.OnAddCollider -= OnAddCollider;
            _collider.OnRemoveCollider -= OnRemoveCollider;
            _team.OnUpdateTeam -= OnUpdateTeam;
        }

        void Start() => Move();

        void Update()
        {
            transform.Translate(translation: _velocity * Time.deltaTime);
        }

        void Move()
        {
            Debug.Log($"{name} Move!");
            _directionsDict.TryGetValue(direction, out var d);
            _velocity = d * speed;
        }

        void Stop()
        {
            Debug.Log($"{name} Stop!");
            _velocity = Vector2.zero;
        }

        void OnAddCollider(GameObject target)
        {
            var opponents = _team.GetCollidingEnemies();
            if (opponents.Any())
            {
                Stop();
            }
        }

        void OnRemoveCollider(GameObject target)
        {
            var opponents = _team.GetCollidingEnemies();
            if (!opponents.Any())
            {
                Move();
            }
        }

        void OnUpdateTeam(Team team)
        {
            direction = team == Team.Home ? Direction.Right : Direction.Left;
        }

        void AdjustVerticalPosition()
        {
            _velocity.z = speed;
        }
    }
}
