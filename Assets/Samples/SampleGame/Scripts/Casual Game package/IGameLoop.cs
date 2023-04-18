using Cysharp.Threading.Tasks;

namespace OneDay.CasualGame
{
    public interface IGameLoop<TLevelDefinition>
    {
        public delegate void LevelFinishedDelegate(int score);
        public delegate void LevelFailedDelegate();

        LevelFinishedDelegate LevelFinished { get; set; }
        LevelFailedDelegate LevelFailed { get; set; }

        UniTask Load(TLevelDefinition levelDefinition, int level);
        void Play(TLevelDefinition levelDefinition, int level);
        void SetPaused(bool paused);
    }
}