using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemiesPrefabs;
    public GameObject[] powerupPrefabs;

    private PowerupType currentPowerup = PowerupType.None;
    
    private float zEnemySpawn = 11.0f;
    private float xSpawnRange = 9.0f;
    private float topPowerupRange = 8.0f;
    private float bottomPowerupRange = 0.0f;
    private float ySpawn = 0.5f;


    public void SpawnRandomEnemy()
    {
        float randomX = Random.Range(-xSpawnRange, xSpawnRange);
        int randomEnemy = Random.Range(0, enemiesPrefabs.Length);

        Vector3 spawnPos = new Vector3(randomX, ySpawn, zEnemySpawn);

        Instantiate(enemiesPrefabs[randomEnemy], spawnPos, enemiesPrefabs[randomEnemy].transform.rotation);
        
    }

     public void SpawnRandomPowerup()
    {
        float randomX = Random.Range(-xSpawnRange, xSpawnRange);
        float randomZ = Random.Range(bottomPowerupRange, topPowerupRange);

        int randomPowerup = Random.Range(0, powerupPrefabs.Length);

        Vector3 spawnPos = new Vector3(randomX, ySpawn, randomZ);

        Instantiate(powerupPrefabs[randomPowerup], spawnPos, powerupPrefabs[randomPowerup].transform.rotation);

        currentPowerup = powerupPrefabs[randomPowerup].gameObject.GetComponent<Powerup>().powerupType;
     }
}
