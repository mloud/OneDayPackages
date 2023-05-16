using System;
using System.Linq;
using UnityEngine;

namespace OneDay.Core
{
    public class ObjectRegistrator : MonoBehaviour
    {
        [SerializeField] private string interfaceName;
        [SerializeField] private Component obj;
        private async void Awake()
        {
            ObjectLocator.RegisterObject(obj, interfaceName);
            var interfaces = obj.GetType().GetInterfaces().ToArray();
            if (Array.Find(interfaces, x => x == typeof(IManager)) != null)
            {
                await ((IManager)obj).Initialize();
            }
        }

        private void OnDestroy()
        {
            ObjectLocator.UnregisterObject(obj, interfaceName);
        }
    }
}