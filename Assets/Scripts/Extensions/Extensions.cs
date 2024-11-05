using UnityEngine;
using System.Collections.Generic;

namespace GameSystem
{
    public static class Extensions
    {
        public static int RoundToInt(this float value) => Mathf.RoundToInt(value);

        public static void ProcessObjects<T>(this List<T> objects, System.Func<T, bool> condition, System.Action<T> trueAction, System.Action<T> falseAction)
        {
            for (int i = objects.Count - 1; i >= 0; i--)
            {
                T obj = objects[i];

                if (condition(obj))
                    trueAction?.Invoke(obj);
                else
                    falseAction?.Invoke(obj);
            }
        }

        public static bool IsNotNullUniversal<T>(this T instance)
        {
            if (instance is Object unityObject)
                return unityObject != null && instance != null;
            else
                return instance != null;
        }

        #region Add/Remove Metods

        public static bool TryAddObject<T>(this GameObject gameObject, List<T> list)
        {
            return gameObject != null && gameObject.TryGetComponent(out T itemType) && itemType.TryAddItem(list);
        }

        public static bool TryRemoveObject<T>(this GameObject gameObject, List<T> list)
        {
            return gameObject != null && gameObject.TryGetComponent(out T itemType) && itemType.TryRemoveItem(list);
        }

        public static bool TryAddItem<T>(this T obj, List<T> list)
        {
            if (obj.IsNotNullUniversal() && !list.Contains(obj))
            {
                list.Add(obj);
                return true;
            }
            else
                return false;
        }

        public static bool TryRemoveItem<T>(this T obj, List<T> list)
        {
            if (obj.IsNotNullUniversal() && list.Contains(obj))
            {
                list.Remove(obj);
                return true;
            }
            else
                return false;
        }

        #endregion
    }
}