using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Indicator : MonoBehaviour
{
    [Tooltip("The object to follow.")]
    public Transform target;

    [Tooltip("The distance between the target and this object.")]
    public Transform showDistanceTo;

    [Tooltip("The label for distance view.")]
    public Text distanceLabel;

    [Tooltip("The offset from screen bord.")]
    public int margin = 50;

    // The color of the image.
    public Color color
    {
        set => GetComponent<Image>().color = value;
        get => GetComponent<Image>().color;
    }
    
    
    // Start is called before the first frame update
    private void Start()
    {
        distanceLabel.enabled = false;
        GetComponent<Image>().enabled = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        if (showDistanceTo != null)
        {
            distanceLabel.enabled = true;

            var distance = (int)Vector3.Magnitude(showDistanceTo.position - target.position);

            distanceLabel.text = distance.ToString() + "m";
        }
        else
        {
            distanceLabel.enabled = false;
        }

        GetComponent<Image>().enabled = true;

        var viewportPoint = Camera.main.WorldToViewportPoint(target.position);

        // The target is behind us?
        if (viewportPoint.z < 0)
        {
            viewportPoint.z = 0;
            viewportPoint = viewportPoint.normalized;
            viewportPoint.x *= -Mathf.Infinity;
        }

        var screenPoint = Camera.main.ViewportToScreenPoint(viewportPoint);

        screenPoint.x = Mathf.Clamp(screenPoint.x, margin, Screen.width - margin * 2);
        screenPoint.y = Mathf.Clamp(screenPoint.y, margin, Screen.height - margin * 2);

        var localPosition = new Vector2();
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            transform.parent.GetComponent<RectTransform>(), screenPoint, Camera.main, out localPosition);

        var rectTransform = GetComponent<RectTransform>();
        rectTransform.localPosition = localPosition;
    }
}
