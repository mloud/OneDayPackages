using Cysharp.Threading.Tasks;
using OneDay.Assets;
using OneDay.Core;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;

namespace OneDay.Ui
{
    public class UiImage : Image
    {
        [SerializeField] private AssetReference ImageAssetReference;
        [Tooltip("Target image alpha when using ImageName")]
        public float ImageAlpha = 1.0f;
        
        [SerializeField] private bool unloadOnDisable;
        private string LoadedImageName { get; set; }

        protected override void OnEnable()
        {
            base.OnEnable();
            
            if (ImageAssetReference.RuntimeKeyIsValid())
            {
                LoadImageReference(ImageAssetReference);
            }
        }

        private async UniTask LoadImageReference(AssetReference reference)
        {
            this.sprite = await ObjectLocator.GetObject<IAssetManager>().Load<Sprite>(reference);
        }
        
        protected override void OnDisable()
        {
            base.OnDisable();
            if (unloadOnDisable)
            {
                if (ImageAssetReference.RuntimeKeyIsValid())
                {
                    ImageAssetReference.ReleaseAsset();
                }
            }
        }

        protected override void OnDestroy()
        {
            if (unloadOnDisable)
            {
                if (ImageAssetReference.RuntimeKeyIsValid())
                    ImageAssetReference.ReleaseAsset();
            }
        }
    }
}