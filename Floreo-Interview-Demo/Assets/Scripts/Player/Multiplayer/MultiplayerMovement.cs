using System;
using StarterAssets.Player.Animation;
using StarterAssets.Player.Audio;
using UnityEngine;
using Unity.Netcode;
using Cinemachine;


#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
using StarterAssets.Player.Camera;
#endif

namespace StarterAssets.Player.Movement
{
    [RequireComponent (typeof(PlayerMovementBaseClass))]
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(MultiplayerAudio))]
    [RequireComponent(typeof(MultiplayerAnimation))]
    [RequireComponent(typeof(PlayerCamera))]
#if ENABLE_INPUT_SYSTEM 
    [RequireComponent(typeof(PlayerInput))]
#endif
    public class MultiplayerMovement : NetworkBehaviour
    {
        public PlayerComponentsSO PlayerComponents;

#if ENABLE_INPUT_SYSTEM 
        private PlayerInput _playerInput;
#endif
        private CharacterController _controller;
        private StarterAssetsInputs _input;
        private GameObject _mainCamera;
        private MultiplayerAnimation _playerAnimator;
        private PlayerCamera _playerCamera;
        private CinemachineVirtualCamera _virtualCamera;
        private PlayerMovementBaseClass _playerMovementBase;

        public event Action<CharacterController> OnFootStepped;
        public event Action<CharacterController> OnPlayerLanded;

        private float _speed;
        
        private float _targetRotation = 0.0f;
        private float _rotationVelocity;
        private float _verticalVelocity;
        private float _terminalVelocity = 53.0f;

        // timeout deltatime
        private float _jumpTimeoutDelta;
        private float _fallTimeoutDelta;


        
        private void Awake()
        {
            if (_mainCamera == null)
            {
                _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
            }

            if (_virtualCamera == null)
            {
                _virtualCamera = FindFirstObjectByType<CinemachineVirtualCamera>();
            }
        }

        private void Start()
        {
            _playerCamera = GetComponent<PlayerCamera>();
            _playerCamera.InitializeCamera();

            _playerAnimator = GetComponent<MultiplayerAnimation>();
            _controller = GetComponent<CharacterController>();
            _input = GetComponent<StarterAssetsInputs>();
            _playerAnimator.AssignAnimationIDs();
            
            _playerMovementBase = GetComponent<PlayerMovementBaseClass>();
            _playerMovementBase.InitMovement(PlayerComponents);
        }

        public override void OnNetworkSpawn()
        {
            if (IsClient && IsOwner)
            {
                _playerInput = GetComponent<PlayerInput>();
                _playerInput.enabled = true;
                _virtualCamera.Follow = transform.GetChild(0);
            }
        }

        private void Update()
        {
            if (!IsOwner) return;
            _playerAnimator.GetAnimatorComponent();
            _playerMovementBase.JumpAndGravity(PlayerComponents, _input, _playerAnimator);
            _playerMovementBase.GroundedCheck(PlayerComponents, _playerAnimator);
            _playerMovementBase.Move(PlayerComponents, _input, _controller, _mainCamera, _playerAnimator);
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
            float target_speed = _input.sprint ? PlayerComponents.SprintSpeed : PlayerComponents.MoveSpeed;

            if (_input.move == Vector2.zero) target_speed = 0.0f;

            float currentHorizontal_speed = new Vector3(_controller.velocity.x, 0.0f, _controller.velocity.z).magnitude;

            float _speedOffset = 0.1f;
            float inputMagnitude = _input.analogMovement ? _input.move.magnitude : 1f;

            if (currentHorizontal_speed < target_speed - _speedOffset ||
                currentHorizontal_speed > target_speed + _speedOffset)
            {
                _speed = Mathf.Lerp(currentHorizontal_speed, target_speed * inputMagnitude,
                    Time.deltaTime * PlayerComponents.SpeedChangeRate);

                _speed = Mathf.Round(_speed * 1000f) / 1000f;
            }
            else
            {
                _speed = target_speed;
            }

            _playerAnimator.AnimationBlend = Mathf.Lerp(_playerAnimator.AnimationBlend, target_speed, Time.deltaTime * PlayerComponents.SpeedChangeRate);
            if (_playerAnimator.AnimationBlend < 0.01f) _playerAnimator.AnimationBlend = 0f;

            Vector3 inputDirection = new Vector3(_input.move.x, 0.0f, _input.move.y).normalized;

            if (_input.move != Vector2.zero)
            {
                _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg +
                                  _mainCamera.transform.eulerAngles.y;
                float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity,
                    PlayerComponents.RotationSmoothTime);

                transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
            }


            Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;

            _controller.Move(targetDirection.normalized * (_speed * Time.deltaTime) +
                             new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);

            _playerAnimator.UpdateAnimator(_playerAnimator.AnimationBlend, inputMagnitude);
        }

        public void JumpAndGravity()
        {
            if (PlayerComponents.Grounded)
            {
                _fallTimeoutDelta = PlayerComponents.FallTimeout;

                _playerAnimator.PlayJumpAndFallAnimation();

                if (_verticalVelocity < 0.0f)
                {
                    _verticalVelocity = -2f;
                }

                if (_input.jump && _jumpTimeoutDelta <= 0.0f)
                {
                    _verticalVelocity = Mathf.Sqrt(PlayerComponents.JumpHeight * -2f * PlayerComponents.Gravity);

                    _playerAnimator.PlayJumpAnimation();
                }

                if (_jumpTimeoutDelta >= 0.0f)
                {
                    _jumpTimeoutDelta -= Time.deltaTime;
                }
            }
            else
            {
                _jumpTimeoutDelta = PlayerComponents.JumpTimeout;

                if (_fallTimeoutDelta >= 0.0f)
                {
                    _fallTimeoutDelta -= Time.deltaTime;
                }
                else
                {
                    _playerAnimator.PlayFallAnimation();
                }

                _input.jump = false;
            }

            if (_verticalVelocity < _terminalVelocity)
            {
                _verticalVelocity += PlayerComponents.Gravity * Time.deltaTime;
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