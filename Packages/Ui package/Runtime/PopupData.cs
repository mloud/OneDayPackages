using System;

namespace OneDay.Ui
{
    public class PopupData : UiData
    {
        public string Title { get; }
        public string Message { get; }
        public string ConfirmButtonText { get; }

        public Action ConfirmAction { get; }
        public PopupData(string title, string message, Action confirmAction, string confirmButtonText = null)
        {
            Title = title;
            Message = message;
            ConfirmAction = confirmAction;
            ConfirmButtonText = confirmButtonText;
        }
    }
}