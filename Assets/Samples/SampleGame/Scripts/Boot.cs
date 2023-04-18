using OneDay.Samples.FallingBlocks.States;
using OneDay.StateMachine.Common;

namespace OneDay.Samples.FallingBlocks
{
    public class Boot : ABoot
    {
        protected override AppStateManagerConfig GetConfig() => new()
        {
            DefaultState = StateConst.BootState,
            States = new()
            {
                {StateConst.BootState, new BootState(new AppStateConfig())},
                {StateConst.MenuState, new MenuState(new AppStateConfig())},
                {StateConst.GameState, new GameState(new AppStateConfig())}
            },

            Transitions = new()
            {
                (TransitionsConst.ToMenuState, StateConst.BootState, StateConst.MenuState),
                (TransitionsConst.ToMenuState, StateConst.GameState, StateConst.MenuState),
                (TransitionsConst.ToGameState, StateConst.MenuState, StateConst.GameState),
            }
        };
    }
}