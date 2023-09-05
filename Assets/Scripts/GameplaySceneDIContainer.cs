#nullable enable

using UnityEngine;

namespace Gameplay
{

    [DefaultExecutionOrder(-9000)]
    public class GameplaySceneDIContainer : MonoBehaviour
    {
        [SerializeField] private CharacterController characterController = default!;
        [SerializeField] private PlayerInput playerInput = default!;
        [SerializeField] private PlayerCharacterControllerConfigScriptable characterControllerConfigScriptable = default!;
        [SerializeField] private Transform cameraArmTransform = default!;

        private void Awake()
        {
            var playerCharacterController = new PlayerCharacterMovementController(characterControllerConfigScriptable.Config,
                characterController, cameraArmTransform);
            var playerCharacterCameraController = new PlayerCharacterCameraController(characterControllerConfigScriptable.Config,
                cameraArmTransform);
            var playerCharacterWeaponController = new PlayerCharacterWeaponController(characterControllerConfigScriptable.Config, cameraArmTransform);

            playerInput.Inject(playerCharacterController, playerCharacterCameraController, playerCharacterWeaponController);

            // TODO: Find a place for this
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}