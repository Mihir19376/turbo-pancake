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
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI enemiesLeft;
    public bool isGameActive = false;
    public Button easyButton;
    public Button mediumButton;
    public Button hardButton;
    public GameObject menu;

    // Start is called before the first frame update
    void Start()
    {
        hardButton.onClick.AddListener(SetDifficultyToHard);
        mediumButton.onClick.AddListener(SetDifficultyToMedium);
        easyButton.onClick.AddListener(SetDifficultyToEasy);

        SpawnEnemyWave(waveNumber);
        UpdateScore(0);
        scoreText.gameObject.SetActive(true);
        enemiesLeft.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameActive == true)
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

    void SetDifficultyToHard()
    {
        Debug.Log("Hard");
        isGameActive = true;
        menu.SetActive(false);
    }

    void SetDifficultyToMedium()
    {
        Debug.Log("Medium");
        isGameActive = true;
        menu.SetActive(false);
    }

    void SetDifficultyToEasy()
    {
        Debug.Log("Easy");
        isGameActive = true;
        menu.SetActive(false);
    }

}
