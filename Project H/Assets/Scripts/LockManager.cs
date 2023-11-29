using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockManager : MonoBehaviour
{
    private List<GameObject> _targetList;

    private void OnEnable()
    {
        _targetList = new List<GameObject>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Lockable"))
        {
            _targetList.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Lockable"))
        {
            _targetList.Remove(other.gameObject);
        }
    }

    public GameObject FindClosestTarget()
    {
        return null;
    }
}
