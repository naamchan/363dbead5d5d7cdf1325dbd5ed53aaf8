#nullable enable

using Shared;
using UnityEngine;

namespace Gameplay
{
    public struct PlayerInputData
    {
        public readonly float HorizontalMovement;
        public readonly float VerticalMovement;
        public readonly bool IsFire;
        public readonly Vector2 MousePositionDelta;

        public PlayerInputData(float horizontalMovement, float verticalMovement, bool isFire, Vector2 mouseDelta)
        {
            HorizontalMovement = horizontalMovement;
            VerticalMovement = verticalMovement;
            IsFire = isFire;
            MousePositionDelta = mouseDelta;
        }
    }

    public class PlayerInputController
    {
        private UnityLifeCycle unityLifeCycle;
        private readonly GameplayManager gameplayManager;
        private PlayerCharacterMovementController playerCharacterController;
        private PlayerCharacterCameraController playerCharacterCameraController;
        private PlayerCharacterWeaponController playerCharacterWeaponController;

        public PlayerInputController(UnityLifeCycle unityLifeCycle,
            GameplayManager gameplayManager,
            PlayerCharacterMovementController playerCharacterController,
            PlayerCharacterCameraController playerCharacterCameraController,
            PlayerCharacterWeaponController playerCharacterWeaponController)
        {
            this.unityLifeCycle = unityLifeCycle ?? throw new System.ArgumentNullException(nameof(unityLifeCycle));
            this.gameplayManager = gameplayManager ?? throw new System.ArgumentNullException(nameof(gameplayManager));
            this.playerCharacterController = playerCharacterController ?? throw new System.ArgumentNullException(nameof(playerCharacterController));
            this.playerCharacterCameraController = playerCharacterCameraController ?? throw new System.ArgumentNullException(nameof(playerCharacterCameraController));
            this.playerCharacterWeaponController = playerCharacterWeaponController ?? throw new System.ArgumentNullException(nameof(playerCharacterWeaponController));

            gameplayManager.WhenGameEnded += OnGameEnded;
            Enable();
        }

        public void Enable()
        {
            unityLifeCycle.WhenUpdate += OnUpdate;
        }

        public void Disable()
        {
            unityLifeCycle.WhenUpdate -= OnUpdate;
        }

        private void OnGameEnded()
        {
            Disable();
        }

        private void OnUpdate()
        {
            PlayerInputData inputData = new PlayerInputData(
                Input.GetAxis("Horizontal"),
                Input.GetAxis("Vertical"),
                Input.GetButton("Fire1"),
                new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"))
            );

            playerCharacterCameraController.OnInput(inputData, Time.deltaTime);
            playerCharacterController.OnInput(inputData, Time.deltaTime);
            playerCharacterWeaponController.OnInput(inputData, Time.time);
        }
    }
}