using StarterAssets;
using UnityEngine;

namespace StarterAssets.Player.Audio
{
    public class PlayerAudio : MonoBehaviour
    {
        public AudioClip LandingAudioClip;
        public AudioClip[] FootstepAudioClips;
        [Range(0, 1)] public float FootstepAudioVolume = 0.5f;
        private PlayerController playerController;

        void Awake()
        {
            playerController = FindFirstObjectByType<PlayerController>();;
        }
        void OnEnable()
        {
            if (playerController == null) return;

            playerController.OnFootStepped += PlayFootstepAudio;
            playerController.OnPlayerLanded += PlayLandingAudio;
        }

        void OnDisable()
        {
            if (playerController == null) return;
        }

        private void PlayFootstepAudio(CharacterController _controller)
        {
            if (FootstepAudioClips.Length > 0)
            {
                var index = Random.Range(0, FootstepAudioClips.Length);
                AudioSource.PlayClipAtPoint(FootstepAudioClips[index], transform.TransformPoint(_controller.center), FootstepAudioVolume);
            }
        }

        private void PlayLandingAudio(CharacterController _controller)
        {
            AudioSource.PlayClipAtPoint(LandingAudioClip, transform.TransformPoint(_controller.center), FootstepAudioVolume);
        }
    }

}
