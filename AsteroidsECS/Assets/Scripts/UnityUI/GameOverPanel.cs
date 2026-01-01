using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UnityUI
{
    public class GameOverPanel : MonoBehaviour, IGameOverPanel
    {
        [SerializeField] private Button _button;
        [SerializeField] private TextMeshProUGUI _scoreText;
        
        public Action RestartPressed { get; set; }

        public void SetActive(bool value)
        {
            gameObject.SetActive(value);
        }

        public void SetScore(int value) => _scoreText.text = value.ToString();

        private void OnEnable()
        {
            _button.onClick.AddListener(OnRestartPressed);
        }

        private void OnRestartPressed()
        {
            RestartPressed?.Invoke();
        }
    }
}