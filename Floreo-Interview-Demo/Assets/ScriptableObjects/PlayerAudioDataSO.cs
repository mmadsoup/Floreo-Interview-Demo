using UnityEngine;

[CreateAssetMenu(fileName = "PlayerAudioDataSO", menuName = "Scriptable Objects/PlayerAudioDataSO")]
public class PlayerAudioDataSO : ScriptableObject
{
         public AudioClip LandingAudioClip;
        public AudioClip[] FootstepAudioClips;
        [Range(0, 1)] public float FootstepAudioVolume;
}
