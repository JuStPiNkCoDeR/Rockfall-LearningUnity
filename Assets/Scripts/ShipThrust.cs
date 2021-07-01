using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipThrust : MonoBehaviour
{
    [Tooltip("Moving speed.")]
    public float speed = 5.0f;

    // Update is called once per frame.
    // Moves the ship towards with constant speed.
    private void Update()
    {
        var offset = Vector3.forward * (Time.deltaTime * speed);
        transform.Translate(offset);
    }
}
