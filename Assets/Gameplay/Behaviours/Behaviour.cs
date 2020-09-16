using Gameplay.Core;
using UnityEngine;

namespace Gameplay.Behaviours
{
    public class Behaviour : MonoBehaviour
    {
        protected Entity Entity;

        protected virtual void Awake() => Initialize();

        void Initialize()
        {
            if (Entity == null)
            {
                Entity = GetComponent<Entity>();
            }
        }
    }
}
