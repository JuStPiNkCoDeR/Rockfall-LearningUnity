using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceStation : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        IndicatorManager.Instance.AddIndicator(gameObject, Color.green);
    }
}
