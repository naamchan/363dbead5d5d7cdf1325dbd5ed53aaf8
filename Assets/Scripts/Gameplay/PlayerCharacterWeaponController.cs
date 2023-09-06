#nullable enable

using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gameplay
{
    public class PlayerCharacterWeaponController
    {
        private readonly PlayerCharacterControllerConfig config;
        private readonly Transform characterCameraPivotTransform;
        private readonly PlayerCharacterAnimationController animationController;
        private float nextFireTime = 0f;
        RaycastHit[] hits = new RaycastHit[16];

        public PlayerCharacterWeaponController(PlayerCharacterControllerConfig config,
            Transform characterCameraPivotTransform, PlayerCharacterAnimationController animationController)
        {
            this.config = config ?? throw new ArgumentNullException(nameof(config));
            this.characterCameraPivotTransform = characterCameraPivotTransform ?? throw new ArgumentNullException(nameof(characterCameraPivotTransform));
            this.animationController = animationController ?? throw new ArgumentNullException(nameof(animationController));
        }

        public void OnInput(PlayerInputData inputData, float currentTime)
        {
            if (!inputData.IsFire || currentTime < nextFireTime)
                return;

            int hitCount = Physics.RaycastNonAlloc(characterCameraPivotTransform.position, characterCameraPivotTransform.forward, hits);
            for(int i = 0; i < hitCount; i++)
            {
                var hit = hits[i];

                if(hit.collider.TryGetComponent<IShootTarget>(out var shootTarget))
                {
                    shootTarget.OnShot();
                }
            }

            nextFireTime = currentTime + config.FireDelay;
            animationController.PlayFireAnimation();
        }
    }
}