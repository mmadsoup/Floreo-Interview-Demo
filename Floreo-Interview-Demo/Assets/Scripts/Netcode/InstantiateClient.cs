using UnityEngine;
using Unity.Netcode;

namespace StarterAssets.Networking
{
    public class InstantiateClient : MonoBehaviour
    {
        
        void Awake()
        {
            NetworkManager.Singleton.StartClient();
        }
    }
}
