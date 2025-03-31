using UnityEngine;

[CreateAssetMenu(fileName = "PlayerComponents", menuName = "Scriptable Objects/PlayerComponents")]
public class PlayerComponentsSO : ScriptableObject
{
    [Header("Player")]
        [Tooltip("Move speed of the character in m/s")]
        [SerializeField]public float MoveSpeed;

        [Tooltip("Sprint speed of the character in m/s")]
        [SerializeField] public float SprintSpeed;

        [Tooltip("How fast the character turns to face movement direction")]
        [Range(0.0f, 0.3f)]
        [SerializeField] public float RotationSmoothTime;

        [Tooltip("Acceleration and deceleration")]
        [SerializeField] public float SpeedChangeRate;

        [Space(10)]
        [Tooltip("The height the player can jump")]
        [SerializeField] public float JumpHeight;

        [Tooltip("The character uses its own gravity value. The engine default is -9.81f")]
        [SerializeField] public float Gravity;

        [Space(10)]
        [Tooltip("Time required to pass before being able to jump again. Set to 0f to instantly jump again")]
        [SerializeField] public float JumpTimeout;

        [Tooltip("Time required to pass before entering the fall state. Useful for walking down stairs")]
        [SerializeField] public float FallTimeout;

        [Header("Player Grounded")]
        [Tooltip("If the character is grounded or not. Not part of the CharacterController built in grounded check")]
        [SerializeField] public bool Grounded;

        [Tooltip("Useful for rough ground")]
        [SerializeField] public float GroundedOffset;

        [Tooltip("The radius of the grounded check. Should match the radius of the CharacterController")]
        [SerializeField] public float GroundedRadius;

        [Tooltip("What layers the character uses as ground")]
        [SerializeField] public LayerMask GroundLayers;

}
