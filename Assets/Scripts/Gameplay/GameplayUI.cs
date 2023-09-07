#nullable enable

using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Gameplay
{
    public class GameplayUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI timeLeftText = default!;
        [SerializeField] private TextMeshProUGUI scoreText = default!;
        [SerializeField] private TextMeshProUGUI highscoreText = default!;
        [SerializeField] private GameObject gameoverPanel = default!;
        [SerializeField] private Button restartButton = default!;

        private GameplayManager gameplayManager = default!;

        public void Inject(GameplayManager gameplayManager, PlayerScoreController playerScoreController)
        {
            if (gameplayManager == null)
            {
                throw new ArgumentNullException(nameof(gameplayManager));
            }

            if (playerScoreController == null)
            {
                throw new ArgumentNullException(nameof(playerScoreController));
            }

            this.gameplayManager = gameplayManager;

            gameplayManager.WhenGameEnded += OnGameEnded;
            playerScoreController.WhenScoreUpdated += OnScoreUpdated;
            playerScoreController.WhenHighscoreUpdated += OnHighscoreUpdated;

            OnScoreUpdated(playerScoreController.Score);
            OnHighscoreUpdated(playerScoreController.Highscore);
        }

        private void OnEnable()
        {
            restartButton.onClick.AddListener(OnGameoverButtonClicked);
        }

        private void OnDisable()
        {
            restartButton.onClick.RemoveListener(OnGameoverButtonClicked);
        }

        private void Update()
        {
            timeLeftText.text = gameplayManager.TimeLeft.ToString("0");
        }

        private void OnGameEnded()
        {
            gameoverPanel.SetActive(true);
        }

        private void OnScoreUpdated(int score)
        {
            scoreText.text = $"Score: {score}";
        }

        private void OnHighscoreUpdated(int highscore)
        {
            highscoreText.text = $"Highscore: {highscore}";
        }

        private void OnGameoverButtonClicked()
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}