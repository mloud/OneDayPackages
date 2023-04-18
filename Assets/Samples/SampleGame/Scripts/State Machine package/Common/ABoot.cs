using OneDay.Core;
using UnityEngine;

namespace OneDay.StateMachine.Common
{
    public abstract class ABoot : MonoBehaviour
    {
        private void Awake()
        {
            ObjectLocator.RegisterObject<IAppStateManager>(new AppStateManager(GetConfig()));
            ObjectLocator.GetObject<IAppStateManager>().Run();
        }
        
        protected abstract AppStateManagerConfig GetConfig();
    }
}