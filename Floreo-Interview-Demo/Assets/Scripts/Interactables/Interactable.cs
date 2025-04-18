using System;
using UnityEngine;

namespace StarterAssets.Interactive
{
    public class Interactable : MonoBehaviour, IInteractable
    {
        [SerializeField] private string _addressablePath; 
        public event Action<string> OnInteracted;
        public event Action<string> OnUninteracted;
        private bool _spawned = false;


        void OnTriggerEnter(Collider other)
        {
            if (_spawned) return;
            Interact();
        }

        void OnTriggerExit(Collider other)
        {
            Uninteract();;
        }

        public void Uninteract()
        {
            OnUninteracted?.Invoke(_addressablePath);
            ToggleInteractable();
        }

        public void Interact()
        {
             OnInteracted?.Invoke(_addressablePath);
             ToggleInteractable();
        }
        
        private void ToggleInteractable()
        {
            _spawned = !_spawned;
            Cursor.visible = !Cursor.visible;
        }
    }
}
