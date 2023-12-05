using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
{
    public static event Action<string> OnDialogueChange;
    public static event Action OnDialogueFinished;
    private int _dialogueIndex = 0;
    [SerializeField]
    private List<string> _dialogue = new List<string>();

    public void Interact(CharacterManager characterManager)
    {
        if(_dialogueIndex == 0)
        {
            characterManager.StopAndRotate(transform.position);
        }
        if(_dialogueIndex != _dialogue.Count)
        {
            OnDialogueChange?.Invoke(_dialogue[_dialogueIndex]);
            _dialogueIndex++;
        }
        else
        {
            _dialogueIndex = 0;
            OnDialogueFinished?.Invoke();
            characterManager.DoneTalking();
        }
    }
}
