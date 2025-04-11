using UnityEngine;
using StarterAssets.Menu;
using System.Collections.Generic;
using System;

namespace StarterAssets.AdrressableObjects {
    public class PlayerControllerInstantiator : MonoBehaviour
    {
        [SerializeField] private MainMenuCotroller _mainMenuCotroller;
        [SerializeField] AddressableInstantiator _addressableInstantiator;
        [SerializeField] private string _singlePlayerPath;
        [SerializeField] private string _hostPath;
        [SerializeField] private string _clientPath;
        
        void Start()
        {
            _addressableInstantiator.GetComponent<AddressableInstantiator>();
        }
        void OnEnable()
        {  
            _mainMenuCotroller.OnSinglePlayerButtonClicked += CreateSinglePlayerController;
            _mainMenuCotroller.OnHostButtonClicked += CreateHostController;
            _mainMenuCotroller.OnJoinButtonClicked += CreateClientController;
        }
        void OnDisable()
        {
            _mainMenuCotroller.OnSinglePlayerButtonClicked -= CreateSinglePlayerController;
            _mainMenuCotroller.OnHostButtonClicked -= CreateHostController;
            _mainMenuCotroller.OnJoinButtonClicked -= CreateClientController;
        }

        private void CreateSinglePlayerController()
        {
            _addressableInstantiator.LoadSceneAdditive(_singlePlayerPath);
        }

         private void CreateHostController()
        {
            _addressableInstantiator.LoadSceneAdditive(_hostPath);
        }

        private void CreateClientController()
        {
           _addressableInstantiator.LoadSceneAdditive(_clientPath);
        }
    }
}


