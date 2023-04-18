using System.Collections.Generic;
using OneDay.CasualGame;
using OneDay.Ui;

namespace OneDay.Samples.FallingBlocks.Screens
{
    public class MenuScreenData : UiData
    {
        public IReadOnlyList<LevelState> LevelStates { get; }

        public MenuScreenData(IReadOnlyList<LevelState> levelStates)
        {
            LevelStates = levelStates;
        }
    }
}