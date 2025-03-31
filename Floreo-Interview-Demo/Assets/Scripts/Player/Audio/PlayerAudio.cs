using UnityEngine;

namespace StarterAssets.Player.Audio
{
        public class PlayerAudio : MonoBehaviour
    {
        [SerializeField] private AudioClip LandingAudioClip;
        [SerializeField] private AudioClip[] FootstepAudioClips;
        [SerializeField] [Range(0, 1)] private float FootstepAudioVolume = 0.5f;
        [SerializeField] private PlayerController playerController;
        private void OnEnable()
        {
            if (playerController == null) 
            {
                return;
            }
            playerController.OnFootStepped += PlayFootStepAudio;
            playerController.OnLanded += PlayLandedAudio;
        }

        private void OnDsable()
        {
            if (playerController == null) 
            {
                return;
            }
            playerController.OnFootStepped -= PlayFootStepAudio;
            playerController.OnLanded -= PlayLandedAudio;
        }

        private void PlayFootStepAudio(CharacterController characterController)
        {
            if (FootstepAudioClips.Length > 0)
                {
                    var index = Random.Range(0, FootstepAudioClips.Length);
                    AudioSource.PlayClipAtPoint(FootstepAudioClips[index], transform.TransformPoint(characterController.center), FootstepAudioVolume);
                }
        }

        private void PlayLandedAudio(CharacterController characterController)
        {
            AudioSource.PlayClipAtPoint(LandingAudioClip, transform.TransformPoint(characterController.center), FootstepAudioVolume);
        }
    }
}

