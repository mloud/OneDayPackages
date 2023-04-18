using System;
using Cysharp.Threading.Tasks;
using OneDay.Localization.Components;
using TMPro;
using UnityEngine;

namespace OneDay.Ui
{
    public class QuestionPopupData : PopupData
    {
        public Action CancelAction { get; }
        public string CancelButtonText { get; }

        public QuestionPopupData(string title, string message, Action confirmAction, Action cancelAction, string confirmButtonText = null, string cancelButtonText = null) : base(title,
            message, confirmAction, confirmButtonText)
        {
            CancelAction = cancelAction;
            CancelButtonText = cancelButtonText;
        }
    }
    
    public class QuestionPopup : APopup<QuestionPopupData>
    {
        [SerializeField] private UiButton confirmButton;
        [SerializeField] private UiButton cancelButton;
        [SerializeField] private TMP_Text titleLabel;
        [SerializeField] private TMP_Text messageLabel;
        protected override UniTask OnShow(QuestionPopupData data)
        {
            titleLabel.text = data.Title;
            messageLabel.text = data.Message;
            confirmButton.RegisterListenerWithClear(()=>
            {
                Hide();
                data.ConfirmAction?.Invoke();
            });
            cancelButton.RegisterListenerWithClear(()=>
            {
                Hide();
                data.CancelAction?.Invoke();
            });

            // confirm button
            var localizedText = confirmButton.Label.GetComponent<LocalizedText>();
            if (localizedText != null)
            {
                localizedText.enabled = data.ConfirmButtonText == null;
            }
            if (data.ConfirmButtonText != null)
            {
                confirmButton.Label.text = data.ConfirmButtonText;
            }
          
            
            // cancel button
            localizedText = cancelButton.Label.GetComponent<LocalizedText>();
            if (localizedText != null)
            {
                localizedText.enabled = data.CancelButtonText == null;
            }
            if (data.CancelButtonText != null)
            {
                cancelButton.Label.text = data.CancelButtonText;
            }
            
            return UniTask.CompletedTask;
        }
    }
}