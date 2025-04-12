using UnityEngine;
using StarterAssets.Menu;

namespace StarterAssets.AdrressableObjects {
    public class PlayerControllerInstantiator : MonoBehaviour
    {
        [SerializeField] private MainMenuCotroller _mainMenuCotroller;
        [SerializeField] private string _singlePlayerPath;
        [SerializeField] private string _hostPath;
        [SerializeField] private string _clientPath;
        

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
            AddressableInstantiator.Instance.LoadSceneAdditive(_singlePlayerPath);
        }

         private void CreateHostController()
        {
            AddressableInstantiator.Instance.LoadSceneAdditive(_hostPath);
        }

        private void CreateClientController()
        {
           AddressableInstantiator.Instance.LoadSceneAdditive(_clientPath);
        }
    }
}


