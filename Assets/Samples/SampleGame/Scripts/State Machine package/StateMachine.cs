using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace OneDay.StateMachine
{
    public class StateMachine : IStateMachine
    {
        private Dictionary<string, IState> States { get; }
        private Dictionary<string, List<TransitionInfo>> Transitions { get; }
        private string CurrentState { get;set;}
        
        private class TransitionInfo
        {
            public string SourceState { get; }
            public string TargetState { get; }
            
            public TransitionInfo(string sourceState, string targetState)
            {
                SourceState = sourceState;
                TargetState = targetState;
            }
        }
        
        public StateMachine(string defaultState)
        {
            CurrentState = defaultState;
            States = new Dictionary<string, IState>();
            Transitions = new Dictionary<string, List<TransitionInfo>>();
        }

        public void RegisterState(IState state, string name)
        {
            if (States.ContainsKey(name))
                throw new ArgumentException($"State with name {name} is already registered");

            States.Add(name, state);
            state.StateMachine = this;
            state.Setup();
        }
       
        public void RegisterTransition(string triggerName, string sourceState, string targetState)
        {
            if (Transitions.ContainsKey(triggerName))
            {
                if (Transitions[triggerName].Exists(x => x.SourceState == sourceState && x.TargetState == targetState))
                    throw new ArgumentException($"Transition from state {sourceState} to {targetState} already exists");
                
                Transitions[triggerName].Add(new TransitionInfo(sourceState, targetState));   
            }
            else
            {
                Transitions.Add(triggerName, new()
                {
                    new(sourceState, targetState)
                });
            }
        }

        public async UniTask Run()
        {
            await States[CurrentState].Enter();
        }

        public async UniTask<bool> MakeTransition(string triggerName)
        {
            if (Transitions.TryGetValue(triggerName, out var transitionInfos))
            {
                var transitionInfo = transitionInfos.Find(x => x.SourceState == CurrentState);
                if (transitionInfo != null)
                {
                    var currentStateObject = States[CurrentState];
                    var targetStateObject = States[transitionInfo.TargetState];

                    if (currentStateObject == null)
                        throw new ArgumentException($"State object for state {CurrentState} is null");
                    if (targetStateObject == null)
                        throw new ArgumentException($"State object for state {transitionInfo.TargetState} is null");

                    await currentStateObject.Leave();
                    CurrentState = transitionInfo.TargetState;
                    await targetStateObject.Enter();
                }
                else
                {
                    Debug.LogError($"No transition for trigger {triggerName} from current state {CurrentState} exists");
                }
            }
            else
            {
                Debug.LogError($"No transition for trigger {triggerName} exist");
                return false;
            }
            return true;
        }
    }
}