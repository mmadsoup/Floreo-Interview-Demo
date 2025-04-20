using UnityEngine;

//Open-closure principle: This interface allows us to add new interactable objects without modifying existing code.

//Any game object that we want to be interactable should implement this interface.
//The player can then call the Interact and Uninteract methods on any object that implements this interface.
//This allows us to define a contract for interactable objects, ensuring they have the necessary methods for interaction.
public interface IInteractable
{
    public void Interact();
    public void Uninteract();
}
