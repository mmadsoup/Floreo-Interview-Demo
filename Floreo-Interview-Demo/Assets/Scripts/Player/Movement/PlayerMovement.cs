using System;
using StarterAssets.Player.Animation;
using StarterAssets.Player.Audio;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM 
using UnityEngine.InputSystem;
using StarterAssets.Player.Camera;
#endif

namespace StarterAssets.Player.Movement
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(PlayerAudio))]
    [RequireComponent(typeof(PlayerAnimation))]
    [RequireComponent(typeof(PlayerCamera))]
#if ENABLE_INPUT_SYSTEM 
    [RequireComponent(typeof(PlayerInput))]
#endif
    public class PlayerMovement : MonoBehaviour, IPlayerController
    {
        public PlayerComponentsSO PlayerComponents;

#if ENABLE_INPUT_SYSTEM 
        private PlayerInput _playerInput;
#endif
        private CharacterController _controller;
        private StarterAssetsInputs _input;
        private GameObject _mainCamera;
        private PlayerAnimation _playerAnimator;
        private PlayerCamera _playerCamera;

        public event Action<CharacterController> OnFootStepped;
        public event Action<CharacterController> OnPlayerLanded;

        private float speed;
        
        private float targetRotation = 0.0f;
        private float rotationVelocity;
        private float verticalVelocity;
        private float terminalVelocity = 53.0f;

        // timeout deltatime
        private float jumpTimeoutDelta;
        private float fallTimeoutDelta;
        private bool IsCurrentDeviceMouse
        {
            get
            {
#if ENABLE_INPUT_SYSTEM
                return _playerInput.currentControlScheme == "KeyboardMouse";
#else
				return false;
#endif
            }
        }

        private void Awake()
        {
            if (_mainCamera == null)
            {
                _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
            }
        }

        private void Start()
        {
            _playerCamera = GetComponent<PlayerCamera>();
            _playerCamera.InitializeCamera();

            _playerAnimator = GetComponent<PlayerAnimation>();
            _controller = GetComponent<CharacterController>();
            _input = GetComponent<StarterAssetsInputs>();
            _playerAnimator.AssignAnimationIDs();
#if ENABLE_INPUT_SYSTEM 
            _playerInput = GetComponent<PlayerInput>();
            
#else       
			Debug.LogError( "Starter Assets package is missing dependencies. Please use Tools/Starter Assets/Reinstall Dependencies to fix it");
#endif
            jumpTimeoutDelta = PlayerComponents.JumpTimeout;
            fallTimeoutDelta = PlayerComponents.FallTimeout;
        }

        private void Update()
        {
            _playerAnimator.GetAnimatorComponent();
            JumpAndGravity();
            GroundedCheck();
            Move();
        }

        private void LateUpdate()
        {
            _playerCamera.RotateCamera(_input.look);
        }

        
        public void GroundedCheck()
        {
            Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - PlayerComponents.GroundedOffset,
                transform.position.z);
            PlayerComponents.Grounded = Physics.CheckSphere(spherePosition, PlayerComponents.GroundedRadius, PlayerComponents.GroundLayers,
                QueryTriggerInteraction.Ignore);

            _playerAnimator.PlayGroundedAnimation();
        }


        public void Move()
        {
            float targetSpeed = _input.sprint ? PlayerComponents.SprintSpeed : PlayerComponents.MoveSpeed;

            if (_input.move == Vector2.zero) targetSpeed = 0.0f;

            float currentHorizontalSpeed = new Vector3(_controller.velocity.x, 0.0f, _controller.velocity.z).magnitude;

            float speedOffset = 0.1f;
            float inputMagnitude = _input.analogMovement ? _input.move.magnitude : 1f;

            if (currentHorizontalSpeed < targetSpeed - speedOffset ||
                currentHorizontalSpeed > targetSpeed + speedOffset)
            {
                speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude,
                    Time.deltaTime * PlayerComponents.SpeedChangeRate);

                speed = Mathf.Round(speed * 1000f) / 1000f;
            }
            else
            {
                speed = targetSpeed;
            }

            _playerAnimator.AnimationBlend = Mathf.Lerp(_playerAnimator.AnimationBlend, targetSpeed, Time.deltaTime * PlayerComponents.SpeedChangeRate);
            if (_playerAnimator.AnimationBlend < 0.01f) _playerAnimator.AnimationBlend = 0f;

            Vector3 inputDirection = new Vector3(_input.move.x, 0.0f, _input.move.y).normalized;

            if (_input.move != Vector2.zero)
            {
                targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg +
                                  _mainCamera.transform.eulerAngles.y;
                float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref rotationVelocity,
                    PlayerComponents.RotationSmoothTime);

                transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
            }


            Vector3 targetDirection = Quaternion.Euler(0.0f, targetRotation, 0.0f) * Vector3.forward;

            _controller.Move(targetDirection.normalized * (speed * Time.deltaTime) +
                             new Vector3(0.0f, verticalVelocity, 0.0f) * Time.deltaTime);

            _playerAnimator.UpdateAnimator(_playerAnimator.AnimationBlend, inputMagnitude);
        }

        public void JumpAndGravity()
        {
            if (PlayerComponents.Grounded)
            {
                fallTimeoutDelta = PlayerComponents.FallTimeout;

                _playerAnimator.PlayJumpAndFallAnimation();

                if (verticalVelocity < 0.0f)
                {
                    verticalVelocity = -2f;
                }

                if (_input.jump && jumpTimeoutDelta <= 0.0f)
                {
                    verticalVelocity = Mathf.Sqrt(PlayerComponents.JumpHeight * -2f * PlayerComponents.Gravity);

                    _playerAnimator.PlayJumpAnimation();
                }

                if (jumpTimeoutDelta >= 0.0f)
                {
                    jumpTimeoutDelta -= Time.deltaTime;
                }
            }
            else
            {
                jumpTimeoutDelta = PlayerComponents.JumpTimeout;

                if (fallTimeoutDelta >= 0.0f)
                {
                    fallTimeoutDelta -= Time.deltaTime;
                }
                else
                {
                    _playerAnimator.PlayFallAnimation();
                }

                _input.jump = false;
            }

            if (verticalVelocity < terminalVelocity)
            {
                verticalVelocity += PlayerComponents.Gravity * Time.deltaTime;
            }
        }

        private void OnFootstep(AnimationEvent animationEvent)
        {
            if (animationEvent.animatorClipInfo.weight > 0.5f)
            {
               OnFootStepped?.Invoke(_controller);
            }
        }

        private void OnLand(AnimationEvent animationEvent)
        {
            if (animationEvent.animatorClipInfo.weight > 0.5f)
            {
                OnPlayerLanded?.Invoke(_controller);
            }
        }
    }
}