using UnityEngine;
using StarterAssets.Player.Movement;

namespace StarterAssets.Player.Audio
{
    public class PlayerAudio : MonoBehaviour
    {
        public AudioClip LandingAudioClip;
        public AudioClip[] FootstepAudioClips;
        [Range(0, 1)] public float FootstepAudioVolume = 0.5f;
        private PlayerMovement playerMovement;

        void Awake()
        {
            playerMovement = FindFirstObjectByType<PlayerMovement>();;
        }
        void OnEnable()
        {
            if (playerMovement == null) return;

            playerMovement.OnFootStepped += PlayFootstepAudio;
            playerMovement.OnPlayerLanded += PlayLandingAudio;
        }

        void OnDisable()
        {
            if (playerMovement == null) return;
            
            playerMovement.OnFootStepped -= PlayFootstepAudio;
            playerMovement.OnPlayerLanded -= PlayLandingAudio;
        }

        private void PlayFootstepAudio(CharacterController _controller)
        {
            if (FootstepAudioClips.Length > 0)
            {
                var index = Random.Range(0, FootstepAudioClips.Length);
                //AudioSource.PlayClipAtPoint(FootstepAudioClips[index], transform.TransformPoint(_controller.center), FootstepAudioVolume);
            }
        }

        private void PlayLandingAudio(CharacterController _controller)
        {
            //AudioSource.PlayClipAtPoint(LandingAudioClip, transform.TransformPoint(_controller.center), FootstepAudioVolume);
        }
    }

}
