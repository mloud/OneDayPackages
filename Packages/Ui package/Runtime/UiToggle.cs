using UnityEngine;
using UnityEngine.UI;

namespace OneDay.Ui
{
    public class UiToggle : Toggle
    {
        [SerializeField] private Transform OffTransform;
        [SerializeField] private Transform OnTransform;

        private bool IsInitialized { get; set; }
        protected override void Awake()
        {
            base.Awake();
            if (!IsInitialized)
            {
                Initialize();
            }
        }

        private void Initialize()
        {
            IsInitialized = true;
            OffTransform = transform.Find("OffState");
            OnTransform = transform.Find("OnState");
            onValueChanged.AddListener(OnValueChanged);
        }
        
        public new bool isOn
        {
            get => base.isOn;
            set
            {
                if (!IsInitialized)
                {
                    Initialize();
                }
                base.isOn = value;
            }
        }

        public void OnValueChanged(bool on)
        {
            if (OffTransform != null)
            {
                OffTransform.gameObject.SetActive(!on);
            }

            if (OnTransform != null)
            {
                OnTransform.gameObject.SetActive(on);
            }
        }

        public void RemoveAllListenersExceptBase()
        {
            onValueChanged.RemoveAllListeners();
            onValueChanged.AddListener(OnValueChanged);
        }
    }
}