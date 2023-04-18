using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace OneDay.Ui
{
    [ExecuteInEditMode]
    public class UiButton : Button
    {
        [FormerlySerializedAs("isInsideContaier")]
        [Tooltip("Most top transform that contains this button")]
        [SerializeField] private Transform buttonContainer;
        
        public TMP_Text Label => label;
        [SerializeField] protected TMP_Text label;
        [Serializable] public class StateTransitionEvent : UnityEvent<int, bool, bool> {}
        [SerializeField] protected StateTransitionEvent OnStateTransition = new();

        public void RegisterListenerWithClear(Action action)
        {
            onClick.RemoveAllListeners();
            onClick.AddListener(()=>action());
        }

        public void SetVisible(bool visible)
        {
            if (buttonContainer != null)
            {
                buttonContainer.gameObject.SetActive(visible);
            }
            else
            {
                gameObject.SetActive(visible);
            }
        }

        protected override void DoStateTransition(SelectionState state, bool instant)
        {
            base.DoStateTransition(state, instant);
            OnStateTransition.Invoke((int) state, instant, interactable);
        }
    }
}