using UnityUI;

namespace GameOver.Services
{
    public class GameOverService : IGameOverService
    {
        private IGameOverPanel _panel;

        public bool IsGameOver { get; private set; }

        public bool IsRestartGame { get; private set; }

        public void SetPanel(IGameOverPanel panel)
        {
            _panel = panel;
            _panel.SetActive(false);
            _panel.RestartPressed += OnRestartPressed;
            IsGameOver = false;
            IsRestartGame = false;
        }

        public void GameOver(int score)
        {
            IsGameOver = true;
            _panel.SetActive(true);
            _panel.SetScore(score);
        }

        private void OnRestartPressed()
        {
            IsRestartGame = true;
            _panel.RestartPressed -= OnRestartPressed;
        }
    }
}