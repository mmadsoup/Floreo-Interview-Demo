using UnityEngine;
using StarterAssets.Player.Movement;

namespace StarterAssets.Player.Audio
{
    public class MultiplayerAudio : MonoBehaviour
    {
        [SerializeField] PlayerAudioDataSO _playerAudio;
        private MultiplayerMovement _playerMovement;

        void Awake()
        {
            _playerMovement = GetComponent<MultiplayerMovement>();;
        }
        void OnEnable()
        {
            if (_playerMovement == null) return;

            _playerMovement.OnFootStepped += PlayFootstepAudio;
            _playerMovement.OnPlayerLanded += PlayLandingAudio;
        }

        void OnDisable()
        {
            if (_playerMovement == null) return;
            
            _playerMovement.OnFootStepped -= PlayFootstepAudio;
            _playerMovement.OnPlayerLanded -= PlayLandingAudio;
        }

        private void PlayFootstepAudio(CharacterController _controller)
        {
            if (_playerAudio.FootstepAudioClips.Length > 0)
            {
                var index = Random.Range(0, _playerAudio.FootstepAudioClips.Length);
                AudioSource.PlayClipAtPoint(_playerAudio.FootstepAudioClips[index], transform.TransformPoint(_controller.center), _playerAudio.FootstepAudioVolume);
            }
        }

        private void PlayLandingAudio(CharacterController _controller)
        {
            AudioSource.PlayClipAtPoint(_playerAudio.LandingAudioClip, transform.TransformPoint(_controller.center), _playerAudio.FootstepAudioVolume);
        }
    }

}
