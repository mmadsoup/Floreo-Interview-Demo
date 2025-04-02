using StarterAssets.AdrressableObjects;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
namespace StarterAssets.Interactable
{
    public class Interactable : MonoBehaviour
    {
        [SerializeField] private string _addressablePath;
        [SerializeField] private string _singlePlayerScene;
        [SerializeField] private string _multiplayerScene;
        private AddressableInstantiator _addressableInstantiator;
        private GameObject _mainCamera;
        private GameObject _uiEvents;
        private GameObject _environment;
        private AsyncOperationHandle<SceneInstance> _handle;
        private bool _spawned = false;

        void Awake()
        {
            var add = GameObject.FindGameObjectWithTag("AddressableInstantiator");
            _addressableInstantiator = add.GetComponent<AddressableInstantiator>();

            _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
            _uiEvents = GameObject.FindGameObjectWithTag("UIEvents");
            _environment = GameObject.FindGameObjectWithTag("Environment");        
        }

        private AsyncOperationHandle<SceneInstance> GetHandle(string address)
        {
            return Addressables.LoadSceneAsync(address);
        }
        void OnTriggerEnter(Collider other)
        {
            if (!_spawned) {
            _handle = GetHandle(name);
            Addressables.LoadSceneAsync(name);
            _addressableInstantiator.LoadSceneAdditive(_addressablePath);

            Scene scene = SceneManager.GetSceneByName(_singlePlayerScene).IsValid() ? SceneManager.GetSceneByName(_singlePlayerScene) : SceneManager.GetSceneByName(_multiplayerScene);
            
            if (scene.IsValid())
            {
                foreach (GameObject obj in scene.GetRootGameObjects())
                {

                        obj.SetActive(false);
                    
                }
            }

            _uiEvents.SetActive(false);
            _mainCamera.SetActive(false);
            }
            _spawned = true;
            //_environment.SetActive(false);
        }
        void OnTriggerExit(Collider other)
        {
            _uiEvents.SetActive(true);
            _mainCamera.SetActive(true);
            //_environment.SetActive(true);
            _addressableInstantiator.UnloadSceneAdditive(_handle);
        }
    }

}
