using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManagerScript : MonoBehaviour
{
    public int maxXRange = 50;
    public int maxZRange = 50;

    public bool isGameActive = false;
    public Button easyButton;
    public Button mediumButton;
    public Button hardButton;
    public GameObject menu;
    
    public GameObject gameOver;
    public Button continueButton;

    public GameObject enemySpawner;
    EnemySpawnerScript enemySpawnerScript;

    public bool hardDifficulty;
    public bool mediumDifficulty;
    public bool easyDifficulty;
    public bool hasGameBeenPlayed = false;

    //public int highScore = 0;
    //public TextMeshProUGUI highScoreText;

    public GameObject enemyPrefab;
    Enemy enemyPrefabScript;

    // Start is called before the first frame update
    void Start()
    {
        //highScoreText.text = "High Score: " + highScore;
        hardDifficulty = false;
        mediumDifficulty = false;
        easyDifficulty = false;
        enemyPrefabScript = enemyPrefab.GetComponent<Enemy>();
        enemySpawnerScript = enemySpawner.GetComponent<EnemySpawnerScript>();
        enemySpawner.gameObject.SetActive(false);
        hardButton.onClick.AddListener(SetDifficultyToHard);
        mediumButton.onClick.AddListener(SetDifficultyToMedium);
        easyButton.onClick.AddListener(SetDifficultyToEasy);
    }

    // Update is called once per frame
    void Update()
    {
        if (hasGameBeenPlayed == true)
        {
            gameOver.SetActive(true);
            //continueButton.onClick.AddListener(RestartGameth);
            //RestartGameth();
        }
    }

    void SetDifficultyToHard()
    {
        enemySpawnerScript.multiplier = 3;
        enemyPrefabScript.speed = 3f;
        Debug.Log("Hard");
        isGameActive = true;
        hardDifficulty = true;
        enemySpawner.SetActive(true);
        menu.SetActive(false);
        
    }

    void SetDifficultyToMedium()
    {
        enemySpawnerScript.multiplier = 2;
        enemyPrefabScript.speed = 2f;
        Debug.Log("Medium");
        isGameActive = true;
        mediumDifficulty = true;
        enemySpawner.SetActive(true);
        menu.SetActive(false);
    }

    void SetDifficultyToEasy()
    {
        enemySpawnerScript.multiplier = 1;
        enemyPrefabScript.speed = 1f;
        Debug.Log("Easy");
        isGameActive = true;
        easyDifficulty = true;
        enemySpawner.SetActive(true);
        menu.SetActive(false);
    }

    public void RestartGameth()
    {
        //highScore = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
