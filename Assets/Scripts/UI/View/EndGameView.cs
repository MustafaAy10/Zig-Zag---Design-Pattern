using Game.DevelopmentKit.Services;
using Game.Services;
using Game.UI.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;

namespace Game.UI.View
{
    public class EndGameView : ViewBase
    {
        public delegate void MessageDelegate();
        public event MessageDelegate OnRestartButtonClick;

        public TextMeshProUGUI scoreText;
        public TextMeshProUGUI bestScoreText;

        private InGameView inGameView;

        public override ViewMenu viewMenu => ViewMenu.EndGameView;

        public override void Initialize(IServiceLocator serviceLocator)
        {
            base.Initialize(serviceLocator);
            inGameView = serviceLocator.Get<UIService>(ServiceKeys.UI_SERVICE).GetView<InGameView>(ViewMenu.InGameView);
        }

        public override void Activate()
        {
            base.Activate();
            UpdateScores();
        }

        public void RequestRestartLevel()
        {
            if(OnRestartButtonClick != null)
                OnRestartButtonClick();
        }

        private void UpdateScores()
        {
            inGameView.GetScores(out int score, out int bestScore);
            scoreText.text = score.ToString();
            bestScoreText.text = bestScore.ToString();
        }
    }
}