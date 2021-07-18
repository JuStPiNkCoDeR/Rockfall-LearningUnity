using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTaking : MonoBehaviour
{
    [Tooltip("The objects health points.")]
    public int hitPoint = 10;

    [Tooltip("The object to create on destruct.")]
    public GameObject destructionPrefab;

    [Tooltip("End game if the object destroyed?")]
    public bool gameOverOnDestroyed = false;

    public void TakeDamage(int amount)
    {
        Debug.Log(gameObject.name + " damaged!");

        hitPoint -= amount;

        if (hitPoint <= 0)
        {
            Debug.Log(gameObject.name + " destroyed!");
            
            Destroy(gameObject);

            if (destructionPrefab != null)
            {
                Instantiate(destructionPrefab, transform.position, transform.rotation);
            }
        }
    }
}
