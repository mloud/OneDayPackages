using OneDay.Ui;
using UnityEngine;

namespace OneDay.Samples.FallingBlocks.Screens
{
    public class MenuScreenView : AScreenView
    {
        public UiButton PlayButton => playButton;
        
        [SerializeField] private UiButton playButton;
    }
}