using StarterAssets.Menu;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace StarterAssets.AdrressableObjects
{
        public class AddressableInstantiator : MonoBehaviour
    {
        [SerializeField] private string _singlePlayerSceneName;
        [SerializeField] private string _multiplayerHostSceneName;
        [SerializeField] private string _multiplayerClientSceneName;
        private MainMenuCotroller _mainMenuCotroller;
        
        void Awake()
        {
            _mainMenuCotroller = FindFirstObjectByType<MainMenuCotroller>();
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
            LoadSceneAdditive(_singlePlayerSceneName);
            Debug.Log("Create Single Player Controller");
        }

         private void CreateHostController()
        {
            LoadSceneAdditive(_multiplayerHostSceneName);
            Debug.Log("Create Host Player Controller");
        }

        private void CreateClientController()
        {
            LoadSceneAdditive(_multiplayerClientSceneName);
            Debug.Log("Create Client Player Controller");
        }

        private void LoadSceneAdditive(string name)
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

    }
}

