#nullable enable

using System;
using UnityEngine;

namespace Gameplay
{
    [Serializable]
    public class PlayerCharacterControllerConfig
    {
        [SerializeField] private float moveSpeed = 1f;
        public float MoveSpeed => moveSpeed;

        [SerializeField] private float rotationSpeed = 1f;
        public float RotationSpeed => rotationSpeed;

        [SerializeField] private float gravity = -9.8f;
        public float Gravity => gravity;

        [SerializeField] private float fireDelay = 0.2f;
        public float FireDelay => fireDelay;

        [SerializeField] private float minCameraPitch = -180f;
        public float MinCameraPitch => minCameraPitch;

        [SerializeField] private float maxCameraPitch = 180f;
        public float MaxCameraPitch => maxCameraPitch;
    }

    [CreateAssetMenu(menuName = "Gameplay/Player Character Controller Config")]
    public class PlayerCharacterControllerConfigScriptable : ScriptableObject
    {
        [SerializeField] private PlayerCharacterControllerConfig config = default!;
        public PlayerCharacterControllerConfig Config => config;
    }
}