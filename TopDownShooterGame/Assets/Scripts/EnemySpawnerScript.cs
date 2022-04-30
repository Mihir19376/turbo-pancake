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
    private int score;
    public int multiplier;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI enemiesLeft;
    GameManagerScript gameManagerScript;
    public GameObject gameManager;

    // Start is called before the first frame update
    void Start()
    {
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
            enemyCount = FindObjectsOfType<Enemy>().Length;

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
        
    }

    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(transform.position.x - spawnRange, transform.position.x + spawnRange);
        float spawnPosZ = Random.Range(transform.position.z - spawnRange, transform.position.z + spawnRange);
        Vector3 randomPos = new Vector3(spawnPosX, .5f, spawnPosZ);
        return randomPos;
    }

    void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }
    }

    private void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    private void UpdateEnemiesLeft()
    {
        enemiesLeft.text = "Enemies Left: " + enemyCount;
    }

}
