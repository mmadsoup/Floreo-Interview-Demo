using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using StarterAssets.Interactive;
namespace StarterAssets.AdrressableObjects
{
    public class AddressableInstantiator : MonoBehaviour
    {
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
                    obj.OnInteracted += LoadSceneAdditive;
                    obj.OnUninteracted += UnloadSceneAdditive;
                }
            }
        }   

        public void LoadSceneAdditive(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);
            }
            else
            {
                Debug.Log("Please add a scene name");
            }
        }

        public void UnloadSceneAdditive(string name)
        {
            SceneManager.UnloadSceneAsync(name);
        }
    }
}

