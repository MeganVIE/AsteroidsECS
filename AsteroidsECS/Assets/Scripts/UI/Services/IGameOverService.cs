using UI.UnityUI;

namespace UI.Services
{
    public interface IGameOverService
    {
        bool IsGameOver { get; }
        bool IsRestartGame { get; }
        void SetPanel(IGameOverPanel panel);
        void GameOver();
    }
}