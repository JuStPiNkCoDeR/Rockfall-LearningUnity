using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipWeapons : MonoBehaviour
{
    [Tooltip("The bullet template.")]
    public GameObject shotPrefab;

    [Tooltip("The weapons list.")]
    public Transform[] firePoints;

    private int _firePointIndex;

    public void Awake()
    {
        InputManager.Instance.SetWeapons(this);
    }

    public void OnDestroy()
    {
        if (Application.isPlaying)
        {
            InputManager.Instance.RemoveWeapons(this);
        }
    }

    public void Fire()
    {
        if (firePoints.Length == 0)
        {
            return;
        }

        var firePointToUse = firePoints[_firePointIndex];

        Instantiate(shotPrefab, firePointToUse.position, firePointToUse.rotation);

        _firePointIndex++;

        if (_firePointIndex >= firePoints.Length)
        {
            _firePointIndex = 0;
        }
    }
}
