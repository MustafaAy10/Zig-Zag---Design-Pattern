using Game.DevelopmentKit;
using Game.DevelopmentKit.ObjectPool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core
{
    public class GroundBase : MonoBehaviour, IPoolable, IDestructible
    {
        private IGroundFactory groundFactory;
        private Rigidbody rb;
        private MeshRenderer meshRenderer;

        public virtual void Initialize()
        {
            rb = GetComponent<Rigidbody>();
            meshRenderer = GetComponent<MeshRenderer>();
        }

        public void InjectGroundFactory(IGroundFactory factory)
        {
            if (groundFactory != null)
                return;

            groundFactory = factory;
        }

        public virtual void Activate()
        {
            PhysicsActivator(false);
            gameObject.SetActive(true);
        }

        public virtual void Deactivate()
        {
            gameObject.SetActive(false);
        }

        public virtual void OnDestruct()
        {
            groundFactory.GetGround();
            Invoke("Fall", 1f);
            Invoke("RemoveFromActiveGrounds", 2.5f);
        }

        private void RemoveFromActiveGrounds()
        {
            groundFactory.RemoveGround(this);
        }

        private void Fall()
        {
            PhysicsActivator(true);
        }

        private void PhysicsActivator(bool useGravity)
        {
            rb.useGravity = useGravity;
            rb.isKinematic = !useGravity;
        }

        public void SetColor(Color color)
        {
            meshRenderer.material.color = color;
        }

    }

}
