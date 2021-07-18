using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AsteroidSpawner : MonoBehaviour
{
    [Tooltip("The sphere radius where new asteroids are creating.")]
    public float radius = 250.0f;

    [Tooltip("The asteroid template to create.")]
    public Rigidbody asteroidPrefab;

    [Tooltip("Wait until the time +- variance before creating the new asteroid.")]
    public float spawnRate = 5.0f;

    public float variance = 1.0f;

    [Tooltip("The object where asteroids 'throwing'.")]
    public Transform target;

    [Tooltip("Should the object spawn new asteroids?")]
    public bool spawnAsteroids = false;
    
    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(CreateAsteroids());
    }

    IEnumerator CreateAsteroids()
    {
        while (true)
        {
            var nextSpawnTime = spawnRate + Random.Range(-variance, variance);

            yield return new WaitForSeconds(nextSpawnTime);

            yield return new WaitForFixedUpdate();

            CreateNewAsteroid();
        }
    }

    void CreateNewAsteroid()
    {
        if (!spawnAsteroids)
        {
            return;
        }

        var asteroidPosition = Random.onUnitSphere * radius;
        
        asteroidPosition.Scale(transform.lossyScale);

        asteroidPosition += transform.position;

        var newAsteroid = Instantiate(asteroidPrefab);

        newAsteroid.transform.position = asteroidPosition;
        
        newAsteroid.transform.LookAt(target);
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;

        Gizmos.matrix = transform.localToWorldMatrix;
        
        Gizmos.DrawWireSphere(Vector3.zero, radius);
    }

    public void DestroyAllAsteroids()
    {
        foreach (var asteroid in FindObjectsOfType<Asteroid>())
        {
            Destroy(asteroid.gameObject);
        }
    }
}
