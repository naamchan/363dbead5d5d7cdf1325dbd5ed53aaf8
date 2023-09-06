#nullable enable

using UnityEngine;

namespace Gameplay
{
    public class PlayerCharacterCameraController
    {
        private PlayerCharacterControllerConfig config;
        private readonly Transform characterCameraPivotTransform;

        private float yawDegree = 0f;
        private float pitchDegree = 0f;

        public PlayerCharacterCameraController(PlayerCharacterControllerConfig config, Transform characterCameraPivotTransform)
        {
            this.config = config ?? throw new System.ArgumentNullException(nameof(config));
            this.characterCameraPivotTransform = characterCameraPivotTransform ?? throw new System.ArgumentNullException(nameof(characterCameraPivotTransform));

            yawDegree = characterCameraPivotTransform.rotation.y;
            pitchDegree = characterCameraPivotTransform.rotation.x;
        }

        public void OnInput(PlayerInputData inputData, float dt)
        {
            var newYawDegree = yawDegree + (inputData.MousePositionDelta.x * config.RotationSpeed * dt);
            var newPitchDegree = pitchDegree + (-inputData.MousePositionDelta.y * config.RotationSpeed * dt);
            newPitchDegree = Mathf.Clamp(newPitchDegree, config.MinCameraPitch, config.MaxCameraPitch);

            characterCameraPivotTransform.localRotation = Quaternion.Euler(newPitchDegree, newYawDegree, 0f);

            yawDegree = newYawDegree;
            pitchDegree = newPitchDegree;
        }
    }
}