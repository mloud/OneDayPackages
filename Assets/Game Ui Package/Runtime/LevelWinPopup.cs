using System;
using Cysharp.Threading.Tasks;
using OneDay.Ui;
using UnityEngine;

namespace OneDay.GameUi
{
    public class LevelWinPopupData : UiData
    {
        public int Stars;
        public int Reward;
        public Action ClaimButtonAction;
        public Action DoubleRewardAction;
    }
    
    public class LevelWinPopup : APopup<LevelWinPopupData>
    {
        [SerializeField] private UiButton claimButton;
        [SerializeField] private UiButton doubleRewardButton;

        private LevelWinPopupData Data { get; set; }
        protected override UniTask OnShow(LevelWinPopupData data)
        {
            Data = data;
            claimButton.RegisterListenerWithClear(OnClaim);
            claimButton.RegisterListenerWithClear(OnDoubleReward);
            return UniTask.CompletedTask;
        }

        protected override UniTask OnHide()
        {
            Data = null;
            return UniTask.CompletedTask;
        }

        private void OnClaim()
        {
            SetHidden();
            Data.ClaimButtonAction();
        }

        private void OnDoubleReward()
        {
            SetHidden();
            Data.ClaimButtonAction();
        }
    }
}
