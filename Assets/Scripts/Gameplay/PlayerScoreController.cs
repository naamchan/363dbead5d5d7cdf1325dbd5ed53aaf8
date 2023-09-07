#nullable enable

using Shared;
using System;

namespace Gameplay
{
    public class PlayerScoreController
    {
        public event Action<int>? WhenScoreUpdated;
        public event Action<int>? WhenHighscoreUpdated;

        private int score;
        public int Score => score;

        private int highscore;
        public int Highscore => highscore;

        private int gameStartHighscore;

        public PlayerScoreController(GameplayManager gameplayManager, int highscore)
        {
            gameplayManager.WhenGameEnded += OnGameEnded;
            this.highscore = highscore;
            gameStartHighscore = highscore;
        }

        public void SetHighscore(int highscore)
        {
            this.highscore = highscore;
            WhenHighscoreUpdated?.Invoke(highscore);
        }

        public void AddScore(int scoreToAdd)
        {
            score += scoreToAdd;
            WhenScoreUpdated?.Invoke(score);

            if (highscore < score)
            {
                SetHighscore(score);
            }
        }

        private void OnGameEnded()
        {
            if (highscore > gameStartHighscore)
            {
                _ = FirebaseManager.UpdatePlayerHighscore(highscore);
            }
        }
    }
}