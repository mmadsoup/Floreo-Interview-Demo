using UnityEngine;

namespace StarterAssets.Player.Animation
{
    public abstract class PlayerAnimatorBaseClass : MonoBehaviour
    {
     //private PlayerMovement playerMovement;
     protected Animator _animator;
     protected int _animIDSpeed;
     protected int _animIDGrounded;
     protected int _animIDJump;
     protected int _animIDFreeFall;
     protected int _animIDMotionSpeed;
     protected bool _hasAnimator;
     public float AnimationBlend {get; set;}
    
    public void GetAnimatorComponent()
       {
            _hasAnimator = TryGetComponent(out _animator);
       }
     public void AssignAnimationIDs()
        {
            _animIDSpeed = Animator.StringToHash("Speed");
            _animIDGrounded = Animator.StringToHash("Grounded");
            _animIDJump = Animator.StringToHash("Jump");
            _animIDFreeFall = Animator.StringToHash("FreeFall");
            _animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
        }

        public abstract void GetPlayerMovemenComponent();

        void Start()
        {
            GetAnimatorComponent();
        }

       public abstract void PlayGroundedAnimation();

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
    }
}

