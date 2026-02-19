using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class moving : MonoBehaviour
{
    const float SPEED = 5f;
    private float tickTimer = 0f;
    const float TICK_INTERVAL = 2f;
    
    public GameObject bulletPrefab;
    private List<GameObject> bulletPool = new List<GameObject>();
    const int POOL_SIZE = 10;
    
    // Start is called before the first frame update
    void Start()
    {
        //print something
        Debug.Log("kontol shift subscript");
        
        // Initialize object pool
        if (bulletPrefab != null)
        {
            for (int i = 0; i < POOL_SIZE; i++)
            {
                GameObject bullet = Instantiate(bulletPrefab);
                bullet.SetActive(false);
                bulletPool.Add(bullet);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //movement 2d - normalized
        Vector3 movementDirection = Vector3.zero;
        
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            movementDirection += Vector3.up;
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            movementDirection += Vector3.down;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            movementDirection += Vector3.left;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            movementDirection += Vector3.right;
        
        if (movementDirection.magnitude > 0)
        {
            movementDirection.Normalize();
            transform.Translate(movementDirection * Time.deltaTime * SPEED);
        }
        
        // Timer that ticks every 2 seconds
        tickTimer += Time.deltaTime;
        if (tickTimer >= TICK_INTERVAL)
        {
            OnTick();
            tickTimer = 0f;
        }
    }
    
    void OnTick()
    {
        ShootBullet();
    }
    
    void ShootBullet()
    {
        // Get a bullet from the pool
        GameObject bullet = null;
        foreach (GameObject pooledBullet in bulletPool)
        {
            if (!pooledBullet.activeInHierarchy)
            {
                bullet = pooledBullet;
                break;
            }
        }
        
        if (bullet == null)
        {
            Debug.LogWarning("No available bullets in pool!");
            return;
        }
        
        // Random direction
        float randomAngle = Random.Range(0f, 360f);
        Vector3 randomDirection = new Vector3(Mathf.Cos(randomAngle * Mathf.Deg2Rad), Mathf.Sin(randomAngle * Mathf.Deg2Rad), 0f);
        
        // Position bullet at player
        bullet.transform.position = transform.position;
        
        // Initialize bullet
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.Initialize(randomDirection);
        }
        
        // Activate bullet
        bullet.SetActive(true);
    }
}
