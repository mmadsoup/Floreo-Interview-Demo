using StarterAssets.Player.Animation;
using UnityEngine;

namespace StarterAssets.Player.Movement
{
    public class PlayerMovementBaseClass : MonoBehaviour
    {
        private float _speed;
        
        private float _targetRotation = 0.0f;
        private float _rotationVelocity;
        private float _verticalVelocity;
        private float _terminalVelocity = 53.0f;

        // timeout deltatime
        private float _jumpTimeoutDelta;
        private float _fallTimeoutDelta;

        private float _speedOffset = 0.1f;
        float _inputMagnitude;
        
        public void InitMovement(PlayerComponentsSO playerComponents)
        {
            _jumpTimeoutDelta = playerComponents.JumpTimeout;
            _fallTimeoutDelta = playerComponents.FallTimeout;
        }

        private void GroundedCheckLogic(PlayerComponentsSO playerComponents)
        {
            Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - playerComponents.GroundedOffset,
                transform.position.z);
            playerComponents.Grounded = Physics.CheckSphere(spherePosition, playerComponents.GroundedRadius, playerComponents.GroundLayers,
                QueryTriggerInteraction.Ignore);

        }

        public void GroundedCheck(PlayerComponentsSO playerComponents, PlayerAnimation playerAnimation)
        {
            GroundedCheckLogic(playerComponents);
            playerAnimation.PlayGroundedAnimation();
        }

        public void GroundedCheck(PlayerComponentsSO playerComponents, MultiplayerAnimation playerAnimation)
        {
            GroundedCheckLogic(playerComponents);
            playerAnimation.PlayGroundedAnimation();
        }

        private void MoveLogic(float currentHorizontalSpeed, float targetSpeed, PlayerComponentsSO playerComponents, StarterAssetsInputs input, CharacterController controller)
        {
           

            if (currentHorizontalSpeed < targetSpeed - _speedOffset ||
                currentHorizontalSpeed > targetSpeed + _speedOffset)
            {
                _speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * _inputMagnitude,
                    Time.deltaTime * playerComponents.SpeedChangeRate);

                _speed = Mathf.Round(_speed * 1000f) / 1000f;
            }
            else
            {
                _speed = targetSpeed;
            }

        }

        private void RotationLogic(PlayerComponentsSO playerComponents, StarterAssetsInputs input, GameObject mainCamera)
        {
             Vector3 inputDirection = new Vector3(input.move.x, 0.0f, input.move.y).normalized;

            if (input.move != Vector2.zero)
            {
                _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg +
                                  mainCamera.transform.eulerAngles.y;
                float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity,
                    playerComponents.RotationSmoothTime);

                transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
            }


            
        }

        public void Move(PlayerComponentsSO playerComponents, StarterAssetsInputs input, CharacterController controller, GameObject mainCamera, PlayerAnimation playerAnimation) 
        {
            float targetSpeed = input.sprint ? playerComponents.SprintSpeed : playerComponents.MoveSpeed;
             if (input.move == Vector2.zero) targetSpeed = 0.0f;

            float currentHorizontalSpeed = new Vector3(controller.velocity.x, 0.0f, controller.velocity.z).magnitude;
            _inputMagnitude = input.analogMovement ? input.move.magnitude : 1f;
            
            MoveLogic(currentHorizontalSpeed, targetSpeed, playerComponents, input, controller);

            playerAnimation.AnimationBlend = Mathf.Lerp(playerAnimation.AnimationBlend, targetSpeed, Time.deltaTime * playerComponents.SpeedChangeRate);
            if (playerAnimation.AnimationBlend < 0.01f) playerAnimation.AnimationBlend = 0f;

           RotationLogic(playerComponents, input, mainCamera);
            Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;
            
            controller.Move(targetDirection.normalized * (_speed * Time.deltaTime) +
                             new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);

            playerAnimation.UpdateAnimator(playerAnimation.AnimationBlend, _inputMagnitude);
        }

        public void Move(PlayerComponentsSO playerComponents, StarterAssetsInputs input, CharacterController controller, GameObject mainCamera, MultiplayerAnimation playerAnimation) 
        {
            float targetSpeed = input.sprint ? playerComponents.SprintSpeed : playerComponents.MoveSpeed;
             if (input.move == Vector2.zero) targetSpeed = 0.0f;

            float currentHorizontalSpeed = new Vector3(controller.velocity.x, 0.0f, controller.velocity.z).magnitude;
            _inputMagnitude = input.analogMovement ? input.move.magnitude : 1f;
            
            MoveLogic(currentHorizontalSpeed, targetSpeed, playerComponents, input, controller);

            playerAnimation.AnimationBlend = Mathf.Lerp(playerAnimation.AnimationBlend, targetSpeed, Time.deltaTime * playerComponents.SpeedChangeRate);
            if (playerAnimation.AnimationBlend < 0.01f) playerAnimation.AnimationBlend = 0f;

           RotationLogic(playerComponents, input, mainCamera);
            Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;
            
            controller.Move(targetDirection.normalized * (_speed * Time.deltaTime) +
                             new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);

            playerAnimation.UpdateAnimator(playerAnimation.AnimationBlend, _inputMagnitude);
        }

        public void JumpAndGravityLogic(PlayerComponentsSO playerComponents, StarterAssetsInputs input)
        {
             if (playerComponents.Grounded)
            {
                _fallTimeoutDelta = playerComponents.FallTimeout;

                if (_verticalVelocity < 0.0f)
                {
                    _verticalVelocity = -2f;
                }

                if (input.jump && _jumpTimeoutDelta <= 0.0f)
                {
                    _verticalVelocity = Mathf.Sqrt(playerComponents.JumpHeight * -2f * playerComponents.Gravity);
                }

                if (_jumpTimeoutDelta >= 0.0f)
                {
                    _jumpTimeoutDelta -= Time.deltaTime;
                }
            }
            else
            {
                _jumpTimeoutDelta = playerComponents.JumpTimeout;

                if (_fallTimeoutDelta >= 0.0f)
                {
                    _fallTimeoutDelta -= Time.deltaTime;
                }

                input.jump = false;
            }

            if (_verticalVelocity < _terminalVelocity)
            {
                _verticalVelocity += playerComponents.Gravity * Time.deltaTime;
            }

        }

        public void JumpAndGravity(PlayerComponentsSO playerComponents, StarterAssetsInputs input, PlayerAnimation playerAnimation)
        {
            JumpAndGravityLogic(playerComponents, input);
           if (playerComponents.Grounded)
           {
                playerAnimation.PlayJumpAndFallAnimation();

                if (input.jump && _jumpTimeoutDelta <= 0.0f)
                {

                    playerAnimation.PlayJumpAnimation();
                }
           } 
           else
           {
                if (_fallTimeoutDelta <= 0.0f)
                {
                    playerAnimation.PlayFallAnimation();
                }
           }
        }

        public void JumpAndGravity(PlayerComponentsSO playerComponents, StarterAssetsInputs input, MultiplayerAnimation playerAnimation)
        {
             JumpAndGravityLogic(playerComponents, input);
           if (playerComponents.Grounded)
           {
                playerAnimation.PlayJumpAndFallAnimation();

                if (input.jump && _jumpTimeoutDelta <= 0.0f)
                {
                    playerAnimation.PlayJumpAnimation();
                }
           } 
           else
           {
                if (_fallTimeoutDelta <= 0.0f)
                {
                    playerAnimation.PlayFallAnimation();
                }
           }
        }
    }
}
