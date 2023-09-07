#nullable enable

using Shared;
using System;
using UnityEngine;

namespace Gameplay
{
    public class GameplayManager
    {
        public event Action? WhenGameEnded;
        public float TimeLeft => Mathf.Max(0f, gameEndTime - Time.time);

        private readonly UnityLifeCycle unityLifeCycle;
        private readonly float gameEndTime;

        public GameplayManager(UnityLifeCycle unityLifeCycle, GameplayConfig gameplayConfig)
        {
            this.unityLifeCycle = unityLifeCycle ?? throw new System.ArgumentNullException(nameof(unityLifeCycle));

            gameEndTime = Time.time + gameplayConfig.GameSessionDurationS;
            unityLifeCycle.WhenUpdate += OnUpdate;

            Cursor.lockState = CursorLockMode.Locked;
        }

        private void OnUpdate()
        {
            if(gameEndTime < Time.time)
            {
                unityLifeCycle.WhenUpdate -= OnUpdate;
                WhenGameEnded?.Invoke();
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }
}