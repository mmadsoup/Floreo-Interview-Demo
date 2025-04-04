using StarterAssets.Player.Movement;

namespace StarterAssets.Player.Animation
{
    public class PlayerAnimation : PlayerAnimatorBaseClass
    {
     private PlayerMovement _playerMovement;
        public override void GetPlayerMovemenComponent()
        {
            _playerMovement = GetComponent<PlayerMovement>();
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

