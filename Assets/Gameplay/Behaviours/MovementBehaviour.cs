using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gameplay.Behaviours
{
    [RequireComponent(typeof(TeamBehaviour))]
    public class MovementBehaviour : MonoBehaviour
    {
        enum Direction
        {
            Left,
            Right,
        }

        [SerializeField] int speed = 1;
        [SerializeField] Direction direction;

        Rigidbody _rigidBody;
        ColliderBehaviour _collider;
        TeamBehaviour _team;

        Vector2 _velocity = Vector2.zero;

        readonly Dictionary<Direction, Vector3> _directionsDict = new Dictionary<Direction, Vector3>
        {
            {Direction.Left,  Vector3.left},
            {Direction.Right, Vector3.right},
        };

        public bool IsMoving => _velocity.magnitude > 0f;

        void Awake()
        {
            _rigidBody = GetComponent<Rigidbody>();
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
    }
}
