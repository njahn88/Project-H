using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    private CharacterController _characterController;

    private Vector3 _moveDirection;

    [SerializeField] private float _moveSpeed = 5f;

    private void OnEnable()
    {
        InputManager.OnMovementInput += HandleMovement;
    }

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void OnDisable()
    {
        InputManager.OnMovementInput -= HandleMovement;
    }

    private void Update()
    {
        if (_moveDirection != Vector3.zero)
        {
            gameObject.transform.forward = _moveDirection;
        }
        _characterController.Move(_moveDirection);
    }

    //Gets the movement vector from InputManager and moves character
    private void HandleMovement(Vector2 moveDirection)
    {
        _moveDirection = new Vector3(moveDirection.x, 0, moveDirection.y) * Time.deltaTime * _moveSpeed;
    }
}
