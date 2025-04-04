using StarterAssets.Player.Movement;
namespace StarterAssets.Player.Animation
{
public class MultiplayerAnimation : PlayerAnimatorBaseClass
{
     private MultiplayerMovement _playerMovement;
        public override void GetPlayerMovemenComponent()
        {
            _playerMovement = GetComponent<MultiplayerMovement>();
        }

        public override void PlayGroundedAnimation()
        {
            if (!_hasAnimator) return;
            _animator.SetBool(_animIDGrounded, _playerMovement.PlayerComponents.Grounded);
        }

        void Awake()
        {
            GetPlayerMovemenComponent();
        }
}

}
