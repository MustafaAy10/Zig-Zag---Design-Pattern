 
using Game.DevelopmentKit.Services;
using Game.DevelopmentKit.HSM;
using Game.Services;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.UI.View;

namespace Game.States
{
    public class IntroGameState : StateMachine
    {
        private IServiceLocator serviceLocator;
        private GamePlayService gamePlayService;
        private UIService uiService;
        private InGameView inGameView;

        public IntroGameState(IServiceLocator serviceLocator)
        {
            this.serviceLocator = serviceLocator;
            gamePlayService = serviceLocator.Get<GamePlayService>(ServiceKeys.GAME_PLAY_SERVICE);
            uiService = serviceLocator.Get<UIService>(ServiceKeys.UI_SERVICE);
            inGameView = serviceLocator.Get<UIService>(ServiceKeys.UI_SERVICE).GetView<InGameView>(ViewMenu.InGameView);
        }

        protected override void OnEnter()
        {
            gamePlayService.OnEnter();
            inGameView.OnDestruct();
            uiService.EnableView(Game.UI.View.ViewMenu.IntroGameView);


            Debug.Log("[IntroGameState] OnEnter() called...");
        }

        protected override void OnUpdate()
        {
            // Intro'ya Tap To Play demek için gerek duyduk.
            if (Input.GetMouseButtonDown(0))
            {
                SendTrigger((int)StateTriggers.InGameState);
            }

            Debug.Log("[IntroGameState] OnUpdate() called...");
        }

        protected override void OnExit()
        {
            uiService.DisableView();

            Debug.Log("[IntroGameState] OnExit() called...");
        }
    }
}