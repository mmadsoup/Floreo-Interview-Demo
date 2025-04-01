using UnityEngine;
using StarterAssets.Player.Movement;

namespace StarterAssets.Player.Animation
{
    public class PlayerAnimation : PlayerAnimatorBaseClass
    {
     private PlayerMovement playerMovement;
        public override void GetPlayerMovemenComponent()
        {
            playerMovement = GetComponent<PlayerMovement>();
        }

        public override void PlayGroundedAnimation()
        {
            if (!_hasAnimator) return;
            _animator.SetBool(_animIDGrounded, playerMovement.PlayerComponents.Grounded);
        }

        void Awake()
        {
            GetPlayerMovemenComponent();
        }
    }
}

