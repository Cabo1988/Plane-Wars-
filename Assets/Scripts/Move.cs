using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// should be called enemy not Move!!!
public class Move : MonoBehaviour
{

    public float speed;
    public GameManager gameManager;
    public ParticleSystem explosionParticle;
    public AudioClip destroySound;
    public AudioSource enemyAudio;
    
    private float lowerBoundry = -3.0f;
    private float upperBoundry = 11.0f;

    public int scoreValue;

    private Rigidbody objectRb;
    

    void Start()
    {
        objectRb = GetComponent<Rigidbody>();   
        enemyAudio = GetComponent<AudioSource>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        objectRb.AddForce(Vector3.forward * speed);
        
        if(transform.position.z < lowerBoundry || transform.position.z > upperBoundry)
        {
            Destroy(gameObject);
        }
    }

    public void DestroyEnemy()
    {
        explosionParticle.Play();
         enemyAudio.PlayOneShot(destroySound, 1.0f);
        gameManager.UpdateScore(scoreValue);
        Destroy(gameObject, 0.2f);
    }
}
