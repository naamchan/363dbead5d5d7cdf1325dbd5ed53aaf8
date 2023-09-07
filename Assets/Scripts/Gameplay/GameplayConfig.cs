#nullable enable

using System;
using UnityEngine;

namespace Gameplay
{
    [Serializable]
    public class GameplayConfig
    {
        [SerializeField] private float gameSessionDurationS = 60f;
        public float GameSessionDurationS => gameSessionDurationS;
    }
}
