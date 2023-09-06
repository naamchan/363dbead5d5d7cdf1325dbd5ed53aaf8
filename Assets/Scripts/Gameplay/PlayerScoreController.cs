﻿#nullable enable

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

        public PlayerScoreController(int highscore)
        {
            this.highscore = highscore;
        }

        public int Highscore => highscore;

        public void SetHighscore(int highscore)
        {
            this.highscore = highscore;
            WhenHighscoreUpdated?.Invoke(highscore);

            //
            _ = FirebaseManager.UpdatePlayerHighscore(highscore);
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
    }
}