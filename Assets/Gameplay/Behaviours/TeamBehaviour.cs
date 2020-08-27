using System.Collections.Generic;
using System.Linq;
using Gameplay.Core;
using UnityEngine;

namespace Gameplay.Behaviours
{
    [RequireComponent(typeof(ColliderBehaviour))]
    public class TeamBehaviour : MonoBehaviour
    {
        [SerializeField] Team team;

        ColliderBehaviour _collider;

        public Team Team => team;

        public Team GetOppositeTeam() => team.Opposite();

        public IEnumerable<GameObject> GetCollidingEnemies()
        {
            if (Team == Team.None)
            {
                return Enumerable.Empty<GameObject>();
            }
            var oppositeTeam = GetOppositeTeam();
            return _collider.CollidingObjects.Where(c => c.CompareTag(oppositeTeam.GetTag()));
        }

        void Awake()
        {
            _collider = GetComponent<ColliderBehaviour>();
            gameObject.tag = Team.GetTag();
        }
    }
}
