using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnRange = 30;
    public int enemyCount;
    public int waveNumber = 3;
    private int score;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI enemiesLeft;

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemyWave(waveNumber);
        UpdateScore(0);
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;

        if (enemyCount == 0)
        {
            UpdateScore(1);
            waveNumber++;
            SpawnEnemyWave(waveNumber);
        }

        UpdateEnemiesLeft();
    }

    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosY = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnPosX, 2.5f, spawnPosY);
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
