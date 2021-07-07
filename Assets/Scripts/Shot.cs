using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    [Tooltip("The bullet speed.")]
    public float speed = 100.0f;

    [Tooltip("The bullet 'life' duration.")]
    public float life = 5.0f;
    
    // Start is called before the first frame update
    private void Start()
    {
        Destroy(gameObject, life);
    }

    // Update is called once per frame
    private void Update()
    {
        transform.Translate(Vector3.forward * (speed * Time.deltaTime));
    }
}
