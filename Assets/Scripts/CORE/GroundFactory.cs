using Game.DevelopmentKit.ObjectPool;
using Game.Services;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core
{
    public class GroundFactory : IGroundFactory
    {
        private Vector3 lastPosition;
        private Vector3 initialPosition = new Vector3(0, 0, -1);
        private const string SOURCE_PATH = "Ground/Ground";
        private Pool<GroundBase> groundPool;
        private List<GroundBase> activeGrounds;

        private GamePlayService gamePlayService;

        private int lastColorIndex = 0, point = 0;
        private Color[] colors = new Color[] { Color.white, Color.cyan, Color.green, Color.yellow };

        public void Initialize(GamePlayService gamePlayService)
        {
            this.gamePlayService = gamePlayService;

            groundPool = new Pool<GroundBase>(SOURCE_PATH);
            activeGrounds = new List<GroundBase>();

        }

        public void Init()
        {
            lastPosition = initialPosition;
            lastColorIndex = 0;
            point = 0;

            gamePlayService.OnScoreChanged += OnScoreChanged;

            for (int i = 0; i < 3; i++)
            {
                GetInitialGround();
            }

            for (int i = 0; i < 20; i++)
            {
                GetGround();
            }
        }

        public void GetGround()
        {
            Vector3 direction = Random.Range(0, 2) == 0 ? Vector3.forward : Vector3.right;
            Vector3 spawnPosition = lastPosition + direction;
            lastPosition = spawnPosition;

            CreateGround(spawnPosition);
        }

        private void GetInitialGround()
        {
            Vector3 spawnPosition = lastPosition + Vector3.forward;
            lastPosition = spawnPosition;

            CreateGround(spawnPosition);
        }

        private void CreateGround(Vector3 spawnPosition)
        {
            var ground = groundPool.GetFromPool();
            ground.InjectGroundFactory(this);
            ground.transform.position = spawnPosition;
            ground.SetColor(colors[lastColorIndex]);

            activeGrounds.Add(ground);
        }

        public void RemoveGround(GroundBase ground)
        {
            ground.CancelInvoke();
            activeGrounds.Remove(ground);
            groundPool.AddToPool(ground);
        }

        private void OnScoreChanged()
        {
            point += 10;
            
            if (point%100 == 0)
            {
                lastColorIndex++;
                
                if (lastColorIndex == colors.Length)
                { 
                    lastColorIndex = 0;
                }

                ChangeColorAll();

            }
        }

        private void ChangeColorAll()
        {
            foreach (var active in activeGrounds)
            {
                active.SetColor(colors[lastColorIndex]);
            }

            foreach(var ground in groundPool.sourcePool)
            {
                ground.SetColor(colors[lastColorIndex]);
            }
        }

        public void OnDestruct()
        {
            for (int i = activeGrounds.Count - 1; i >= 0; i--)
            {
                RemoveGround(activeGrounds[i]);
            }

            activeGrounds.Clear();

            gamePlayService.OnScoreChanged -= OnScoreChanged;

        }
    }
}
