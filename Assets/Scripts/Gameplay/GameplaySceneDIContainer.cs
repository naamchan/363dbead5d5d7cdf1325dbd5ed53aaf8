#nullable enable

using Shared;
using System.Linq;
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
        [SerializeField] private Animator leftWeaponAnimator = default!;
        [SerializeField] private Animator rightWeaponAnimator = default!;
        [SerializeField] private GameplayUI gameplayUI = default!;
        [SerializeField] private ShootTarget[] shootTargets = new ShootTarget[0];

        private void Awake()
        {
            var playerCharacterController = new PlayerCharacterMovementController(characterControllerConfigScriptable.Config,
                characterController, cameraArmTransform);
            var playerCharacterCameraController = new PlayerCharacterCameraController(characterControllerConfigScriptable.Config,
                cameraArmTransform);
            var playerCharacterAnimationController = new PlayerCharacterAnimationController(leftWeaponAnimator, rightWeaponAnimator);
            var playerCharacterWeaponController = new PlayerCharacterWeaponController(characterControllerConfigScriptable.Config,
                cameraArmTransform, playerCharacterAnimationController);
            var playerScoreController = new PlayerScoreController(FirebaseManager.CachedHighscore);

            playerInput.Inject(playerCharacterController, playerCharacterCameraController, playerCharacterWeaponController);
            gameplayUI.Inject(playerScoreController);
            foreach(var shootTarget in shootTargets)
            {
                shootTarget.Inject(playerScoreController);
            }

            // TODO: Find a place for this
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void OnValidate()
        {
            shootTargets = FindObjectsByType<ShootTarget>(FindObjectsSortMode.None);
        }
    }
}