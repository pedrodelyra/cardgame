using System.Collections.Generic;
using Gameplay.Behaviours;
using UnityEngine;
using UnityEngine.EventSystems;
using Utils;

namespace Gameplay.Core
{
    public class Lane : MonoBehaviour
    {
        [SerializeField] Collider collider;
        [SerializeField] Transform cornerLeft;
        [SerializeField] Transform cornerRight;

        const float MinZ = -0.75f;
        const float MaxZ = +0.75f;

        IDictionary<Team, List<MovementBehaviour>> LaneObjects { get; } = new Dictionary<Team, List<MovementBehaviour>>();

        Transform GetCorner(Team team) => team == Team.Home
            ? cornerLeft
            : cornerRight;

        List<MovementBehaviour> GetObjectsFromTeam(Team team) =>
            LaneObjects.GetValueOrDefault(team, new List<MovementBehaviour>());

        public Collider Collider => collider;

        public void AddEntity(Entity entity, Team team)
        {
            entity.transform.SetParent(transform);
            entity.transform.SetAsLastSibling();
            entity.transform.position = GetCorner(team).position;
            entity.OnRemoved += OnRemovedEntity;
            var teamObjects = GetObjectsFromTeam(team);
            teamObjects.Add(entity.GetComponent<MovementBehaviour>());
        }

        void Update()
        {
            if (LaneObjects == null)
            {
                return;
            }

            foreach (var kv in LaneObjects)
            {
                var objects = kv.Value;
                AdjustObjectsVerticalPosition(objects);
            }
        }

        void AdjustObjectsVerticalPosition(IReadOnlyList<MovementBehaviour> objects)
        {
            var amountOfObjects = objects.Count;
            var verticalOffset = 1f / (1 + amountOfObjects); // f(1) = 0.5, f(2) = 0.33, f(3) = 0.25, ...
            for (var i = 0; i < amountOfObjects; ++i)
            {
                var obj = objects[i].transform;
                var zTarget = Mathf.Lerp(MinZ, MaxZ, verticalOffset * (1 + i));
                var zVector = zTarget * Vector3.forward;
                const float eps = 0.005f; // close enough
                if (Mathf.Abs(obj.position.z - zTarget) < eps)
                {
                    continue;
                }
                obj.Translate(translation: zVector * Time.deltaTime);
            }
        }

        void OnRemovedEntity(Entity entity)
        {
            entity.OnRemoved -= OnRemovedEntity;
            var teamObjects = GetObjectsFromTeam(entity.Team);
            teamObjects.Remove(entity.GetComponent<MovementBehaviour>());
        }
    }
}
