using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject projectilePrefab;
    public GameManager gameManager;
    public Move enemy;
    public PowerupType currentPowerup = PowerupType.None;
    public GameObject powerupIndicator;
    public AudioClip shootSound;
    public AudioClip destroySound;
    public AudioClip powerupPickupSound;
    public ParticleSystem explosionParticle;

    private Coroutine powerupCountdown;
    private Rigidbody playerRb;
    public AudioSource playerAudio;


    private float speed = 60.0f;
    private float topBoundry = 8.0f;
    private float lowerBoundry = 1.0f;
    private float xBoundry = 9.0f;
    private float bounceForce = 10.0f;
    
    private Vector3 offset = new Vector3(0, 0.3f, 0);
    private Vector3 powerupIndicatorOffset = new Vector3(0, 0.5f, 0);
            
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>(); 
        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        ConstrainPlayerPosition();
        
        // Fire a missile
        if (Input.GetKeyDown(KeyCode.Space) && gameManager.isGameActive)
        {
            Instantiate(projectilePrefab, transform.position + offset, projectilePrefab.transform.rotation);
            playerAudio.PlayOneShot(shootSound, 1.0f);
            
        }
        
        // Deploy the Bomb
        if(Input.GetKeyDown(KeyCode.F) && gameManager.isGameActive && currentPowerup == PowerupType.Bomb) 
        {
            DeployBomb();
        }

        //set the powerup position to follow player
        powerupIndicator.transform.position =  transform.position + powerupIndicatorOffset;
    }

    //Moves the player based on arrow key inputs
    void MovePlayer()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        playerRb.AddForce(Vector3.forward * speed * verticalInput);
        playerRb.AddForce(Vector3.right * speed * horizontalInput);
    }

    //Contrain the player from moving out of the screen edges
    void ConstrainPlayerPosition()
    {
        if (transform.position.z < lowerBoundry)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, lowerBoundry);
        }

        if (transform.position.z > topBoundry)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, topBoundry);
        }

        if (transform.position.x < -xBoundry)
        {
            transform.position = new Vector3(-xBoundry, transform.position.y, transform.position.z);
            playerRb.AddForce(Vector3.right * bounceForce, ForceMode.Impulse);
        }

        if (transform.position.x > xBoundry)
        {
            transform.position = new Vector3(xBoundry, transform.position.y, transform.position.z);
            playerRb.AddForce(Vector3.left * bounceForce, ForceMode.Impulse);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            currentPowerup = other.gameObject.GetComponent<Powerup>().powerupType;
            Destroy(other.gameObject);
            powerupIndicator.gameObject.SetActive(true);
            playerAudio.PlayOneShot(powerupPickupSound, 1.0f);
            
            if (powerupCountdown != null)
            {
                StopCoroutine(powerupCountdown);
            }
            powerupCountdown = StartCoroutine(PowerupCountdownRoutine());

            // Active powerup countdown
            IEnumerator PowerupCountdownRoutine()
            {
               yield return new WaitForSeconds(5);
               currentPowerup = PowerupType.None;
               powerupIndicator.gameObject.SetActive(false);
            }
        }       
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            enemy = collision.gameObject.GetComponent<Move>();
            enemy.DestroyEnemy();
            playerAudio.PlayOneShot(destroySound, 1.0f);
            
            if (currentPowerup != PowerupType.Shield)
            {
                gameManager.UpdateLives(-1);
            }
            
            if (gameManager.lives <= 0)
            {
                playerAudio.PlayOneShot(destroySound, 1.0f);
                explosionParticle.Play();
                Destroy(gameObject, 0.5f);
                powerupIndicator.gameObject.SetActive(false);
            }
        }
    }

    void DeployBomb()
    {
        foreach (var enemy in FindObjectsOfType<Move>())
        {
            enemy.DestroyEnemy();
        }

        foreach (var enemyProjectile in FindObjectsOfType<EnemyProjectile>())
        { 
            Destroy(enemyProjectile.gameObject);
        }

        currentPowerup = PowerupType.None;
        powerupIndicator.gameObject.SetActive(false);
    }
}
