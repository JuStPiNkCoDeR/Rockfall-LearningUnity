using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    [Tooltip("The object to follow.")]
    public Transform target;

    [Tooltip("The vertical distance between the camera and the target object.")]
    public float height = 5.0f;

    [Tooltip("The distance to the target object without height.")]
    public float distance = 10.0f;

    [Tooltip("The camera slow down value during the target rotation.")]
    public float rotationDamping;

    [Tooltip("The camera slow down value during the target's height changes.")]
    public float heightDamping;

    // Calls after all Update methods. 
    private void LateUpdate()
    {
        // Exit on no target.
        if (!target)
        {
            return;
        }
        
        // Calculate suitable position and rotation.
        var targetPosition = target.position;
        var wantedRotationAngle = target.eulerAngles.y;
        var wantedHeight = targetPosition.y + height;
        
        // Extract current position and rotation.
        var currentTransform = transform;
        var currentRotationAngle = currentTransform.eulerAngles.y;
        var currentHeight = currentTransform.position.y;
        
        // Continue slow rotation around the y-axis.
        currentRotationAngle =
            Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);
        
        // Continue step-by-step correct height related to the target.
        currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);
        
        // Convert angle to rotate.
        var currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);
        
        // Set the camera location at x-z axis using "distance" meter from the target.
        var currentPosition = targetPosition - currentRotation * Vector3.forward * distance;
        // Set the camera location using the calculated height.
        currentPosition = new Vector3(currentPosition.x, currentHeight, currentPosition.z);
        transform.position = currentPosition;
        
        // Set the camera rotation following the target rotation.
        transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, rotationDamping * Time.deltaTime);
    }
}
