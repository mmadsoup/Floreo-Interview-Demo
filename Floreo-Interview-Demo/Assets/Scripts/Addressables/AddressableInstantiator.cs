using UnityEngine;
using UnityEngine.SceneManagement;
namespace StarterAssets.AdrressableObjects
{
    public class AddressableInstantiator : MonoBehaviour
    {
        public static AddressableInstantiator Instance { get; private set; }

        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
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

