using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public GameManager gameManager;
    public Move enemy;
    public float speed;

    private float zBoundry = 11.0f;
    
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        if (transform.position.z > zBoundry)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") && gameManager.isGameActive)
        {
            enemy = other.gameObject.GetComponent<Move>();
            enemy.DestroyEnemy();
            Destroy(gameObject);
        }
                  
    }
}
