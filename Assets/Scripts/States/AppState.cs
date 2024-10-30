using Game.DevelopmentKit.Services;
using Game.DevelopmentKit.HSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.States
{
    public class AppState : StateMachine
    {
        private IServiceLocator serviceLocator;

        private IntroGameState introGameState;
        private InGameState inGameState;
        private EndGameState endGameState;

        public AppState(IServiceLocator serviceLocator)
        {
            this.serviceLocator = serviceLocator;
            
            introGameState = new IntroGameState(serviceLocator);
            inGameState = new InGameState(serviceLocator);
            endGameState = new EndGameState(serviceLocator);

            AddSubState(introGameState);
            AddSubState(inGameState);
            AddSubState(endGameState);

            AddTransition(introGameState, inGameState, (int)StateTriggers.InGameState);
            AddTransition(inGameState, endGameState, (int)StateTriggers.EndGameState);
            AddTransition(endGameState, introGameState, (int)StateTriggers.IntroGameState);
        }

        protected override void OnEnter()
        {
            Debug.Log("[AppState] OnEnter() called...");
        }

        protected override void OnUpdate()
        {
            Debug.Log("[AppState] OnUpdate() called...");
        }

        protected override void OnExit()
        {
            Debug.Log("[AppState] OnExit() called...");
        }
    }
}