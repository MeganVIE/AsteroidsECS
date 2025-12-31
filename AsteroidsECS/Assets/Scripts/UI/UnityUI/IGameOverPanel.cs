using System;

namespace UI.UnityUI
{
    public interface IGameOverPanel
    {
        Action RestartPressed { get; set; }
        void SetActive(bool value);
    }
}