using System;
using UnityEngine;

//This component is attached to the player and is responsible for handling interactions with other objects.
//Whenever the player's collider collides with another object's collider, it checks if that object implements a IInteractable interface.
public class PlayerInteract : MonoBehaviour
{
    // Liskov Substitution Principle: This class can work with any object that implements the IInteractable interface, allowing for flexibility and reusability.
    // Dependency Inversion Principle: This class depends on the IInteractable interface, allowing it to work with any object that implements this interface.
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Interactable")) return;
        //Notice that I am getting the interface here. We are search for a component that implements the IInteractable interface on the object.
        //This is a good example of the Dependency Inversion Principle, where we depend on an abstraction (the interface) rather than a concrete implementation (LevelTeleporter.cs or MusicBox.cs).
        //This allows us to easily add new interactable objects without modifying this class.
        var interactable = other.gameObject.GetComponent<IInteractable>(); 
        if (interactable != null)
        {
            // Call the Interact method on the interactable object.
            interactable.Interact();
            
            //As you can see we don't need to know what the interactable object is, we just need to know it implements the IInteractable interface and call the available methods in the interface.
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Interactable")) return;
        var interactable = other.gameObject.GetComponent<IInteractable>();
        if (interactable != null)
        {
            // Call the Uninteract method on the interactable object.
            interactable.Uninteract();
        }
    }
}
