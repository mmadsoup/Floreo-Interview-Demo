using System;
using StarterAssets.Player.Animation;
using StarterAssets.Player.Audio;
using UnityEngine;
using Unity.Netcode;
using Cinemachine;
using System.Diagnostics.Contracts;


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
        //[SerializeField] private PlayerControllerSO _playerControllerType;
        [SerializeField] private PlayerControllerType _playerControllerType;
        private NetworkBehaviour _networkBehavriour;

#if ENABLE_INPUT_SYSTEM 
        private PlayerInput _playerInput;
#endif

        private PlayerMovementBaseClass _playerMovementBase;
        private CharacterController _controller;
        private StarterAssetsInputs _input;
        private GameObject _mainCamera;
        private PlayerAnimation _playerAnimator;
        private PlayerCamera _playerCamera;
        private CinemachineVirtualCamera _virtualCamera;

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
            _playerAnimator.AssignAnimationIDs();
            _input = GetComponent<StarterAssetsInputs>();
#if ENABLE_INPUT_SYSTEM 
            
            _controller = GetComponent<CharacterController>();
            
#else       
			Debug.LogError( "Starter Assets package is missing dependencies. Please use Tools/Starter Assets/Reinstall Dependencies to fix it");
#endif      
            _playerMovementBase = GetComponent<PlayerMovementBaseClass>();
            _playerMovementBase.InitMovement(PlayerComponents);

           InitSinglePlayer();
           InitMultiplayer();
        }


        private void InitSinglePlayer()
        {
            if (_playerControllerType == PlayerControllerType.Multi) return;
            _playerInput = GetComponent<PlayerInput>();
        }

        private void InitMultiplayer()
        {
            if (_playerControllerType == PlayerControllerType.Single) return;
            _networkBehavriour = GetComponent<NetworkBehaviour>();
            
            if (_playerControllerType == PlayerControllerType.Multi && _networkBehavriour == null) return;

            if (_virtualCamera == null)
            {
                _virtualCamera = FindFirstObjectByType<CinemachineVirtualCamera>();
            }

            if (_networkBehavriour.IsClient && _networkBehavriour.IsOwner)
            {
                _playerInput = GetComponent<PlayerInput>();
                _playerInput.enabled = true;
                _virtualCamera.Follow = transform.GetChild(0);
            }
        }


        private void Update()
        {
            if (_playerControllerType == PlayerControllerType.Multi && !_networkBehavriour.IsOwner) return;
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