using System.Collections;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace OneDay.Ui
{
    public class Loading : ABaseLoading
    {
        [SerializeField] private UiImage progressImage;
        [SerializeField] private GameObject loadingCircleAnimation;
        [SerializeField] private GameObject donePanel;
        [SerializeField] private GameObject doneLabel;
        [SerializeField] private GameObject loadingLabel;
        [SerializeField] private GameObject cancelContainer;
        [SerializeField] private UiButton cancelButton;

        private Coroutine cancelCoroutine;
        
        public override async UniTask Show(CancelSettings cancelSettings)
        {
            loadingCircleAnimation.gameObject.SetActive(true);
            doneLabel.SetActive(false);
            donePanel.SetActive(false);
            loadingLabel.SetActive(true);
            cancelContainer.SetActive(false);

            if (cancelCoroutine != null)
            {
                StopCoroutine(cancelCoroutine);
                cancelCoroutine = null;
            }
           
            await base.Show();
               
            if (cancelSettings != null)
            {
                cancelCoroutine = StartCoroutine(RunCancel(cancelSettings));
            }
        }

    
        public override async UniTask Hide(bool showDone)
        {
            loadingCircleAnimation.gameObject.SetActive(false);
            loadingLabel.SetActive(false);
            donePanel.SetActive(showDone);
            doneLabel.SetActive(showDone);
            cancelContainer.SetActive(false);
         
            if (cancelCoroutine != null)
            {
                StopCoroutine(cancelCoroutine);
                cancelCoroutine = null;
            }

            if (showDone)
            {
                await UniTask.Delay(2000);
            }

            await base.Hide();
        }

        private IEnumerator RunCancel(CancelSettings cancelSettings)
        {
            yield return new WaitForSecondsRealtime(cancelSettings.Duration);
            cancelContainer.SetActive(true);
            cancelButton.RegisterListenerWithClear(cancelSettings.Action);
        }
    }
}