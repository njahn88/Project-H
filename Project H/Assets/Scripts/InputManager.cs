using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static event Action<Vector2> OnMovementInput;
    public static event Action<bool> OnPauseInput;
    public static InputManager Instance;
    private PlayerInput _playerInput;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        _playerInput = new PlayerInput();
        SetupStartingInput();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    private void SetupStartingInput()
    {
        _playerInput.Enable();
        _playerInput.GamePlay.Disable();
        SetupGameplayConnections();
        SetupMenuConnections();
    }

    //Subscribes functions to corresponding events in Gameplay InputAction
    private void SetupGameplayConnections()
    {
        _playerInput.GamePlay.Movement.performed += OnMovement;
        _playerInput.GamePlay.Pause.performed += OnPauseFromGamePlay;
    }

    private void SetupMenuConnections()
    {
        _playerInput.Menus.UnPause.performed += OnPauseFromMenus;
    }

    //Fires event for player movement sending a Vector2
    public void OnMovement(InputAction.CallbackContext context)
    {
        Debug.Log("Input: " + context.ReadValue<Vector2>());
        if (context.performed) OnMovementInput?.Invoke(context.ReadValue<Vector2>());
    }

    //Fires event for player pausing game, switches to Menus input
    public void OnPauseFromGamePlay(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnPauseInput?.Invoke(true);
            _playerInput.GamePlay.Disable();
            _playerInput.Menus.Enable();
        }
    }

    //Fires event for player pausing game, switches to Gameplay input
    private void OnPauseFromMenus(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnPauseInput?.Invoke(false);
            _playerInput.GamePlay.Enable();
            _playerInput.Menus.Disable();
        }
    }
}
