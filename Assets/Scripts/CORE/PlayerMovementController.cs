using Game.Core;
using Game.DevelopmentKit;
using Game.DevelopmentKit.Services;
using Game.Services;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Core
{
    public class PlayerMovementController : MonoBehaviour, IUpdatable, IInitializable, IDestructible
    {
        [SerializeField] private float speed;
        private float initialSpeed;
        private Vector3 direction;

        private IServiceLocator serviceLocator;
        private InGameInputSystem inputSystem;
        private GamePlayService gamePlayService;
        private Rigidbody rb;

        public void Initialize(IServiceLocator serviceLocator, InGameInputSystem inputSystem)
        {
            this.serviceLocator = serviceLocator;
            gamePlayService = serviceLocator.Get<GamePlayService>(ServiceKeys.GAME_PLAY_SERVICE);
            this.inputSystem = inputSystem;
            initialSpeed = speed;
        }
        public void Init()
        {
            direction = Vector3.forward;
            inputSystem.OnScreenTouchDown += OnScreenTouchDown;
            speed = initialSpeed;
        }

        public void CallUpdate()
        {
            if (transform.position.y < -0.5)
            {
                Debug.Log("[PlayerMovementController] Game is Over...");
                gamePlayService.TriggerGameOver();
            }

            transform.position += direction * speed * Time.deltaTime;
        }

        private void OnScreenTouchDown()
        {
            if (direction == Vector3.forward)
            {
                direction = Vector3.right;
            }
            else
            {
                direction = Vector3.forward;
            }

            speed += 0.01f;
        }

        public void OnDestruct()
        {
            inputSystem.OnScreenTouchDown -= OnScreenTouchDown;

        }
    }
}