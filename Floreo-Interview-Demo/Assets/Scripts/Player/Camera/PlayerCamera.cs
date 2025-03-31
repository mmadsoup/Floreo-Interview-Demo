using UnityEngine;
using StarterAssets.Utilities;

namespace StarterAssets.Player.Camera
{
    public class PlayerCamera : MonoBehaviour
    {
        
            public GameObject CinemachineCameraTarget;

            [Tooltip("How far in degrees can you move the camera up")]
            public float TopClamp = 70.0f;

            [Tooltip("How far in degrees can you move the camera down")]
            public float BottomClamp = -30.0f;

            [Tooltip("Additional degress to override the camera. Useful for fine tuning camera position when locked")]
            public float CameraAngleOverride = 0.0f;

            [Tooltip("For locking the camera position on all axis")]
            public bool LockCameraPosition = false;
            // cinemachine
            private float _cinemachineTargetYaw;
            private float _cinemachineTargetPitch;
            private const float _threshold = 0.01f;
            private PlayerMovement playerController;

            public void InitializeCamera()
            {
                _cinemachineTargetYaw = CinemachineCameraTarget.transform.rotation.eulerAngles.y;
            }

            public void RotateCamera(Vector2 input, bool isCurrentDeviceMouse)
            {
                // if there is an input and camera position is not fixed
                if (input.sqrMagnitude >= _threshold && !LockCameraPosition)
                {
                    if (isCurrentDeviceMouse) 
                    {
                        //Don't multiply mouse input by Time.deltaTime;
                        float deltaTimeMultiplier = isCurrentDeviceMouse ? 1.0f : Time.deltaTime;

                        _cinemachineTargetYaw += input.x * deltaTimeMultiplier;
                        _cinemachineTargetPitch += input.y * deltaTimeMultiplier;
                    }
                }

                // clamp our rotations so our values are limited 360 degrees
                _cinemachineTargetYaw = Utils.ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
                _cinemachineTargetPitch = Utils.ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

                // Cinemachine will follow this target
                CinemachineCameraTarget.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch + CameraAngleOverride,
                    _cinemachineTargetYaw, 0.0f);
            }
    }
}
