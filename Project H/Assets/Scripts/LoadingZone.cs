using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BoxCollider))]
public class LoadingZone : MonoBehaviour
{
    //Point for player to move to once in loading zone
    [SerializeField] private GameObject _moveToPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<CharacterManager>(out CharacterManager characterManager))
        {
            characterManager.StopAndMoveToPoint(_moveToPoint.transform.position);
        }
    }
}
