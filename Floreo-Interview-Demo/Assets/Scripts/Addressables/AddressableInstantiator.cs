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
        private Interactable _interactable;
        private Dictionary<string, string> _addressableDictionary = new();
        
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
            _interactable.OnUninteracted += UnloadSceneAdditive;
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

