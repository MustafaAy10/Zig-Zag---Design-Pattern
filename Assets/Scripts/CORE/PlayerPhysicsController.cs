using Game.DevelopmentKit;
using Game.DevelopmentKit.Services;
using Game.Services;
using Game.UI.View;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

namespace Game.Core
{
    public class PlayerPhysicsController : MonoBehaviour, IService
    {
        private IServiceLocator serviceLocator;
        private GamePlayService gamePlayService;
        private Rigidbody rb;

        //public delegate void ScoreMessageDelegate();
        //public event ScoreMessageDelegate OnScoreChanged;

        private Vector3 initialPosition = new Vector3(0, 1, 0);

        public void Initialize(IServiceLocator serviceLocator)
        {
            this.serviceLocator = serviceLocator;
            gamePlayService = serviceLocator.Get<GamePlayService>(ServiceKeys.GAME_PLAY_SERVICE);
            rb = GetComponent<Rigidbody>();
        }

        public void Init()
        {
            PhysicsActivator(false);
            transform.position = initialPosition;
            PhysicsActivator(true);
        }

        private void PhysicsActivator(bool useGravitiy)
        {
            rb.useGravity = useGravitiy;
            rb.isKinematic = !useGravitiy;
        }

        private void OnCollisionExit(Collision collision)
        {
            var ground = collision.gameObject.GetComponent<GroundBase>();
            if (ground != null)
            {
                ground.OnDestruct();

                gamePlayService.TriggerScoreChanged();
            }
        }
    }
}
