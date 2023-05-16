using System;
using Cysharp.Threading.Tasks;
using OneDay.Ui;
using UnityEngine;

namespace OneDay.GameUi
{
    public class LevelLoosePopupData : UiData
    {
        public Action ExitButtonAction;
        public Action RetryButtonAction;
    }
    
    public class LevelLoosePopup : APopup<LevelLoosePopupData>
    {
        [SerializeField] private UiButton retryButton;
        [SerializeField] private UiButton exitButton;

        private LevelLoosePopupData Data { get; set; }
        protected override UniTask OnShow(LevelLoosePopupData data)
        {
            Data = data;
            retryButton.RegisterListenerWithClear(OnRetry);
            exitButton.RegisterListenerWithClear(OnExit);
            return UniTask.CompletedTask;
        }

        private void OnExit()
        { 
            SetHidden();
            Data.ExitButtonAction();
        }

        protected override UniTask OnHide()
        {
            Data = null;
            return UniTask.CompletedTask;
        }

        private void OnRetry()
        {
            SetHidden();
            Data.RetryButtonAction();
        }
    }
}