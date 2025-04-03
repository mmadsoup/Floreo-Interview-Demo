using System.Collections.Generic;
using StarterAssets.Menu;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using StarterAssets.Interactive;
using System;

namespace StarterAssets.AdrressableObjects
{
        public class AddressableInstantiator : MonoBehaviour
    {
        [SerializeField] private AddressableDatabaseSO _addressables;

        [SerializeField] private MainMenuCotroller _mainMenuCotroller;
        private Interactable _interactable;
        void Awake()
        {
            GameObject interactableObject = GameObject.FindGameObjectWithTag("Interactable");
            _interactable = interactableObject.GetComponent<Interactable>();
        }

        void OnEnable()
        {
            _mainMenuCotroller.OnSinglePlayerButtonClicked += CreateSinglePlayerController;
            _mainMenuCotroller.OnHostButtonClicked += CreateHostController;
            _mainMenuCotroller.OnJoinButtonClicked += CreateClientController;
            _interactable.OnInteracted += LoadSceneAdditive;

        }   

        void OnDisable()
        {
            _mainMenuCotroller.OnSinglePlayerButtonClicked -= CreateSinglePlayerController;
            _mainMenuCotroller.OnHostButtonClicked -= CreateHostController;
            _mainMenuCotroller.OnJoinButtonClicked -= CreateClientController;
        }


        private string GetAddressableByName(string name)
        {
            for (int i = 0; i < _addressables.addressables.Length; i++)
            {
                var child = _addressables.addressables[i].addressableName;
                if (name == child)
                {
                    return child = _addressables.addressables[i].addressablePath;
                }
            }
            return null;
        }

        private void CreateSinglePlayerController()
        {
            var singlePlayer = GetAddressableByName("single_player");
            LoadSceneAdditive(singlePlayer);
        }

         private void CreateHostController()
        {
            var hostPlayer = GetAddressableByName("multiplayer_host");
            LoadSceneAdditive(hostPlayer);
        }

        private void CreateClientController()
        {
            var clientPlayer = GetAddressableByName("multiplayer_client");
            LoadSceneAdditive(clientPlayer);
        }

        public void LoadSceneAdditive(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                Addressables.LoadSceneAsync(name, LoadSceneMode.Additive);
            }
            else
            {
                Debug.Log("Please add a scene name");
            }
        }

        public void UnloadSceneAdditive(AsyncOperationHandle<SceneInstance> handle)
        {
            //private AsyncOperationHandle<SceneInstance> _sceneHandle;
            Addressables.UnloadSceneAsync(handle, UnloadSceneOptions.None);
        }

    }
}

