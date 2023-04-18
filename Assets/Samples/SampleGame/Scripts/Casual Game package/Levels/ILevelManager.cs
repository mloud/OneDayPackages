using System.Collections.Generic;
using OneDay.Core;

namespace OneDay.CasualGame
{
    public interface ILevelManager<TLevelDefinition, TLevelState>: IManager
    {
        IReadOnlyList<TLevelDefinition> GetLevelDefinitions();
        IReadOnlyList<TLevelState> GetLevelStates();
        IReadOnlyList<(TLevelDefinition levelDefinition, TLevelState levelState)>  GetLevels();
     
        int GetCurrentLevel();
        void SetLevelFinished(int level, int score);

    }
}