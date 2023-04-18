using OneDay.Core;

namespace OneDay.CasualGame
{
    public delegate void LevelStarted(int level);
    public delegate void LevelFinished(LevelResult result);
    public delegate void GamePaused();

    public interface IGameManager<TLevelDefinition, TLevelState>  : IManager
        where TLevelDefinition : class where TLevelState: LevelState
    {
        LevelStarted LevelStarted { get; set; }
        LevelFinished LevelFinished { get; set; }
        GamePaused GamePaused { get; set; }

        bool IsGameRunning();
        void Play();
        void Play(int level);
        void Pause();
    }
}