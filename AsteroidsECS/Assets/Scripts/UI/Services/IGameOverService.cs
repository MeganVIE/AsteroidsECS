using UnityEngine;

namespace UI.Services
{
    public interface IGameOverService
    {
        bool IsGameOver { get; }
        void SetPanel(GameObject panel);
        void GameOver();
    }
}