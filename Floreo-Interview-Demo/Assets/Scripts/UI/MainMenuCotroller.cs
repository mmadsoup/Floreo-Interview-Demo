using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuCotroller : MonoBehaviour
{
    private VisualElement _ui;
    private Button _buttonOne;
    private Button _buttonTwo;
    private TextElement _titleText;
    private TextElement _subtitleText;
    private enum MenuState { DefaultMenu, Multiplayer}
    private MenuState state = MenuState.DefaultMenu;
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
                Debug.Log("Single Player");
            break;
            case MenuState.Multiplayer:
                Debug.Log("Create Host controller");
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
                Debug.Log("Create client controller controller");
            break;
        }
    }
}
