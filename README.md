# Interview Tech Demo
Small educational demo where I experimented with unfamiliar technologies such as addressables, networking and loading scenes into a hierarchy asynchronously.

## How to Use
Use WASD and mouse to move the character, go near spheres to learn about SOLID prinicpals, move away from them to exit. Available in both single and multiplayer.

## Extending from
- [Unity Third Person Controller](https://assetstore.unity.com/packages/essentials/starter-assets-thirdperson-updates-in-new-charactercontroller-pa-196526?srsltid=AfmBOoqvIsOoFsGqnJY6_LrtKc1UEwbI17N8BXxxux6a0p3WZgX4wC5X)
- [Unity Design Patterns](https://assetstore.unity.com/packages/essentials/tutorial-projects/level-up-your-code-with-design-patterns-and-solid-289616?srsltid=AfmBOoo1yzzEJk81iBWJnT2g8zU0QgPfhlVvohvhjxRWq8Mas6FF4ETF)

## Quick Jump - Scripts
### My Scripts:
- [Main Menu Controller](https://github.com/mmadsoup/Floreo-Interview-Demo/blob/main/Floreo-Interview-Demo/Assets/Scripts/UI/MainMenuCotroller.cs)
- [Addressable Instantiator](https://github.com/mmadsoup/Floreo-Interview-Demo/blob/main/Floreo-Interview-Demo/Assets/Scripts/Addressables/AddressableInstantiator.cs)
- [Interact Script](https://github.com/mmadsoup/Floreo-Interview-Demo/blob/main/Floreo-Interview-Demo/Assets/Scripts/Interactables/Interactable.cs)
- [Interactable Instantiator](https://github.com/mmadsoup/Floreo-Interview-Demo/blob/main/Floreo-Interview-Demo/Assets/Scripts/Interactables/InteractableInstantiator.cs)
- [Player Controller Instantiator](https://github.com/mmadsoup/Floreo-Interview-Demo/blob/main/Floreo-Interview-Demo/Assets/Scripts/Addressables/PlayerControllerInstantiator.cs)


### Player:
#### Separated into single responsibility scripts, [OG Third Person Controller](https://github.com/mmadsoup/Floreo-Interview-Demo/blob/main/Floreo-Interview-Demo/Assets/StarterAssets/ThirdPersonController/Scripts/ThirdPersonController.cs)
- [Movement](https://github.com/mmadsoup/Floreo-Interview-Demo/blob/main/Floreo-Interview-Demo/Assets/Scripts/Player/Movement/PlayerMovement.cs)
- [Camera](https://github.com/mmadsoup/Floreo-Interview-Demo/blob/main/Floreo-Interview-Demo/Assets/Scripts/Player/Camera/PlayerCamera.cs)
- [Audio](https://github.com/mmadsoup/Floreo-Interview-Demo/blob/main/Floreo-Interview-Demo/Assets/Scripts/Player/Audio/PlayerAudio.cs)
- [Animation](https://github.com/mmadsoup/Floreo-Interview-Demo/blob/main/Floreo-Interview-Demo/Assets/Scripts/Player/Animation/PlayerAnimation.cs)
- [Animation Base Class](https://github.com/mmadsoup/Floreo-Interview-Demo/blob/main/Floreo-Interview-Demo/Assets/Scripts/Player/BaseClasses/PlayerAnimatorBaseClass.cs)
- [Movement Base Class](https://github.com/mmadsoup/Floreo-Interview-Demo/blob/main/Floreo-Interview-Demo/Assets/Scripts/Player/BaseClasses/PlayerMovementBaseClass.cs)

## Technologies used
- Loading scenes asynchronously using addressables
- Netcode
- UI Toolkit

## Additional features
- Refactored monolith 3rd person controller class
- Observer pattern
- Singleton pattern
- Flyweight pattern
- Interface implementation
- Interactables


![image](https://github.com/user-attachments/assets/c405d7e2-5918-45c5-8217-006e6c539016)
