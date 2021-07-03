using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class VirtualJoystick : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Tooltip("The sprite moving on the screen.")] public RectTransform thumb;

    [Tooltip("The distance that a finger has moved.")]
    public Vector2 delta;
    // The finger and joystick position on movement.
    private Vector2 _originalPosition;

    private Vector2 _originalThumbPosition;
    
    // Start is called before the first frame update.
    private void Start()
    {
        // At the beginning save initial position.
        _originalPosition = GetComponent<RectTransform>().localPosition;
        _originalThumbPosition = thumb.localPosition;
        
        // Disable thumb.
        thumb.gameObject.SetActive(false);
        
        // Restore delta to zero. 
        delta = Vector2.zero;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Make the thumb be active.
        thumb.gameObject.SetActive(true);
        
        // Fix world position where the movement has begun.
        var worldPoint = new Vector3();
        RectTransformUtility.ScreenPointToWorldPointInRectangle(
            transform as RectTransform,
            eventData.position,
            eventData.enterEventCamera,
            out worldPoint);
        
        // Place the joystick to that position.
        GetComponent<RectTransform>().position = worldPoint;
        
        // Place the plane in source position in relation to the joystick.
        thumb.localPosition = _originalThumbPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Calculate the current world position of the finger touch with the screen.
        var worldPoint = new Vector3();
        RectTransformUtility.ScreenPointToWorldPointInRectangle(transform as RectTransform, eventData.position,
            eventData.enterEventCamera, out worldPoint);
        
        // Place the plane in the point.
        thumb.position = worldPoint;
        
        // Calculate the delta with the source point.
        var size = GetComponent<RectTransform>().rect.size;

        delta = thumb.localPosition;

        delta.x /= size.x / 2.0f;
        delta.y /= size.y / 2.0f;

        delta.x = Mathf.Clamp(delta.x, -1.0f, 1.0f);
        delta.y = Mathf.Clamp(delta.y, -1.0f, 1.0f);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Reset the joystick position.
        GetComponent<RectTransform>().localPosition = _originalPosition;
        
        // Set the delta as zero.
        delta = Vector2.zero;
        
        // Hide the plane.
        thumb.gameObject.SetActive(false);
    }
}
