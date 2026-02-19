using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 2f;
    private Vector3 direction;
    private float spawnTime;

    public void Initialize(Vector3 shootDirection)
    {
        direction = shootDirection.normalized;
        spawnTime = Time.time;
    }

    void Update()
    {
        // Move the bullet
        transform.Translate(direction * speed * Time.deltaTime);

        // Check if lifetime has expired
        if (Time.time - spawnTime >= lifetime)
        {
            // Deactivate for object pooling
            gameObject.SetActive(false);
        }
    }
}
