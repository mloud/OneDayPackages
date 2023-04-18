using System;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace OneDay.Ui
{
    public class SimplePopupData : PopupData
    {
        public SimplePopupData(string title, string message, Action confirmAction) : base(title, message, confirmAction)
        { }
    }
    
    public class SimplePopup : APopup<SimplePopupData>
    {
        [SerializeField] private UiButton confirmButton;
        [SerializeField] private TMP_Text titleLabel;
        [SerializeField] private TMP_Text messageLabel;

        protected override UniTask OnShow(SimplePopupData data)
        {
            titleLabel.text = data.Title;
            messageLabel.text = data.Message;
            confirmButton.RegisterListenerWithClear(()=>
            {
                Hide();
                data.ConfirmAction?.Invoke();
            });
            return UniTask.CompletedTask;
        }
    }
}