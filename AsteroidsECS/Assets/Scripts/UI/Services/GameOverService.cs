using UnityEngine;

namespace UI.Services
{
    public class GameOverService : IGameOverService
    {
        private GameObject _panel;
        
        public bool IsGameOver { get; private set; }

        public void SetPanel(GameObject panel)
        {
            _panel = panel;
            _panel.SetActive(false);
            IsGameOver = false;
        }

        public void GameOver()
        {
            IsGameOver = true;
            _panel.SetActive(true);
        }
    }
}