using Game.DevelopmentKit;
using UnityEngine;

namespace Game.Core
{
    public class InGameInputSystem : IUpdatable, IDestructible, IInitializable
    {
        public delegate void TouchMessageDelegate();
        public event TouchMessageDelegate OnScreenTouchDown;

        private bool isInputAvailable = false;

        public void CallUpdate()
        {
            if (Input.GetMouseButtonDown(0))
            {
                // Debug.Log("OnScreenTouch");
                if (OnScreenTouchDown != null)
                {
                    OnScreenTouchDown();
                }
            }
        }

        public void OnDestruct()
        {
            isInputAvailable = false;
        }

        public void Init()
        {
            isInputAvailable = true;
        }


    }
}
