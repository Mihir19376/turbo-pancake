using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManagerScript : MonoBehaviour
{
    public bool isGameActive = false;
    public Button easyButton;
    public Button mediumButton;
    public Button hardButton;
    public GameObject menu;
    public GameObject enemySpawner;

    public GameObject gameOver;
    public Button continueButton;

    EnemySpawnerScript enemySpawnerScript;
    public bool hardDifficulty;
    public bool mediumDifficulty;
    public bool easyDifficulty;
    public bool hasGameBeenPlayed = false;
    // Start is called before the first frame update
    void Start()
    {
        hardDifficulty = false;
        mediumDifficulty = false;
        easyDifficulty = false;
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
        Debug.Log("Hard");
        isGameActive = true;
        hardDifficulty = true;
        enemySpawner.gameObject.SetActive(true);
        menu.SetActive(false);
        
    }

    void SetDifficultyToMedium()
    {
        enemySpawnerScript.multiplier = 2;
        Debug.Log("Medium");
        isGameActive = true;
        mediumDifficulty = true;
        enemySpawner.gameObject.SetActive(true);
        menu.SetActive(false);
    }

    void SetDifficultyToEasy()
    {
        Debug.Log("Easy");
        isGameActive = true;
        easyDifficulty = true;
        enemySpawner.gameObject.SetActive(true);
        menu.SetActive(false);
    }

    public void RestartGameth()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
