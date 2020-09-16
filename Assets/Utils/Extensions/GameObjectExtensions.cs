using System.Collections.Generic;
using UnityEngine;

namespace Utils.Extensions
{
    public static class GameObjectExtensions
    {
        public static T Instantiate<T>(GameObject gameObject)
        {
            var obj = Object.Instantiate(gameObject);
            return obj.GetComponent<T>();
        }

        public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
        {
            var component = gameObject.GetComponent<T>();
            if (component == null)
            {
                component = gameObject.AddComponent<T>();
            }
            return component;
        }

        public static T GetOrAddComponent<T>(this Behaviour behaviour) where T : Component
        {
            return behaviour.gameObject.GetOrAddComponent<T>();
        }
    }
    
    public class ByInstanceId : IComparer<GameObject>
    {
        public int Compare(GameObject lhs, GameObject rhs)
        {
            var lhsId = lhs != null ? lhs.GetInstanceID() : 0;
            var rhsId = rhs != null ? rhs.GetInstanceID() : 0;
            return lhsId.CompareTo(rhsId);
        }
    }
}
