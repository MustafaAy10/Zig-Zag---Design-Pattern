using Game.DevelopmentKit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core
{
    public class CameraController : MonoBehaviour, IUpdatable
    {
        private PlayerMovementController playerMovementController;
        private Vector3 distance;
        private Vector3 initialPosition;

        public void Initialİze(PlayerMovementController playerMovementController)
        {
            this.playerMovementController = playerMovementController;
            distance = transform.position - playerMovementController.transform.position;
            initialPosition = transform.position;
        }

        public void Init()
        {
            transform.position = initialPosition;
        }

        // Update is called once per frame
        public void CallUpdate()
        {
            transform.position = distance + playerMovementController.transform.position;
        }


    }

}
