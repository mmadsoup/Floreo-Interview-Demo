using UnityEngine;
using Unity.Netcode;
namespace StarterAssets.Networking
{
    public class InstantiateHost : MonoBehaviour
    {
        
        void Awake()
        {
            NetworkManager.Singleton.StartHost();
        }
    }
}

