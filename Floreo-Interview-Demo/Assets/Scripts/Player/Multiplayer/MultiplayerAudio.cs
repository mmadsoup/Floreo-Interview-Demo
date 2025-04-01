using UnityEngine;
using StarterAssets.Player.Movement;

namespace StarterAssets.Player.Audio
{
    public class MultiplayerAudio : MonoBehaviour
    {
        [SerializeField] PlayerAudioDataSO playerAudio;
        private MultiplayerMovement playerMovement;

        void Awake()
        {
            playerMovement = FindFirstObjectByType<MultiplayerMovement>();;
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
            if (playerAudio.FootstepAudioClips.Length > 0)
            {
                var index = Random.Range(0, playerAudio.FootstepAudioClips.Length);
                AudioSource.PlayClipAtPoint(playerAudio.FootstepAudioClips[index], transform.TransformPoint(_controller.center), playerAudio.FootstepAudioVolume);
            }
        }

        private void PlayLandingAudio(CharacterController _controller)
        {
            AudioSource.PlayClipAtPoint(playerAudio.LandingAudioClip, transform.TransformPoint(_controller.center), playerAudio.FootstepAudioVolume);
        }
    }

}
