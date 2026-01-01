using UnityUI;

namespace GameOver.Services
{
    public interface IGameOverService
    {
        bool IsGameOver { get; }
        bool IsRestartGame { get; }
        void SetPanel(IGameOverPanel panel);
        void GameOver(int score);
    }
}