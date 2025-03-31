using UnityEngine;

namespace StarterAssets.Player.Movement
{
    public abstract class PlayerBaseMovement : MonoBehaviour
    {
        protected float speed;
        
        protected float targetRotation = 0.0f;
        protected float rotationVelocity;
        protected float verticalVelocity;
        protected float terminalVelocity = 53.0f;

        // timeout deltatime
        protected float jumpTimeoutDelta;
        protected float fallTimeoutDelta;

        protected abstract void Move();
        protected abstract void GroundedCheck();
        protected abstract void JumpAndGravity();

    }
}
