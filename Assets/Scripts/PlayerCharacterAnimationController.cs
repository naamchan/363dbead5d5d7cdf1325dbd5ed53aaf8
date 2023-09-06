#nullable enable

using System;
using UnityEngine;

namespace Gameplay
{
    public class PlayerCharacterAnimationController
    {
        private readonly int FireAnimationID = Animator.StringToHash("Fire");
        private readonly Animator leftWeaponAnimator;
        private readonly Animator rightWeaponAnimator;
        private bool usingRight = true;

        public PlayerCharacterAnimationController(Animator leftWeaponAnimator, Animator rightWeaponAnimator)
        {
            this.leftWeaponAnimator = leftWeaponAnimator ?? throw new ArgumentNullException(nameof(leftWeaponAnimator));
            this.rightWeaponAnimator = rightWeaponAnimator ?? throw new ArgumentNullException(nameof(rightWeaponAnimator));
        }

        public void PlayFireAnimation()
        {
            var animator = usingRight ? rightWeaponAnimator : leftWeaponAnimator;
            animator.SetTrigger(FireAnimationID);
            usingRight = !usingRight;
        }
    }
}