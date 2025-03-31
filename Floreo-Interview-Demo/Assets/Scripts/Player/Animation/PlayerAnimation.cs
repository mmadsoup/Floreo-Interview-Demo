using UnityEngine;
using StarterAssets.Player.Movement;

namespace StarterAssets.Player.Animation
{
    public class PlayerAnimation : PlayerAnimatorBaseClass
    {
        private PlayerMovement playerMovement;

        void Start()
        {
            GetAnimatorComponent();
            playerMovement = GetComponent<PlayerMovement>();
        }
       

       public override void PlayGroundedAnimation()
       {
            if (!_hasAnimator) return;
            _animator.SetBool(_animIDGrounded, playerMovement.PlayerComponents.Grounded);
       }

       public override void PlayJumpAndFallAnimation()
       {
            if (!_hasAnimator) return;
            _animator.SetBool(_animIDJump, false);
            _animator.SetBool(_animIDFreeFall, false);
       }

       public override void PlayJumpAnimation()
       {
            if (!_hasAnimator) return;
            _animator.SetBool(_animIDJump, true);
       }
       
       public override void UpdateAnimator(float animBlend, float inputMagnitude)
       {
            if (!_hasAnimator) return;
            _animator.SetFloat(_animIDSpeed, animBlend);
            _animator.SetFloat(_animIDMotionSpeed, inputMagnitude);
       }

       public override void PlayFallAnimation()
       {
            if (!_hasAnimator) return;
            _animator.SetBool(_animIDFreeFall, true);
       }
   
    }
}

