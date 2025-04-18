using UnityEngine;
using System.Collections.Generic;
using StarterAssets.AdrressableObjects;
using System.Linq;

namespace StarterAssets.Interactive
{
    public class InteractableInstantiator : MonoBehaviour
    {
        private GameObject[] _interactableGameObjects;
        private List<Interactable> _interactablesList = new();
        void Awake()
        {
            _interactableGameObjects = GameObject.FindGameObjectsWithTag("Interactable");
            _interactablesList = _interactableGameObjects.Select(obj => obj.GetComponent<Interactable>()).Where(interactable => interactable != null).ToList();
        }

        void OnEnable()
        {
            foreach (Interactable obj in _interactablesList)
            {
                if (obj == null) continue;
                obj.OnInteracted += AddressableInstantiator.Instance.LoadSceneAdditive;
                obj.OnUninteracted += AddressableInstantiator.Instance.UnloadSceneAdditive;
                
            }
        }

        void OnDisable()
        {
            foreach (Interactable obj in _interactablesList)
            {
                if (obj == null) continue;
                obj.OnInteracted -= AddressableInstantiator.Instance.LoadSceneAdditive;
                obj.OnUninteracted -= AddressableInstantiator.Instance.UnloadSceneAdditive;
                
            }
        }
    }
}
