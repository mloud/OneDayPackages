using System;
using System.Collections.Generic;
using UnityEngine;

namespace OneDay.Core
{
    public static class ObjectLocator
    {
        private static Dictionary<Type, object> Objects { get; } = new Dictionary<Type, object>();

        public static TInterface RegisterObject<TInterface>(object obj)
        {
            Debug.Assert(!Objects.ContainsKey(typeof(TInterface)),$"Object of type {typeof(TInterface)} is already registered");
            Objects.Add(typeof(TInterface), obj);
            return (TInterface) obj;
        }

        public static object RegisterObject(object obj, string interfaceName)
        {
            var interfaceType = Array.Find(obj.GetType().GetInterfaces(), x => x.Name.Contains(interfaceName));
            Debug.Assert(interfaceType != null,$"Object {obj.ToString()} of does not implement interface {interfaceName}");
            Debug.Assert(!Objects.ContainsKey(interfaceType), $"Object of type {interfaceType} is already registered");
            Objects.Add(interfaceType, obj);
            return obj;
        }

        public static void UnregisterObject<TInterface>()
        {
            Debug.Assert(Objects.ContainsKey(typeof(TInterface)),$"Object of type {typeof(TInterface)} is not registered");
            Objects.Remove(typeof(TInterface));
        }
        
        public static void UnregisterObject(object obj, string interfaceName)
        {
            var interfaceType = Array.Find(obj.GetType().GetInterfaces(), x => x.Name.Contains(interfaceName));
            Debug.Assert(interfaceType != null,$"Object {obj.ToString()} of does not implement interface {interfaceName}");
            Debug.Assert(Objects.ContainsKey(interfaceType), $"Object of type {interfaceType} is not registered");
            Objects.Remove(interfaceType);
        }
        
        public static TInterface GetObject<TInterface>()
        {
            if (Objects.TryGetValue(typeof(TInterface), out var obj))
            {
                return (TInterface)obj;
            }
            Debug.LogError($"Could not get object of type {typeof(TInterface)} from Locator");
            return default(TInterface);
        }
        
        public static IReadOnlyList<TInterface> GetObjects<TInterface>()
        {
            var result = new List<TInterface>();
            foreach (var pair in Objects)
            {
                if (Array.Exists(pair.Value.GetType().GetInterfaces(),x=>x == (typeof(TInterface))))
                {
                    result.Add((TInterface)pair.Value);
                }
            }

            return result;
        }
    }
}