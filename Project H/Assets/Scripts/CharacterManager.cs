using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    private CharacterController _characterController;
    [SerializeField] private LockManager _lockManager;

    private Vector2 _moveDirection;
    private GameObject _closestTarget;

    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _rotateSpeed = 10f;

    private const float _gravity = 9.8f;
    private bool _isLocked = false;
    private Vector3 _playerVelocity;

    private void OnEnable()
    {
        InputManager.OnMovementInput += HandleMovement;
        InputManager.OnLockInput += ToggleTargetLock;
        LockManager.NoTargets += DisableLock;
    }

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void OnDisable()
    {
        InputManager.OnMovementInput -= HandleMovement;
        InputManager.OnLockInput -= ToggleTargetLock;
        LockManager.NoTargets -= DisableLock;
    }

    private void Update()
    {
        MovePlayer();
        PlayerGravity();
    }
    #region Update Functions
    //Moves the player based on the angle of the camera 
    private void MovePlayer()
    {
        Vector3 turnedInputs = CameraBasedAngle();
        if (_moveDirection != Vector2.zero && !_isLocked)
        {
            gameObject.transform.forward = Vector3.Slerp(transform.forward, turnedInputs, Time.deltaTime * _rotateSpeed);
        }
        if(_isLocked && _closestTarget != null)
        {
            Vector3 targetDirection = _closestTarget.transform.position - transform.position;
            targetDirection.y = 0;
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            gameObject.transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * _rotateSpeed);
        }
        _characterController.Move(turnedInputs * _moveSpeed * Time.deltaTime);
    }

    //Determines if the player should be falling or not
    private void PlayerGravity()
    {
        _playerVelocity = Vector3.zero;
        if (!_characterController.isGrounded)
        {
            _playerVelocity.y -= _gravity;
            _characterController.Move(_playerVelocity * Time.deltaTime);
        }
    }
    #endregion
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

    //Toggles targeting and finds the closest target if there is one present
    private void ToggleTargetLock(bool isLocked)
    {
        _isLocked = isLocked;
        _closestTarget = _lockManager.FindClosestTarget();
    }

    private void DisableLock()
    {
        _isLocked = false;
    }
}
