using UnityEngine;

[CreateAssetMenu(fileName = "PlayerComponents", menuName = "Scriptable Objects/PlayerComponents")]
public class PlayerComponentsSO : ScriptableObject
{
    [Header("Player")]
        public float MoveSpeed;

        [Tooltip("Sprint speed of the character in m/s")]
        public float SprintSpeed;

        [Tooltip("How fast the character turns to face movement direction")]
        [Range(0.0f, 0.3f)]
        public float RotationSmoothTime;

        [Tooltip("Acceleration and deceleration")]
        public float SpeedChangeRate;

        [Space(10)]
        [Tooltip("The height the player can jump")]
        public float JumpHeight;

        [Tooltip("The character uses its own gravity value. The engine default is -9.81f")]
        public float Gravity;

        [Space(10)]
        [Tooltip("Time required to pass before being able to jump again. Set to 0f to instantly jump again")]
        public float JumpTimeout;

        [Tooltip("Time required to pass before entering the fall state. Useful for walking down stairs")]
        public float FallTimeout;

        [Header("Player Grounded")]
        [Tooltip("If the character is grounded or not. Not part of the CharacterController built in grounded check")]
        public bool Grounded;

        [Tooltip("Useful for rough ground")]
        public float GroundedOffset;

        [Tooltip("The radius of the grounded check. Should match the radius of the CharacterController")]
        public float GroundedRadius;

        [Tooltip("What layers the character uses as ground")]
        public LayerMask GroundLayers;

}
