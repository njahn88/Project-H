using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public static event Action OnGameStarted;
    [SerializeField]
    private Button[] _menuButtons;

    private int _menuButtonsIndex = 0;
    private void OnEnable()
    {
        InputManager.OnMovementInput += SwitchSelectedButton;
    }

    private void Start()
    {
        _menuButtons[_menuButtonsIndex].Select();
    }

    private void OnDisable()
    {
        InputManager.OnMovementInput -= SwitchSelectedButton;
    }

    //Cycles through the avaiable buttons on the main menu, updating current index
    private void SwitchSelectedButton(Vector2 moveDirection)
    {
        if(moveDirection.y > 0)
        {
            if(_menuButtonsIndex == 0) _menuButtonsIndex = _menuButtons.Length - 1;
            else _menuButtonsIndex--;
        }
        else
        {
            if (_menuButtonsIndex == _menuButtons.Length - 1) _menuButtonsIndex = 0;
            else _menuButtonsIndex++;
        }
        _menuButtons[_menuButtonsIndex].Select();
    }

    public void StartGame()
    {
        SceneMangerTool.Instance.LoadScene("SampleScene");
        OnGameStarted?.Invoke();
    }

    public void Settings()
    {

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
