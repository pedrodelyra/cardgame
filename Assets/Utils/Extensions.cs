using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Utils
{
    public static class Extensions
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

        public static List<T> GetEnumValues<T>() where T : Enum
        {
            return Enum.GetValues(typeof(T)).Cast<T>().ToList();
        }

        public static T GetValueOrDefault<TK, T>(this IDictionary<TK, T> source, TK key, T defaultValue = default)
        {
            if (source.TryGetValue(key, out T value))
            {
                return value;
            }
            return source[key] = defaultValue;
        }
    }
}
