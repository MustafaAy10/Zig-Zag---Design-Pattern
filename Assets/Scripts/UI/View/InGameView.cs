using Game.DevelopmentKit.Services;
using UnityEngine;
using Game.DevelopmentKit;
using UnityEngine.UI;
using TMPro;
using Game.Services;


namespace Game.UI.View
{
    public class InGameView : ViewBase, IDestructible
    {
        private int score;
        private int bestScore;

        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI bestScoreText;

        private AccountService accountService;
        private GamePlayService gamePlayService;

        public override ViewMenu viewMenu => ViewMenu.InGameView;

        public override void Initialize(IServiceLocator serviceLocator)
        {
            base.Initialize(serviceLocator);
            Activate();
            accountService = serviceLocator.Get<AccountService>(ServiceKeys.ACCOUNT_SERVICE);
            gamePlayService = serviceLocator.Get<GamePlayService>(ServiceKeys.GAME_PLAY_SERVICE);
            gamePlayService.OnScoreChanged += OnScoreChanged;
            gamePlayService.OnGameOver += CheckBestScore;
        }

        public void OnScoreChanged()
        {
            score += 10;
            UpdateScore();
        }

        public void OnDestruct()
        {
            score = 0;
            bestScore = accountService.GetBestScore();
            UpdateScore();  
            UpdateBestScore();
        }

        public void UpdateScore()
        {
            scoreText.text = score.ToString();
        }

        public void UpdateBestScore()
        {
            bestScoreText.text = bestScore.ToString();
        }

        public void GetScores(out int score, out int bestScore)
        {
            score = this.score;
            bestScore = this.bestScore;
        }

        private void CheckBestScore()
        {
            if(score > bestScore)
            {
                accountService.SetBestScore(score);
            }
        }
    }
}
