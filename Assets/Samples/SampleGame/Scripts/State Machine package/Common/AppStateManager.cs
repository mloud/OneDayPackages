namespace OneDay.StateMachine.Common
{
    public class AppStateManager : IAppStateManager
    {
        private IStateMachine StateMachine { get; }
        
        public AppStateManager(AppStateManagerConfig config)
        {
            StateMachine = new StateMachine(config.DefaultState);
            foreach (var state in config.States)
            {
                StateMachine.RegisterState(state.Value, state.Key);
            }
            foreach (var transition in config.Transitions)
            {
                StateMachine.RegisterTransition(transition.transitionName, transition.sourceState, transition.targetState);
            }
        }

        public void MakeTransition(string transition)
            => StateMachine.MakeTransition(transition);
        
        public void Run()
        {
            StateMachine.Run();
        }
    }
}