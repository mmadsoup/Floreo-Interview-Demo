using System;
using StarterAssets.AdrressableObjects;
using UnityEngine;
using UnityEngine.Events;

namespace StarterAssets.Interactive
{
    // Single Responsibility Principle: This class is only responsible for handling the interactions to load or unload the scene.
    // Open-Closed Principle: This class is open for extension (we can add new interactable objects) but closed for modification (we don't need to modify this class to add new interactable objects).
    // Liskov Substitution Principle: This class implements the IInteractable interface, allowing it to be used interchangeably with other interactable objects
    // Interface Segregation Principle: The IInteractable interface is small and specific, allowing this class to implement only the methods it needs. (Interact and Uninteract)
    // Dependency Inversion Principle: This class depends on the IInteractable interface, allowing it to work with any object that implements this interface.
    public class LevelTeleporter : MonoBehaviour, IInteractable
    {
        [SerializeField] private string _addressablePath; 
        private bool _spawned = false;

        public void Uninteract()
        {
            AddressableInstantiator.Instance.UnloadSceneAdditive(_addressablePath);
            ToggleInteractable();
        }

        // Concretely define the Interact method from the IInteractable interface.
        public void Interact()
        {
             AddressableInstantiator.Instance.LoadSceneAdditive(_addressablePath);
             ToggleInteractable();
        }
        
        private void ToggleInteractable()
        {
            _spawned = !_spawned;
            Cursor.visible = !Cursor.visible;
        }
    }
}
