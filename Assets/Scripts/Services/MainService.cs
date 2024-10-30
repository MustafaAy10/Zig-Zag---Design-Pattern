using Game.DevelopmentKit.Services;
using Game.States;
using UnityEngine;

namespace Game.Services
{
    public class MainService : MonoBehaviour
    {
        private AppState appState;

        private GamePlayService gamePlayService;
        private UIService uiService;
        private AccountService accountService;

        private IServiceLocator serviceLocator;

        void Awake()
        {
            serviceLocator = new ServiceLocator();

            CreateUIService();
            CreateGamePlayService();
            CreateAccountService();

            InitializeAllServices();

            CreateAppState();

            appState.Enter();

        }

        private void CreateUIService()
        {
            uiService = FindObjectOfType<UIService>();
            serviceLocator.Add(ServiceKeys.UI_SERVICE, uiService);
        }

        private void CreateGamePlayService()
        {
            gamePlayService = FindObjectOfType<GamePlayService>();
            serviceLocator.Add(ServiceKeys.GAME_PLAY_SERVICE, gamePlayService);
        }

        private void CreateAccountService()
        {
            accountService = new AccountService();
            serviceLocator.Add(ServiceKeys.ACCOUNT_SERVICE, accountService);
        }

        private void InitializeAllServices()
        {
            uiService.Initialize(serviceLocator);
            accountService.Initialize(serviceLocator);
            gamePlayService.Initialize(serviceLocator);
        }

        private void CreateAppState()
        {
            appState = new AppState(serviceLocator);
        }

        private void Update()
        {
            appState.Update();
        }
    }
}