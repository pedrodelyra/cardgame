using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Behaviours
{
    public delegate void CollisionCallback(GameObject target);

    [RequireComponent(typeof(Collider))]
    public class ColliderBehaviour : MonoBehaviour
    {
        public readonly HashSet<GameObject> CollidingObjects = new HashSet<GameObject>();

        public event CollisionCallback OnAddCollider;

        public event CollisionCallback OnRemoveCollider;

        void OnCollisionEnter(Collision collision)
        {
            var collidingObject = collision.collider.gameObject;
            Debug.Log($"{name} OnCollisionEnter: {collidingObject.name}!");
            AddObject(collidingObject);
        }

        void OnCollisionExit(Collision collision)
        {
            var collidingObject = collision.collider.gameObject;
            Debug.Log($"{name} OnCollisionExit: {collidingObject.name}!");
            RemoveObject(collidingObject);
        }

        void AddObject(GameObject collidingObject)
        {
            CollidingObjects.Add(collidingObject);
            OnAddCollider?.Invoke(collidingObject);
        }

        void RemoveObject(GameObject collidingObject)
        {
            CollidingObjects.Remove(collidingObject);
            OnRemoveCollider?.Invoke(collidingObject);
        }
    }
}
