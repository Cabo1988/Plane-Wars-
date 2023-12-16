using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    public SpawnManager spawnManager;
    public GameObject titleScreen;
    public GameObject pausedScreen;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI livesText;

    public Button restartButton;
     
    public bool isGameActive;
    private bool paused;

    private float spawnRateEnemy;
    private float spawnRatePowerup = 10f; 
    private int score;
    public int lives;
      
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            ChangePaused();
        }
    }

    IEnumerator SpawnEnemy()
    {
        while(isGameActive)
        {
            spawnRateEnemy = Random.Range(1f, 2.5f);
            yield return new WaitForSeconds(spawnRateEnemy);
            spawnManager.SpawnRandomEnemy();
        }
    }

    IEnumerator SpawnPowerup()
    {
        while(isGameActive)
        {
            spawnRatePowerup = Random.Range(10f, 15f);
            yield return new WaitForSeconds(spawnRatePowerup);
            spawnManager.SpawnRandomPowerup();
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void UpdateLives(int livesToChange)
    {
        lives += livesToChange;
        livesText.text = "Lives: " + lives;

        if (lives <= 0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        isGameActive = false;
        restartButton.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        // We can also use "name" of the scene we want to reload
        SceneManager.LoadScene("My Game");
    }

    public void StartGame()
    {
        isGameActive = true;
        score = 0;
        UpdateScore(0);
        UpdateLives(3);

        StartCoroutine(SpawnEnemy());
        StartCoroutine(SpawnPowerup());

        titleScreen.gameObject.SetActive(false);
    }

    void ChangePaused()
    { 
        if(!paused)
        {
            paused = true;
            pausedScreen.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            paused = false;
            pausedScreen.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
