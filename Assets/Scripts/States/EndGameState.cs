using Game.DevelopmentKit.Services;
using Game.DevelopmentKit.HSM;
using Game.Services;
using Game.UI.View;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.States
{
    public class EndGameState : StateMachine
    {
        private IServiceLocator serviceLocator;
        private UIService uiService;
        private EndGameView endGameView;

        private bool isTimeUp = false;
        private const float TIME_TO_WAIT = 1.2f;
        private float enterTime;

        public EndGameState(IServiceLocator serviceLocator)
        {
            this.serviceLocator = serviceLocator;
            uiService = serviceLocator.Get<UIService>(ServiceKeys.UI_SERVICE);
            endGameView = uiService.GetView<EndGameView>(ViewMenu.EndGameView);
        }

        protected override void OnEnter()
        {
            endGameView.OnRestartButtonClick += OnRestart;
            isTimeUp = false;
            enterTime = Time.time;

            Debug.Log("[EndGameState] OnEnter() called...");
        }

        protected override void OnUpdate()
        {
            if(!isTimeUp && Time.time > (TIME_TO_WAIT + enterTime))
            {
                uiService.EnableView(ViewMenu.EndGameView);
                isTimeUp=true;
            }

            Debug.Log("[EndGameState] OnUpdate() called...");
        }

        protected override void OnExit()
        {
            endGameView.OnRestartButtonClick -= OnRestart;
            uiService.DisableView();

            Debug.Log("[EndGameState] OnExit() called...");
        }

        private void OnRestart()
        {
            SendTrigger((int)StateTriggers.IntroGameState);
        }
    }
}