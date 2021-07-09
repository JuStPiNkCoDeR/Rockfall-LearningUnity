using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipTarget : MonoBehaviour
{
    [Tooltip("The sprite as target point.")]
    public Sprite targetImage;
    
    // Start is called before the first frame update
    private void Start()
    {
        IndicatorManager.Instance.AddIndicator(gameObject, Color.yellow, targetImage);
    }
}
