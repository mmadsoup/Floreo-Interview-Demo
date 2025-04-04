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
    [RequireComponent (typeof(PlayerMovementBaseClass))]
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(PlayerAudio))]
    [RequireComponent(typeof(PlayerAnimation))]
    [RequireComponent(typeof(PlayerCamera))]
#if ENABLE_INPUT_SYSTEM 
    [RequireComponent(typeof(PlayerInput))]
#endif
    public class PlayerMovement : MonoBehaviour
    {
        public PlayerComponentsSO PlayerComponents;

#if ENABLE_INPUT_SYSTEM 
        private PlayerInput _playerInput;
#endif

        private PlayerMovementBaseClass _playerMovementBase;
        private CharacterController _controller;
        private StarterAssetsInputs _input;
        private GameObject _mainCamera;
        private PlayerAnimation _playerAnimator;
        private PlayerCamera _playerCamera;

        public event Action<CharacterController> OnFootStepped;
        public event Action<CharacterController> OnPlayerLanded;


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
            _playerMovementBase = GetComponent<PlayerMovementBaseClass>();
            _playerMovementBase.InitMovement(PlayerComponents);
        }

        private void Update()
        {
            _playerAnimator.GetAnimatorComponent();
            _playerMovementBase.JumpAndGravity(PlayerComponents, _input, _playerAnimator);
            _playerMovementBase.GroundedCheck(PlayerComponents, _playerAnimator);
            _playerMovementBase.Move(PlayerComponents, _input, _controller, _mainCamera, _playerAnimator);
        }

        private void LateUpdate()
        {
            _playerCamera.RotateCamera(_input.look);
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