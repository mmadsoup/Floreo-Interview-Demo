using System;
using UnityEngine;
using UnityEngine.UIElements;
using Unity.Netcode;

namespace StarterAssets.Menu
{
    public class MainMenuCotroller : MonoBehaviour
    {
        private VisualElement _ui;
        private Button _buttonOne;
        private Button _buttonTwo;
        private TextElement _titleText;
        private TextElement _subtitleText;
        private enum MenuState { DefaultMenu, Multiplayer}
        private MenuState state = MenuState.DefaultMenu;

        public event Action OnSinglePlayerButtonClicked;
        public event Action OnHostButtonClicked;
        public event Action OnJoinButtonClicked;
        
        void Awake()
        {
            _ui = GetComponent<UIDocument>().rootVisualElement;
            _titleText = _ui.Q<TextElement>("TitleText");
            _subtitleText = _ui.Q<TextElement>("SubtitleText");
        }

        void OnEnable()
        {
            _buttonOne = _ui.Q<Button>("ButtonOne");
            _buttonOne.text = "Single Player";
            _buttonOne.clicked += OnSinglePlayerClicked;

            _buttonTwo = _ui.Q<Button>("ButtonTwo");
            _buttonTwo.text = "Multiplayer";
            _buttonTwo.clicked += OnMultiplayerClicked;
        }

        void OnDisable()
        {
            _buttonOne.clicked -= OnSinglePlayerClicked;
            _buttonTwo.clicked -= OnMultiplayerClicked;
        }

        private void OnSinglePlayerClicked()
        {
            
            ButtonOneStateHandler();
        }
        
        private void OnMultiplayerClicked()
        {
            ButtonTwoStateHandler();
        }

        private void ButtonOneStateHandler()
        {
            switch (state)
            { 
                case MenuState.DefaultMenu:
                    OnSinglePlayerButtonClicked?.Invoke();
                    UnloadMenu();
                break;
                case MenuState.Multiplayer:
                    OnHostButtonClicked?.Invoke();
                    UnloadMenu();
                break;
            }
        }

        private void ButtonTwoStateHandler()
        {
            switch (state)
            { 
                case MenuState.DefaultMenu:
                    Debug.Log("Multi Player");
                    state = MenuState.Multiplayer;
                    _buttonTwo.text = "Join";
                    _buttonOne.text = "Host";
                break;
                case MenuState.Multiplayer:
                    NetworkManager.Singleton.StartClient();
                    OnJoinButtonClicked?.Invoke();
                    UnloadMenu();
                break;
            }
        }

        private void UnloadMenu()
        {
            Destroy(gameObject);
            Resources.UnloadUnusedAssets();
        }
    }

}
