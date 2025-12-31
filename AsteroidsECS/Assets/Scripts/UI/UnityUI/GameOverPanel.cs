using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.UnityUI
{
    public class GameOverPanel : MonoBehaviour, IGameOverPanel
    {
        [SerializeField] private Button _button;
        
        public Action RestartPressed { get; set; }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnRestartPressed);
        }

        private void OnRestartPressed()
        {
            RestartPressed?.Invoke();
        }

        public void SetActive(bool value)
        {
            gameObject.SetActive(value);
        }
    }
}