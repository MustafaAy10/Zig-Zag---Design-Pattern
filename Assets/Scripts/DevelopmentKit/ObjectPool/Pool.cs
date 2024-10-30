using System.Collections.Generic;
using UnityEngine;

namespace Game.DevelopmentKit.ObjectPool 
{

    public class Pool<T> 
        where T : IPoolable
    {
        protected Queue<T> pool;
        [SerializeField]
        private GameObject sourceObject;
        private string sourceObjectPath;

        public Queue<T> sourcePool { get { return pool; } }

        private Pool() { }

        public Pool(string sourceObjectPath) 
        {
            this.sourceObjectPath = sourceObjectPath;
            sourceObject = Resources.Load<GameObject>(sourceObjectPath);
            PopulatePool(10);
        }

        public void PopulatePool(int initialPoolSize)
        {
            pool = new Queue<T>();

            for (int i = 0; i < initialPoolSize; i++)
            {
                GenerateObject();
            }
        }

        public void AddToPool(T poolable)
        {
            poolable.Deactivate();
            pool.Enqueue(poolable);
        }

        public T GetFromPool() 
        {
            if (pool.Count == 0) 
            {
                GenerateObject();
            }

            var pooledObject = pool.Dequeue();
            pooledObject.Activate();
            return pooledObject;
        }

        private void GenerateObject() 
        {
            var poolableObject = GameObject.Instantiate(sourceObject).GetComponent<T>();

            if (poolableObject == null)
            {
                //TODO: apply nullable design pattern here!!!
                Debug.LogError("Source prefab does not contain any IPoolable");
                return;
            }

            poolableObject.Initialize();

            pool.Enqueue(poolableObject);
        }

    }
}


