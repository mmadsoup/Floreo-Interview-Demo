using System;
using NUnit.Framework.Constraints;
using StarterAssets.AdrressableObjects;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace StarterAssets.Interactive
{
    public class Interactable : MonoBehaviour, IInteractable
    {
        [SerializeField] private string _addressablePath;  
        public event Action<string> OnInteracted;
        public event Action<string> OnUninteracted;
        private bool _spawned = false;   

        void Awake()
        {
           
               
        }

        void OnTriggerEnter(Collider other)
        {
            if (_spawned) return;
            Interact();
        }

        public void Interact()
        {
             OnInteracted?.Invoke(_addressablePath);
             _spawned = true;
        }
    }

}
