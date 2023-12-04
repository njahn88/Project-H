using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockManager : MonoBehaviour
{
    public static event Action NoTargets;
    private List<GameObject> _targetList;

    private void OnEnable()
    {
        _targetList = new List<GameObject>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Lockable"))
        {
            Debug.Log("added to list");
            _targetList.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Lockable"))
        {
            Debug.Log("removed from list");
            _targetList.Remove(other.gameObject);
            if (!HasTarget())
            {
                NoTargets?.Invoke();
            }
        }
    }

    //Finds the closest target to the player if target button is pressed
    public GameObject FindClosestTarget()
    {
        if(_targetList.Count == 0)
        {
            return null;
        }
        Vector3 currentPosition = transform.position;
        float closestDistance = 1000;
        GameObject closestObject = null;
        foreach (GameObject target in _targetList)
        {
            float distance = Vector3.Distance(target.transform.position, currentPosition);
            if (distance < closestDistance)
            {
                closestObject = target;
                closestDistance = distance;
            }
        }
        return closestObject;
    }

    public bool HasTarget()
    {
        return _targetList.Count > 0;
    }
}
