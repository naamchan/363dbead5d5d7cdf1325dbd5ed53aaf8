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
        private float nextFireTime = 0f;
        RaycastHit[] hits = new RaycastHit[16];

        public PlayerCharacterWeaponController(PlayerCharacterControllerConfig config, Transform characterCameraPivotTransform)
        {
            this.config = config ?? throw new ArgumentNullException(nameof(config));
            this.characterCameraPivotTransform = characterCameraPivotTransform ?? throw new ArgumentNullException(nameof(characterCameraPivotTransform));
        }

        public void OnInput(PlayerInputData inputData, float currentTime)
        {
            if (!inputData.IsFire || currentTime < nextFireTime)
                return;

            int hitCount = Physics.RaycastNonAlloc(characterCameraPivotTransform.position, characterCameraPivotTransform.forward, hits);
            for(int i = 0; i < hitCount; i++)
            {
                var hit = hits[i];

                if(hit.collider.TryGetComponent<Renderer>(out var renderer))
                {
                    renderer.material.color = new Color(Random.value, Random.value, Random.value);
                }
            }

            nextFireTime = currentTime + config.FireDelay;
        }
    }
}