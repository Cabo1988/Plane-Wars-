using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{

    public GameObject projectilePrefab;
    private AudioSource enemyProjectileAudio;

    public AudioClip enemyShoot;

    private Vector3 offset = new Vector3(0, 0.5f, 0);   
    
    void Start()
    {
        enemyProjectileAudio = GetComponent<AudioSource>();
        InvokeRepeating("Shoot", 0.5f, 2.0f);
    }

    void Shoot()
    {
        Instantiate(projectilePrefab, transform.position + offset, projectilePrefab.transform.rotation);
        enemyProjectileAudio.PlayOneShot(enemyShoot, 1.0f);
    }
}

