using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace StarterAssets.Menu
{
    public class MainMenuCotroller : MonoBehaviour
    {
        private VisualElement _ui;
        private Button _buttonOne;
        private Button _buttonTwo;
        private TextElement _titleText;
        private TextElement _subtitleText;
        private enum MenuState { DefaultMenu, Multiplayer }
        private Dictionary<MenuState, Action> _buttonOneEventHandlers = new();
        private Dictionary<MenuState, Action> _buttonTwoEventHandlers = new();
        private MenuState _state = MenuState.DefaultMenu;

        public event Action OnSinglePlayerButtonClicked;
        public event Action OnHostButtonClicked;
        public event Action OnJoinButtonClicked;
        
        void Awake()
        {
            _ui = GetComponent<UIDocument>().rootVisualElement;
            _titleText = _ui.Q<TextElement>("TitleText");
            _subtitleText = _ui.Q<TextElement>("SubtitleText");

            _buttonOneEventHandlers = new()
            {
                { MenuState.DefaultMenu, () => 
                {
                    OnSinglePlayerButtonClicked?.Invoke();
                    UnloadMenu();
                }},
                { MenuState.Multiplayer, () => 
                {
                    OnHostButtonClicked?.Invoke();
                    UnloadMenu();
                }}
            };

            _buttonTwoEventHandlers = new()
            {
                { MenuState.DefaultMenu, () => 
                {
                    _state = MenuState.Multiplayer;
                    _buttonTwo.text = "Join";
                    _buttonOne.text = "Host";
                }},
                { MenuState.Multiplayer, () =>
                {
                    OnJoinButtonClicked?.Invoke();
                    UnloadMenu();
                }}
            };
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
            if (_buttonOneEventHandlers.TryGetValue(_state, out var action))
            {
                action.Invoke();
            }
        }

        private void ButtonTwoStateHandler()
        {
            if (_buttonTwoEventHandlers.TryGetValue(_state, out var action))
            {
                action.Invoke();
            }
        }

        private void UnloadMenu()
        {
            Destroy(gameObject);
            Resources.UnloadUnusedAssets();
        }
    }

}
