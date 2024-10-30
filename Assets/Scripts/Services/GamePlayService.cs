
using Game.DevelopmentKit.Services;
using Game.DevelopmentKit;
using Game.UI.View;
using UnityEngine;
using Game.Core;

namespace Game.Services
{
    public class GamePlayService : MonoBehaviour, IService, IUpdatable, IDestructible
    {
        private IServiceLocator serviceLocator;

        public delegate void MessageDelegate();
        public event MessageDelegate OnGameOver;
        public event MessageDelegate OnScoreChanged;
        
        private PlayerMovementController playerMovementController;
        private PlayerPhysicsController playerPhysicsController;

        private GroundFactory groundFactory;
        private InGameInputSystem inputSystem;
        private CameraController cameraController;

        public void Initialize(IServiceLocator serviceLocator)
        {
            this.serviceLocator = serviceLocator;
            inputSystem = new InGameInputSystem();
            cameraController = Camera.main.GetComponent<CameraController>();
            
            CreatePlayer();
            CreateInstance();

            cameraController.Initialİze(playerMovementController);
        }

        private void CreatePlayer()
        {
            playerMovementController = FindObjectOfType<PlayerMovementController>();
            playerMovementController.Initialize(serviceLocator, inputSystem);

            playerPhysicsController = playerMovementController.GetComponent<PlayerPhysicsController>();
            playerPhysicsController.Initialize(serviceLocator);
        }

        private void CreateInstance()
        {
            groundFactory = new GroundFactory();

            groundFactory.Initialize(this);

        }

        public void OnEnter()
        {
            OnDestruct();
            Init();
        }

        public void Init()
        {
            groundFactory.Init();
            playerMovementController.Init();
            playerPhysicsController.Init();
            cameraController.Init();
        }

        public void OnExit()
        {

        }

        public void CallUpdate()
        {
            inputSystem.CallUpdate();
            playerMovementController.CallUpdate();
            cameraController.CallUpdate();
        }

        public void OnDestruct()
        {
            groundFactory.OnDestruct();
            playerMovementController.OnDestruct();
        }

        public void TriggerGameOver()
        {
            OnGameOver?.Invoke();
        }

        public void TriggerScoreChanged()
        {
            OnScoreChanged?.Invoke();  
        }

    }
}