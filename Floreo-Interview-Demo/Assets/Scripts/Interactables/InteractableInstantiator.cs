using UnityEngine;
using System.Collections.Generic;
using StarterAssets.AdrressableObjects;

namespace StarterAssets.Interactive
{
    public class InteractableInstantiator : MonoBehaviour
    {
        [SerializeField]
        private AddressableInstantiator _addressableInstantiator;
        private GameObject[] _interactableGameObjects;
        private List<Interactable> _interactables = new();
        void Awake()
        {
            _interactableGameObjects = GameObject.FindGameObjectsWithTag("Interactable");

            foreach (GameObject obj in _interactableGameObjects)
            {
                Interactable interactable = obj.GetComponent<Interactable>();

                if (interactable != null)
                {
                    _interactables.Add(interactable);
                }
            }
        }

        void OnEnable()
        {
            foreach (Interactable obj in _interactables)
            {
                if (obj != null)
                {
                    obj.OnInteracted += _addressableInstantiator.LoadSceneAdditive;
                    obj.OnUninteracted += _addressableInstantiator.UnloadSceneAdditive;
                }
            }
        }

        void OnDisable()
        {
            foreach (Interactable obj in _interactables)
            {
                if (obj != null)
                {
                    obj.OnInteracted -= _addressableInstantiator.LoadSceneAdditive;
                    obj.OnUninteracted -= _addressableInstantiator.UnloadSceneAdditive;
                }
            }
        }
    }
}
