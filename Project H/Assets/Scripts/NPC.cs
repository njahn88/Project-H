using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
{

    public void Interact(CharacterManager characterManager)
    {
        characterManager.StopAndRotate(transform.position);
    }
}
