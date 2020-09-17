using System;
using UnityEngine;

namespace Gameplay.Core
{
    public class Entity : MonoBehaviour
    {
        [SerializeField] Team team;

        bool _removed;

        public Team Team
        {
            get => team;
            set
            {
                if (team == value)
                {
                    return;
                }
                team = value;
                gameObject.tag = Team.GetTag();
                OnUpdateTeam?.Invoke(Team);
            }
        }

        public event Action<Team> OnUpdateTeam;

        public event Action<Entity> OnRemoved;

        public void Remove(float time = 0)
        {
            if (_removed)
            {
                return;
            }

            _removed = true;

            gameObject.SetActive(false);

            ScheduleDestroy(time);
        }

        void OnDestroy()
        {
            _removed = true;
            OnRemoved?.Invoke(this);
        }

        void ScheduleDestroy(float time) => Destroy(gameObject, time); // Unity destroy
    }
}
