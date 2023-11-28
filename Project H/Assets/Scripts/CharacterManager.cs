using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    private CharacterController _characterController;

    private Vector2 _moveDirection;

    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _rotateSpeed = 10f;

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
        Vector3 turnedInputs = CameraBasedAngle();
        if (_moveDirection != Vector2.zero)
        {
            gameObject.transform.forward = Vector3.Slerp(transform.forward, turnedInputs, Time.deltaTime * _rotateSpeed);
        }
        _characterController.Move(turnedInputs * _moveSpeed * Time.deltaTime);
    }

    //Adds the given rotation of the camera into the movement of the player
    private Vector3 CameraBasedAngle()
    {
        float cameraAngle = Camera.main.transform.eulerAngles.y;
        Vector3 moveDir = new Vector3(_moveDirection.x, 0, _moveDirection.y);
        return Quaternion.Euler(0, cameraAngle, 0) * moveDir;
    }

    //Gets the movement vector from InputManager and moves character
    private void HandleMovement(Vector2 moveDirection)
    {
        _moveDirection = moveDirection;
    }
}
