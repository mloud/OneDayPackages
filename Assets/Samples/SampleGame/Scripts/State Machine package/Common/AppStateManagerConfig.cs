using System.Collections.Generic;

namespace OneDay.StateMachine.Common
{
    public class AppStateManagerConfig
    {
        public string DefaultState;
        public Dictionary<string, IState> States;
        public List<(string transitionName, string sourceState, string targetState)> Transitions;
    }
}