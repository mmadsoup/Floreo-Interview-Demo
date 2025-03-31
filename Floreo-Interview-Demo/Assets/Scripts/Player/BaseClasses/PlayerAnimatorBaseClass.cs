using UnityEngine;

public abstract class PlayerAnimatorBaseClass : MonoBehaviour
{
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

        
       public abstract void PlayGroundedAnimation();
       public abstract void PlayJumpAndFallAnimation();

       public abstract void PlayJumpAnimation();
       
       public abstract void UpdateAnimator(float animBlend, float inputMagnitude);

       public abstract void PlayFallAnimation();
    
}
