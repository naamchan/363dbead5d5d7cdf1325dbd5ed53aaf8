#nullable enable

using System;
using TMPro;
using UnityEngine;

namespace Gameplay
{
    public class GameplayUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText = default!;
        [SerializeField] private TextMeshProUGUI highscoreText = default!;

        public void Inject(PlayerScoreController playerScoreController)
        {
            if (playerScoreController == null)
            {
                throw new ArgumentNullException(nameof(playerScoreController));
            }

            playerScoreController.WhenScoreUpdated += OnScoreUpdated;
            playerScoreController.WhenHighscoreUpdated += OnHighscoreUpdated;

            OnScoreUpdated(playerScoreController.Score);
            OnHighscoreUpdated(playerScoreController.Highscore);
        }

        private void OnScoreUpdated(int score)
        {
            scoreText.text = $"Score: {score}";
        }

        private void OnHighscoreUpdated(int highscore)
        {
            highscoreText.text = $"Highscore: {highscore}";
        }
    }
}