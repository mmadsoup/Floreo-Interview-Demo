using StarterAssets.Player.Movement;
namespace StarterAssets.Player.Animation
{
public class MultiplayerAnimation : PlayerAnimatorBaseClass
{
     private MultiplayerMovement playerMovement;
        public override void GetPlayerMovemenComponent()
        {
            playerMovement = GetComponent<MultiplayerMovement>();
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
