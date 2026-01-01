using System;

namespace UnityUI
{
    public interface IGameOverPanel
    {
        Action RestartPressed { get; set; }
        void SetActive(bool value);
        void SetScore(int value);
    }
}