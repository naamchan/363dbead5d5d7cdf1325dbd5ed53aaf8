#nullable enable

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

    public class PlayerInput : MonoBehaviour
    {
        private PlayerCharacterMovementController playerCharacterController = default!;
        private PlayerCharacterCameraController playerCharacterCameraController = default!;
        private PlayerCharacterWeaponController playerCharacterWeaponController = default!;

        public void Inject(PlayerCharacterMovementController playerCharacterController, PlayerCharacterCameraController playerCharacterCameraController,
            PlayerCharacterWeaponController playerCharacterWeaponController)
        {
            this.playerCharacterController = playerCharacterController ?? throw new System.ArgumentNullException(nameof(playerCharacterController));
            this.playerCharacterCameraController = playerCharacterCameraController ?? throw new System.ArgumentNullException(nameof(playerCharacterCameraController));
            this.playerCharacterWeaponController = playerCharacterWeaponController ?? throw new System.ArgumentNullException(nameof(playerCharacterWeaponController));
        }

        private void Update()
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