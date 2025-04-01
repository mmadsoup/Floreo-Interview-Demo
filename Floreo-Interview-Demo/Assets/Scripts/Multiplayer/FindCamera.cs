using Cinemachine;
using UnityEngine;

namespace StarterAssets.Player.Movement
{
        public class FindCamera : MonoBehaviour
    {
        GameObject followCamera;

        void OnEnable()
        {
            followCamera = GameObject.FindGameObjectWithTag("PlayerFollowCamera");
            followCamera.GetComponent<CinemachineVirtualCamera>().Follow = transform.GetChild(0);
        }
    }

}
