using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float speed;

    private float zBoundry = -3.0f;

    public GameManager gameManager;
    public PlayerController playerController;
    

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();   
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        if (transform.position.z < zBoundry)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        // Has to add condition if Shield is active
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            if (playerController.currentPowerup != PowerupType.Shield)
            {
                gameManager.UpdateLives(-1);
                playerController.playerAudio.PlayOneShot(playerController.destroySound, 1.0f);
            }
            
            if(gameManager.lives <= 0)
            {
                Destroy(other.gameObject, 0.5f);
                playerController.playerAudio.PlayOneShot(playerController.destroySound, 1.0f);
                playerController.explosionParticle.Play();
            }
        }

    }
}
