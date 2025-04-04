using System.Collections.Generic;
using StarterAssets.Menu;
using UnityEngine;
using UnityEngine.SceneManagement;
using StarterAssets.Interactive;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using DesignPatterns.Command;
namespace StarterAssets.AdrressableObjects
{
        public class AddressableInstantiator : MonoBehaviour
    {
        [SerializeField] private MainMenuCotroller _mainMenuCotroller;
        [SerializeField] private string _singlePlayerPath;
        [SerializeField] private string _hostPath;
        [SerializeField] private string _clientPath;
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
            _mainMenuCotroller.OnSinglePlayerButtonClicked += CreateSinglePlayerController;
            _mainMenuCotroller.OnHostButtonClicked += CreateHostController;
            _mainMenuCotroller.OnJoinButtonClicked += CreateClientController;
            
            foreach (Interactable obj in _interactables)
            {
                if (obj != null)
                {
                    obj.OnInteracted += LoadSceneAdditive;
                    obj.OnUninteracted += UnloadSceneAdditive;
                }
            }
        }   

        void OnDisable()
        {
            _mainMenuCotroller.OnSinglePlayerButtonClicked -= CreateSinglePlayerController;
            _mainMenuCotroller.OnHostButtonClicked -= CreateHostController;
            _mainMenuCotroller.OnJoinButtonClicked -= CreateClientController;
        }

        private void CreateSinglePlayerController()
        {
            LoadSceneAdditive(_singlePlayerPath);
        }

         private void CreateHostController()
        {
            LoadSceneAdditive(_hostPath);
        }

        private void CreateClientController()
        {
           LoadSceneAdditive(_clientPath);
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

