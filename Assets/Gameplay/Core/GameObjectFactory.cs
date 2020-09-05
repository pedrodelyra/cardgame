using UnityEngine;

namespace Gameplay.Core
{
    public class GameObjectFactory
    {
        GameObject BasePrefab { get; }

        public GameObjectFactory(GameObject basePrefab)
        {
            BasePrefab = basePrefab;
        }

        GameObject Create()
        {
            var instance = Object.Instantiate(BasePrefab);
            return instance;
        }
    }
}
