using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour, PlayerInput.IGamePlayActions, PlayerInput.IMenusActions
{
    public static event Action<Vector2> OnMovementInput;
    public static event Action<bool> OnPauseInput;
    public static event Action<bool> OnLockInput;
    public static InputManager Instance;
    private PlayerInput _playerInput;

    private bool _inMenus = false;

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
        //_playerInput.Disable();
    }
    //Set the base inputs to Menus
    private void SetupStartingInput()
    {
        _playerInput.Enable();
        EnableMenuInputs();
        SetupInputConnections();
    }

    //Subscribes functions to corresponding events in Gameplay InputAction
    private void SetupInputConnections()
    {
        _playerInput.GamePlay.SetCallbacks(this);
        _playerInput.Menus.SetCallbacks(this);
    }

    #region InputEvents
    //Fires event for player movement sending a Vector2
    public void OnMovement(InputAction.CallbackContext context)
    {
        //Debug.Log("Input: " + context.ReadValue<Vector2>());
        if (context.phase == InputActionPhase.Canceled)
        {
            OnMovementInput?.Invoke(new Vector2(0, 0));
            return;
        }
        OnMovementInput?.Invoke(context.ReadValue<Vector2>());
    }

    //Fires event for player pausing game, switches to Menus input
    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (!_inMenus)
            {
                EnableMenuInputs();
            }
            else
            {
                EnableGamePlayInputs();
            }
            OnPauseInput?.Invoke(_inMenus);
            _inMenus = !_inMenus;
        }
    }
    //Used for menu navigation cause Unity or I am dumb
    public void OnNavigate(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed) OnMovementInput?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnLock(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            OnLockInput?.Invoke(true);
            Debug.Log("Shift pressed");
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            OnLockInput?.Invoke(false);
            Debug.Log("Shift released");
        }
    }
        #endregion
        public void DisableAllInputs()
    {
        _playerInput.Disable();
    }

    public void EnableAllInputs()
    {
        _playerInput.Enable();
    }
    //Enables menu inputs and disables gameplay inputs
    public void EnableMenuInputs()
    {
        _playerInput.GamePlay.Disable();
        _playerInput.Menus.Enable();
    }
    //Enables Gameplay inputs and disables menu inputs

    public void EnableGamePlayInputs()
    {
        _playerInput.GamePlay.Enable();
        _playerInput.Menus.Disable();
    }
}
