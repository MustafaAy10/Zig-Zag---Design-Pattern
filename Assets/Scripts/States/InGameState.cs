 
using Game.DevelopmentKit.Services;
using Game.DevelopmentKit.HSM;
using Game.Services;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.States
{
    public class InGameState : StateMachine
    {
        private IServiceLocator serviceLocator;
        private GamePlayService gamePlayService;

        public InGameState(IServiceLocator serviceLocator)
        {
            this.serviceLocator = serviceLocator;
            gamePlayService = serviceLocator.Get<GamePlayService>(ServiceKeys.GAME_PLAY_SERVICE);
        }

        protected override void OnEnter()
        {
            Debug.Log("[InGameState] OnEnter() called...");

            gamePlayService.OnGameOver += OnGameOver;

        }

        protected override void OnUpdate()
        {
            gamePlayService.CallUpdate();

            Debug.Log("[InGameState] OnUpdate() called...");
        }

        protected override void OnExit()
        {
            gamePlayService.OnExit();

            gamePlayService.OnGameOver -= OnGameOver;

            Debug.Log("[InGameState] OnExit() called...");
        }

        private void OnGameOver()
        {
            SendTrigger((int)StateTriggers.EndGameState);
        }
    }
}