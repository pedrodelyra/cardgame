using System.Collections.Generic;
using Gameplay.Core;
using UnityEngine;
using Utils;

namespace Gameplay.Behaviours
{
    public delegate void CollisionCallback(GameObject target);

    public class ColliderBehaviour : MonoBehaviour
    {
        readonly IDictionary<Team, List<Entity>> _collidingObjects = new Dictionary<Team, List<Entity>>();

        public event CollisionCallback OnAddCollider;

        public event CollisionCallback OnRemoveCollider;

        public List<Entity> GetCollidingObjects(Team team)
        {
            return _collidingObjects[team];
        }

        void Awake()
        {
            foreach (var team in Extensions.GetEnumValues<Team>())
            {
                _collidingObjects.Add(team, new List<Entity>());
            }
        }

        void OnCollisionEnter(Collision collision)
        {
            var collidingObject = collision.collider.gameObject;
            Debug.Log($"{name} OnCollisionEnter: {collidingObject.name}!");
            AddObject(collidingObject);
        }

        void OnCollisionExit(Collision collision)
        {
            var collidingObject = collision.collider.gameObject;
            Debug.Log($"---------- {name} OnCollisionExit: {collidingObject.name}!");
            RemoveObject(collidingObject);
        }

        void AddObject(GameObject collidingObject)
        {
            var entity = collidingObject.GetComponent<Entity>();
            _collidingObjects[entity.Team].Add(entity);
            OnAddCollider?.Invoke(collidingObject);
            entity.OnRemoved += RemoveObject;
        }

        void RemoveObject(GameObject collidingObject)
        {
            var entity = collidingObject.GetComponent<Entity>();
            _collidingObjects[entity.Team].Remove(entity);
            OnRemoveCollider?.Invoke(collidingObject);
            entity.OnRemoved -= RemoveObject;
        }

        // This way we enforce that a gameObject will trigger OnRemoveCollider even if it gets destroyed
        void RemoveObject(Entity entity) => RemoveObject(entity.gameObject);
    }
}
