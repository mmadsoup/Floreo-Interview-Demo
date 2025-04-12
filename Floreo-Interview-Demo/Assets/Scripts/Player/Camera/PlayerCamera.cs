using UnityEngine;
using StarterAssets.Utilities;
using StarterAssets.Player.Movement;

namespace StarterAssets.Player.Camera
{
    public class PlayerCamera : MonoBehaviour
    {
            public GameObject CinemachineCameraTarget;

            [Tooltip("How far in degrees can you move the camera up")]
            private float _topClamp = 70.0f;

            [Tooltip("How far in degrees can you move the camera down")]
            private float _bottomClamp = -30.0f;

            [Tooltip("Additional degress to override the camera. Useful for fine tuning camera position when locked")]
            private float _cameraAngleOverride = 0.0f;

            [Tooltip("For locking the camera position on all axis")]
            private bool _lockCameraPosition = false;
            // cinemachine
            private float _cinemachineTargetYaw;
            private float _cinemachineTargetPitch;
            private const float _threshold = 0.01f;

            public void InitializeCamera()
            {
                _cinemachineTargetYaw = CinemachineCameraTarget.transform.rotation.eulerAngles.y;
            }

            public void RotateCamera(Vector2 input)
            {
                if (input.sqrMagnitude >= _threshold && !_lockCameraPosition)
                {
                    float deltaTimeMultiplier = 1.0f;

                    _cinemachineTargetYaw += input.x * deltaTimeMultiplier;
                    _cinemachineTargetPitch += input.y * deltaTimeMultiplier;
                    
                }

                _cinemachineTargetYaw = Utils.ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
                _cinemachineTargetPitch = Utils.ClampAngle(_cinemachineTargetPitch, _bottomClamp, _topClamp);

                CinemachineCameraTarget.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch + _cameraAngleOverride,
                    _cinemachineTargetYaw, 0.0f);
            }
    }
}
