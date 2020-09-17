using System.Collections.Generic;
using System.Linq;
using Gameplay.Core;
using UnityEngine;

namespace Gameplay.Behaviours
{
    public class MovementBehaviour : Behaviour
    {
        enum Direction
        {
            Left,
            Right,
        }

        [SerializeField] int speed = 1;

        Direction _direction;
        ColliderBehaviour _collider;

        Vector3 _velocity = Vector3.zero;

        readonly Dictionary<Direction, Vector3> _directionsDict = new Dictionary<Direction, Vector3>
        {
            {Direction.Left,  Vector3.left},
            {Direction.Right, Vector3.right},
        };

        readonly Dictionary<Team, Direction> _teamDirections = new Dictionary<Team, Direction>
        {
            {Team.Home,  Direction.Right},
            {Team.Visitor, Direction.Left},
        };

        public bool IsMoving => _velocity.magnitude > 0f;

        public int Speed => speed;

        protected override void Awake()
        {
            base.Awake();
            _collider = GetComponent<ColliderBehaviour>();
            _collider.OnAddCollider += OnAddCollider;
            _collider.OnRemoveCollider += OnRemoveCollider;
            Entity.OnUpdateTeam += OnUpdateTeam;
        }

        void OnDestroy()
        {
            _collider.OnAddCollider -= OnAddCollider;
            _collider.OnRemoveCollider -= OnRemoveCollider;
            Entity.OnUpdateTeam -= OnUpdateTeam;
        }

        void Start() => Move();

        void Update()
        {
            transform.Translate(translation: _velocity * Time.deltaTime);
        }

        void Move()
        {
            Debug.Log($"{name} Move!");
            _directionsDict.TryGetValue(_direction, out var d);
            _velocity = d * speed;
        }

        void Stop()
        {
            Debug.Log($"{name} Stop!");
            _velocity = Vector2.zero;
        }

        void OnAddCollider(GameObject _)
        {
            var myTeam = Entity.Team;
            var opponents = _collider.GetCollidingObjects(myTeam.Opposite());
            if (opponents.Any())
            {
                Stop();
            }
        }

        void OnRemoveCollider(GameObject target)
        {
            var myTeam = Entity.Team;
            var opponents = _collider.GetCollidingObjects(myTeam.Opposite());
            if (opponents.Count == 0)
            {
                Move();
            }
        }

        void OnUpdateTeam(Team team)
        {
            _direction = _teamDirections[team];
        }
    }
}
