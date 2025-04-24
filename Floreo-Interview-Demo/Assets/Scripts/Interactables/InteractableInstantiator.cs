using UnityEngine;
using System.Collections.Generic;
using StarterAssets.AdrressableObjects;
using System.Linq;

namespace StarterAssets.Interactive
{
    public class InteractableInstantiator : MonoBehaviour
    {
        
        //Commented these out to show how we can loosely couple even more from the original Interactable.cs
        
        /*private GameObject[] _interactableGameObjects;
        private List<LevelTeleporter> _interactablesList = new();
        void Awake()
        {
            _interactableGameObjects = GameObject.FindGameObjectsWithTag("Interactable");
            _interactablesList = _interactableGameObjects.Select(obj => obj.GetComponent<LevelTeleporter>()).Where(interactable => interactable != null).ToList();
        }

        void OnEnable()
        {
            foreach (LevelTeleporter obj in _interactablesList)
            {
                if (obj == null) continue;
                obj.OnInteracted += AddressableInstantiator.Instance.LoadSceneAdditive;
                obj.OnUninteracted += AddressableInstantiator.Instance.UnloadSceneAdditive;
                
            }
        }

        void OnDisable()
        {
            foreach (LevelTeleporter obj in _interactablesList)
            {
                if (obj == null) continue;
                obj.OnInteracted -= AddressableInstantiator.Instance.LoadSceneAdditive;
                obj.OnUninteracted -= AddressableInstantiator.Instance.UnloadSceneAdditive;
                
            }
        }*/
    }
}
