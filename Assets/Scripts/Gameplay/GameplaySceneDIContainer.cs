#nullable enable

using Shared;
using UnityEngine;

namespace Gameplay
{
    [DefaultExecutionOrder(-9000)]
    public class GameplaySceneDIContainer : MonoBehaviour
    {
        [SerializeField] private CharacterController characterController = default!;
        [SerializeField] private GameplayConfigScriptable gameplayConfigScriptable = default!;
        [SerializeField] private PlayerCharacterControllerConfigScriptable characterControllerConfigScriptable = default!;
        [SerializeField] private Transform cameraArmTransform = default!;
        [SerializeField] private Animator leftWeaponAnimator = default!;
        [SerializeField] private Animator rightWeaponAnimator = default!;
        [SerializeField] private GameplayUI gameplayUI = default!;
        [SerializeField] private ShootTarget[] shootTargets = new ShootTarget[0];

        private void Awake()
        {
            var unityLifeCycle = gameObject.AddComponent<UnityLifeCycle>();

            var gameplayManager = new GameplayManager(unityLifeCycle, gameplayConfigScriptable.Config);
            var playerCharacterController = new PlayerCharacterMovementController(characterControllerConfigScriptable.Config,
                characterController, cameraArmTransform);
            var playerCharacterCameraController = new PlayerCharacterCameraController(characterControllerConfigScriptable.Config,
                cameraArmTransform);
            var playerCharacterAnimationController = new PlayerCharacterAnimationController(leftWeaponAnimator, rightWeaponAnimator);
            var playerCharacterWeaponController = new PlayerCharacterWeaponController(characterControllerConfigScriptable.Config,
                cameraArmTransform, playerCharacterAnimationController);
            var playerScoreController = new PlayerScoreController(gameplayManager, FirebaseManager.CachedHighscore);
            var playerInput = new PlayerInputController(
                unityLifeCycle, gameplayManager, playerCharacterController, playerCharacterCameraController, playerCharacterWeaponController);

            gameplayUI.Inject(gameplayManager, playerScoreController);
            foreach (var shootTarget in shootTargets)
            {
                shootTarget.Inject(playerScoreController);
            }
        }

        private void OnValidate()
        {
            shootTargets = FindObjectsByType<ShootTarget>(FindObjectsSortMode.None);
        }
    }


}