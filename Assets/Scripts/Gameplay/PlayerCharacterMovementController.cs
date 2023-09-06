#nullable enable

using UnityEngine;

namespace Gameplay
{
    public class PlayerCharacterMovementController
    {
        private readonly PlayerCharacterControllerConfig config;
        private readonly CharacterController characterController;
        private readonly Transform characterCameraPivotTransform;

        public PlayerCharacterMovementController(PlayerCharacterControllerConfig config, CharacterController characterController, Transform characterCameraPivotTransform)
        {
            this.config = config ?? throw new System.ArgumentNullException(nameof(config));
            this.characterController = characterController ?? throw new System.ArgumentNullException(nameof(characterController));
            this.characterCameraPivotTransform = characterCameraPivotTransform ?? throw new System.ArgumentNullException(nameof(characterCameraPivotTransform));
        }

        public void OnInput(PlayerInputData inputData, float dt)
        {
            var moveDirection = characterCameraPivotTransform.right * inputData.HorizontalMovement
                + characterCameraPivotTransform.forward * inputData.VerticalMovement;

            moveDirection *= config.MoveSpeed;
            moveDirection.y = config.Gravity;

            characterController.Move(moveDirection * dt);
        }
    }
}