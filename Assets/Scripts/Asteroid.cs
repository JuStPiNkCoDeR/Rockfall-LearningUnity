using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [Tooltip("The asteroid movement speed.")]
    public float speed = 10.0f;
    
    // Start is called before the first frame update
    private void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * speed;

        var indicator = IndicatorManager.Instance.AddIndicator(gameObject, Color.red);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
