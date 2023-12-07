using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InSceneDoor : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject _positionToTeleport, _positionToMove;
    public void Interact(CharacterManager characterManager)
    {
        characterManager.DoorTeleport(_positionToMove.transform.position, _positionToTeleport.transform.position);
    }

}
