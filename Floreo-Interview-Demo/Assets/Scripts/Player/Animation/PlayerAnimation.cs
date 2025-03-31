using UnityEngine;

namespace StarterAssets.Player.Animation
{
    public class PlayerAnimation : MonoBehaviour
    {
        private Animator _animator;
        private PlayerController playerController;
        private int _animIDSpeed;
        private int _animIDGrounded;
        private int _animIDJump;
        private int _animIDFreeFall;
        private int _animIDMotionSpeed;
        private bool _hasAnimator;
        private float _animationBlend;
        public float AnimationBlend {get; set;}

        void Start()
        {
            GetAnimatorComponent();
            playerController = GetComponent<PlayerController>();
        }
        public void AssignAnimationIDs()
        {
            _animIDSpeed = Animator.StringToHash("Speed");
            _animIDGrounded = Animator.StringToHash("Grounded");
            _animIDJump = Animator.StringToHash("Jump");
            _animIDFreeFall = Animator.StringToHash("FreeFall");
            _animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
        }

       public void PlayGroundedAnimation()
       {
            if (!_hasAnimator) return;
            _animator.SetBool(_animIDGrounded, playerController.Grounded);
       }

       public void PlayJumpAndFallAnimation()
       {
            if (!_hasAnimator) return;
            _animator.SetBool(_animIDJump, false);
            _animator.SetBool(_animIDFreeFall, false);
       }

       public void PlayJumpAnimation()
       {
            if (!_hasAnimator) return;
            _animator.SetBool(_animIDJump, true);
       }
       
       public void UpdateAnimator(float animBlend, float inputMagnitude)
       {
            if (!_hasAnimator) return;
            _animator.SetFloat(_animIDSpeed, animBlend);
            _animator.SetFloat(_animIDMotionSpeed, inputMagnitude);
       }

       public void PlayFallAnimation()
       {
            if (!_hasAnimator) return;
            _animator.SetBool(_animIDFreeFall, true);
       }
       
       public void GetAnimatorComponent()
       {
            _hasAnimator = TryGetComponent(out _animator);
       }
    }
}

