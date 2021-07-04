using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSteering : MonoBehaviour
{
    [Tooltip("The rotation speed.")]
    public float turnRate = 6.0f;

    [Tooltip("The strength of the ship alignment.")]
    public float levelDamping = 1.0f;

    // Update is called once per frame
    private void Update()
    {
        var steeringInput = InputManager.Instance.steering.delta;
        var rotation = new Vector2();

        rotation.x = steeringInput.x;
        rotation.y = steeringInput.y;
        rotation *= turnRate;
        rotation.x = Mathf.Clamp(rotation.x, -Mathf.PI * 0.9f, Mathf.PI * 0.9f);

        var newOrientation = Quaternion.Euler(rotation);
        transform.rotation *= newOrientation;

        var levelAngles = transform.eulerAngles;
        levelAngles.z = 0.0f;

        var levelOrientation = Quaternion.Euler(levelAngles);

        transform.rotation = Quaternion.Slerp(transform.rotation, levelOrientation, levelDamping * Time.deltaTime);
    }
}
