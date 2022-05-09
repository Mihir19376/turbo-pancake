using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

/// <summary>
/// The Game Manager script which is the script that controls and sets many
/// menu setting and difficulty booleans
/// </summary>
public class GameManagerScript : MonoBehaviour
{
    // Max z and x ranges the bullets and player can go
    public int maxXRange = 50;
    public int maxZRange = 50;

    // a univeral boolaen letting other scripts know ehnt he game is active and
    // so when to star certain functions (for instance, I dont want the player
    // to be able to move or to shoot while the player is still in the menu)
    public bool isGameActive = false;

    // Buttons to choose the difficulty modes and the main menu game obejct
    public Button easyDifficultyButton;
    public Button mediumDifficultyButton;
    public Button hardDifficultyButton;
    // The main menu enmpty game obejct where all the main menu buttona and
    // texts are stored as child objects. Set this to active or not acrive,
    // will autiomaticlly set everything it carry to active or not too
    public GameObject mainMenu;

    // the gameoverandlost menu game object it doest the same thing as the one
    // above, but is called when the player looses
    public GameObject gameOverAndLost;
    // A contines button, to continue back to the main menu (by reloading the
    // scene)
    public Button continueButton;

    // EnemySpawener where the enemySpawnerScript is stroed andextracted in the
    // script
    public GameObject enemySpawner;
    // Enemy spawner script used to change the wave multiplier and to check the
    // current score
    EnemySpawnerScript enemySpawnerScript;

    // The difficulty booleans which when one is set to true, changes the factor
    // of the eenmies spawn rate
    public bool hardDifficulty;
    public bool mediumDifficulty;
    public bool easyDifficulty;
    // used to check is that game has been played 
    public bool hasGameBeenPlayed = false;

    // the final score before loosing and its text obejct on whcih is displayed
    // on the lose menu
    public int finalScore = 0;
    public TextMeshProUGUI finalScoreText;

    // the enemy prefab where the enemyPrefabScript will be extracted as a
    // component to changes the speed of the nemies based on the difficulty
    // level chosen
    public GameObject enemyPrefab;
    EnemyPrefabScript enemyPrefabScript;

    // Start is called before the first frame update
    void Start()
    {
        // At the start of the game all the difficulty booleans are set to false
        // as a mode hasn't been picked yet
        hardDifficulty = false;
        mediumDifficulty = false;
        easyDifficulty = false;

        // Gets the EnemyPrefabScript component from the enemyPrefab and stores
        // it in a variable named enemyPrefabScript
        enemyPrefabScript = enemyPrefab.GetComponent<EnemyPrefabScript>();
        // Gets the EnemySpawnerScript component from the enemyPrefab and stores
        // it in a variable named enemySpawnerScript
        enemySpawnerScript = enemySpawner.GetComponent<EnemySpawnerScript>();

        // The enemySpawner gameobject is set to false, meaning itis not acitve
        // in heirachy and this means that the script inthe herachy is also not
        // active
        enemySpawner.gameObject.SetActive(false);

        // for eash of the buttons, on their click they carry out there
        // respective set difficulty methods
        hardDifficultyButton.onClick.AddListener(SetDifficultyToHard);
        mediumDifficultyButton.onClick.AddListener(SetDifficultyToMedium);
        easyDifficultyButton.onClick.AddListener(SetDifficultyToEasy);
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the game has been played, by chekcing if the hasGameBeenPlayed
        // is set to true. If so it carryies out the following code, if not then
        // it does nothing
        if (hasGameBeenPlayed == true)
        {
            // The fianl score is set equal to the score right before the game
            // ended (derived from enemySpawnerScript)
            finalScore = enemySpawnerScript.score;
            // Sets the text component of the finalScoreText show the
            // "Final Score: " + finalScore. e.g if the fianl scfroe before
            // loosing was 2 it would show "Final Score: 2"
            finalScoreText.text = "Final Score: " + finalScore;
            // Sets the gameOver boolean to true, whcih other scripts and this
            // scripts will use as a way to tell the gam eis over and carry out
            // some code that shoudl stop all gameplay actions
            gameOverAndLost.SetActive(true);
        }
    }

    /// <summary>
    /// When the follwoing 3 methods is called the follwowing happens:
    ///  - the enemySpawnerScripts' multiplier is set to 3 for Hard 2 for Medium
    ///  and 1 for Easy, which means the spawn rate goes up by 3, 2, or 1 every wave
    ///  - the enemySpeed (derived from the enemySpawnerScript)is set is set to
    ///  3 for Hard 2 for Medium and 1 for Easy making the enemies faster or slower
    ///  - the isGameActive boolean is set to true, signalling the other script
    /// the the game is in play
    ///  - the hardDifficulty or mediumDifficulty or easyDifficulty
    ///  (depending on what button is clicked) boolean is set to true which
    ///  other scripts will use as they need to 
    ///  - the enemySpawner game obejct is set active meaning it and the script
    ///  on it can now satrt working
    ///  - the menu gameObject is un set active, meaning the entire menu will
    ///  dissapear from veiw on the game
    /// </summary>
    void SetDifficultyToHard()
    {
        enemySpawnerScript.waveMultiplier = 3;
        enemyPrefabScript.enemySpeed = 3f;
        isGameActive = true;
        hardDifficulty = true;
        enemySpawner.SetActive(true);
        mainMenu.SetActive(false);
        
    }

    void SetDifficultyToMedium()
    {
        enemySpawnerScript.waveMultiplier = 2;
        enemyPrefabScript.enemySpeed = 2f;
        isGameActive = true;
        mediumDifficulty = true;
        enemySpawner.SetActive(true);
        mainMenu.SetActive(false);
    }

    void SetDifficultyToEasy()
    {
        enemySpawnerScript.waveMultiplier = 1;
        enemyPrefabScript.enemySpeed = 1f;
        isGameActive = true;
        easyDifficulty = true;
        enemySpawner.SetActive(true);
        mainMenu.SetActive(false);
    }

    /// <summary>
    /// THis method will Reload the scene by getting the SceneManager and using
    /// the LoadScene method (taking the parameter of what scene to load name
    /// (the current one which is gotten by usnig the GetActiveScene().name method
    /// on the SceneManager))
    /// </summary>
    public void RestartGameth()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // When called it will quit the application. This wont work if you are in
    // editing mode (i.e. using the Unity Editor to play the game), but it would
    // work if that game is actiually being played on a game player (like on a app)
    // This code fro quitting the game is adapted from this video:
    // https://www.youtube.com/watch?v=WRoIw3ktZTE&ab_channel=KapKoder
    public void QuitGame()
    {
        Application.Quit();
    }
}
