using Game.DevelopmentKit.Services;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Services
{
    public class AccountService : IService
    {
        private int bestScore;
        public void Initialize(IServiceLocator serviceLocator)
        {

        }

        public int GetBestScore()
        {
            return PlayerPrefs.GetInt("BestScore"); 
        }

        public void SetBestScore(int newBestScore)
        {
            PlayerPrefs.SetInt("BestScore", newBestScore);
        }
    }
}