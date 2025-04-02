using StarterAssets.AdrressableObjects;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
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

        _handle = GetHandle(name);
        Addressables.LoadSceneAsync(name);
        _addressableInstantiator.LoadSceneAdditive(_addressablePath);
        _uiEvents.SetActive(false);
        _mainCamera.SetActive(false);
        _environment.SetActive(false);
    }
    void OnTriggerExit(Collider other)
    {
        _uiEvents.SetActive(true);
        _mainCamera.SetActive(true);
        _environment.SetActive(true);
        _addressableInstantiator.UnloadSceneAdditive(_handle);
    }
}
