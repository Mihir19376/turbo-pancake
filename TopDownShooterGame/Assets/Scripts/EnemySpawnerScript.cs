using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EnemySpawnerScript : MonoBehaviour
{
    public GameObject enemyPrefab;
    private float spawnRange = 20;
    public int enemyCount;
    public int waveNumber = 3;
    public int winningScore = 5;
    public int score;
    public int multiplier;

    public GameObject gameWonMenu;
    public Button continueButton;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI enemiesLeft;

    GameManagerScript gameManagerScript;
    public GameObject gameManager;

    private AudioSource enemySpawnerAudioSource;
    public AudioClip newEnemyWaveSound;

    // Start is called before the first frame update
    void Start()
    {
        enemySpawnerAudioSource = GetComponent<AudioSource>();
        score = 0;
        gameManagerScript = gameManager.GetComponent<GameManagerScript>();
        SpawnEnemyWave(waveNumber);
        UpdateScore(0);
        scoreText.gameObject.SetActive(true);
        enemiesLeft.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManagerScript.isGameActive == true)
        {
            enemyCount = FindObjectsOfType<EnemyPrefabScript>().Length;

            if (score == winningScore)
            {
                gameWonMenu.SetActive(true);
                gameManagerScript.isGameActive = false;

            }

            if (enemyCount == 0)
            {
                UpdateScore(1);

                if (gameManagerScript.easyDifficulty == true)
                {
                    waveNumber++;
                }
                else if (gameManagerScript.mediumDifficulty == true)
                {
                    waveNumber *= multiplier;
                }
                else if (gameManagerScript.hardDifficulty == true)
                {
                    waveNumber *= multiplier;
                }

                SpawnEnemyWave(waveNumber);
            }

            UpdateEnemiesLeft();
        }

        GenerateSpawnPosition();
        
    }

    

    /// <summary>
    /// 
    /// </summary>
    /// <returns>A random spawn Position for a enemy as a Vector 3</returns>
    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(transform.position.x - spawnRange, transform.position.x + spawnRange);
        float spawnPosZ = Random.Range(transform.position.z - spawnRange, transform.position.z + spawnRange);
        Vector3 randomPos = new Vector3(spawnPosX, .5f, spawnPosZ);
        return randomPos;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="enemiesToSpawn">Numbers of enmies you want spawned in this wave</param>
    void SpawnEnemyWave(int enemiesToSpawn)
    {
        enemySpawnerAudioSource.PlayOneShot(newEnemyWaveSound, 1);
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="scoreToAdd">What interger you want to update the scroe by</param>
    private void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    /// <summary>
    /// Whenever this method is called, it will update the Enemies left UI with
    /// the current amount of enemies left (the enemy count, whcih is a count
    /// of all the enemies left on the screen with the EnemyScript on them)
    /// </summary>
    private void UpdateEnemiesLeft()
    {
        enemiesLeft.text = "Enemies Left: " + enemyCount;
    }

}
