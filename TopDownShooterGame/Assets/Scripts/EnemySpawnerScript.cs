using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// Script which randomly spawns enemies at one of a few spawns points
/// </summary>
public class EnemySpawnerScript : MonoBehaviour
{
    // the prefab (enemy) that this cript will spawn 
    public GameObject enemyPrefab;

    // the decimal range around the player in which the enemies will be spawned
    private float spawnRange = 20f;
    // the Count of enmies on teh screen, so display on the UI and to tell the
    // script when the enemies have been all destroyed (when the enemy count is
    // 0)
    public int enemyCount;
    // int of enemies in each wave, defaulty it is set to 3, but will
    // increase as each wave is killed
    public int waveNumber = 3;
    // The amount of waves needed to be defeated to win thr game (5)
    public int winningScore = 5;
    // The current score of the game, updatetd every time a wave in defeated
    public int score;
    // The multiplier that decides the the amount of enemies spawned after each
    // wave (e.g. if the multiplier was 2, two time the enemies would spawn
    // reletive to the last wave)
    public int waveMultiplier;
    // the Y position of the enemies (which will be set later so that the
    // enemies always spawn right at the level of the ground)
    private float enemySpawnPositionY;

    // The gameWonMenu enmpty game obejct where all the gameWonMenu buttons and
    // texts are stored as child objects. Set this to active or not active,
    // will autiomaticlly set everything it carry to active or not too
    public GameObject gameWonMenu;
    // A contines button, to continue back to the main menu (by reloading the
    // scene)
    public Button continueButton;

    // a Text game obejct which is updated with and dispays the score eof the game
    public TextMeshProUGUI scoreText;
    // a Text game obejct which is updated with and dispays the amouunt of
    // enemies left in the game
    public TextMeshProUGUI enemiesLeft;

    // the game maneger game obejct and script (I use the game manager object
    // to extract the script from and store in a in the script vairable)
    GameManagerScript gameManagerScript;
    public GameObject gameManager;

    // The audio source compoent from which the newEnemyWaveSound audio
    // sound/clip will play from
    private AudioSource enemySpawnerAudioSource;
    public AudioClip newEnemyWaveSound;

    // Start is called before the first frame update
    void Start()
    {
        // Makes the Y spawn position for the enies to be half their width so
        // they spawn just above the platform no matter how big they are
        enemySpawnPositionY = enemyPrefab.transform.localScale.y / 2;
        // Will Extract the AudioSource, of the GameObject this script has
        // attached to (the the Enemy Spawner GameObject), and store it for
        // later use in this AudioSource Variable (enemySpawnerAudioSource)
        enemySpawnerAudioSource = GetComponent<AudioSource>();
        // As soon as this enemy spawener game obejct is set active, the score
        // is set to 0
        score = 0;
        // Will Extract the GameManagerScript component from the gameManager
        // gameObject and store it in a variable named gameManagerScript
        gameManagerScript = gameManager.GetComponent<GameManagerScript>();
        // Will call the SpawnEnemyWave method, hence spawn a waveNumber of enemies 
        SpawnEnemyWave(waveNumber);
        // Updates the Score to 0 at the start of the script because no points
        // have been scored yet
        UpdateScore(0);
        // Now that the game has started (ebacsuethis script is only acitve when
        // the game starts), the scoreText and enemiesLeft gameObejcts are set
        // active to be readfy to diplay the score and enemies left
        scoreText.gameObject.SetActive(true);
        enemiesLeft.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the game is is active and carry out code below if not does nothing
        // to check if the game is active, the script will extract the
        // isGameActive boolean from the gameManagerScript (which has the
        // GameManagerScript stored inside) and checks if it equals "true"
        if (gameManagerScript.isGameActive == true)
        {
            // stores the number of obejcts in the Heirachy with the
            // EnemyPrefabScript in the enemyCount variable
            enemyCount = FindObjectsOfType<EnemyPrefabScript>().Length;

            // if the current score is the score needed to win then make the game
            // not active anymore and display the gameWonMenu
            // chekc if the score int is ewual to the winningScore int assinged
            // at the top and if so sets the gameWonMenu game obejct to active
            // and sets the isGameActive (derived from the gameManagerScript
            // variable) to false to signal that the game is over so other
            // script know and carry out the neccesary code for this.
            if (score == winningScore)
            {
                gameWonMenu.SetActive(true);
                gameManagerScript.isGameActive = false;
            }

            // When the enemyCount is equal to 0 (no more enemies on the
            // heirachy), the follwoing code gets carried out
            if (enemyCount == 0)
            {
                // Adds one point to the scroe, because the player has defeated
                // one full wave of enemies
                UpdateScore(1);

                // If the easyDifficulty (derived from the gameManagerScript) is
                // set to true then add one to the waveNumber
                if (gameManagerScript.easyDifficulty == true)
                {
                    waveNumber++;
                }
                // If the mediumDifficulty (derived from the gameManagerScript) is
                // set to true then mutiply the waveNumber by the multiplier
                // (which is lower for medium difficulty)
                else if (gameManagerScript.mediumDifficulty == true)
                {
                    waveNumber *= waveMultiplier;
                }
                // If the hardDifficulty (derived from the gameManagerScript) is
                // set to true then mutiply the waveNumber by the multiplier
                // (which is hgiher for hardDifficulty)
                else if (gameManagerScript.hardDifficulty == true)
                {
                    waveNumber *= waveMultiplier;
                }

                // Spawn a wave of enemies (with the newly modified waveNumber)
                // because the enemies count is 0
                SpawnEnemyWave(waveNumber);
            }

            // Update the number of enemies for the UI
            UpdateEnemiesLeft();
        }
   
    }



    /// <summary>
    /// Creates a random spawnPosX for the x-axis for an enemy to spawn
    /// Creates a random spawnPosZ for the z-axis for an enemy to spawn
    /// Creates a Vector 3 called randomPos with the spawnPosX for x, .5f for y,
    /// and spawnPosZ for z.
    /// And it returns this value
    /// </summary>
    /// <returns>A random spawn Position for a enemy as a Vector 3</returns>
    private Vector3 GenerateSpawnPosition()
    {
        // a float nemed spawnPosX with a interger betwen the x position of the
        // object this script is attched to (EnemySpawner) minus the spawnRange
        // and the x position of the object this script is attched to
        // (EnemySpawner) plus the spawnRange
        float spawnPosX = Random.Range(transform.position.x - spawnRange, transform.position.x + spawnRange);
        // a float nemed spawnPosZ with a interger betwen the z position of the
        // object this script is attched to (EnemySpawner) minus the spawnRange
        // and the z position of the object this script is attched to
        // (EnemySpawner) plus the spawnRange
        float spawnPosZ = Random.Range(transform.position.z - spawnRange, transform.position.z + spawnRange);
        //Creates a Vector 3 called randomPos with the spawnPosX for x, .5f for y,
        // and spawnPosZ for z.
        Vector3 randomPos = new Vector3(spawnPosX, enemySpawnPositionY, spawnPosZ);
        // returns this randomPos as a Vector3 to use
        return randomPos;
    }


    /// <summary>
    /// takes in Numbers of enmies you want spawned in this wave
    /// plays the newEnemyWaveSound
    /// and for a enemiesToSpawn number of times the code will spawn an enemy
    /// (enemyPrefab) at the vecter the GenerateSpawnPosition() gives, and at
    /// the rotation of the enemy prefab
    /// </summary>
    /// <param name="enemiesToSpawn">Numbers of enmies you want spawned in this
    /// wave</param>
    void SpawnEnemyWave(int enemiesToSpawn)
    {
        // form the enemySpawnerAudioSource pay the newEnemyWaveSound at full
        // volume (whcih is what 1 represents) once (which is what PlayOneShot
        // represents)
        enemySpawnerAudioSource.PlayOneShot(newEnemyWaveSound, 1);
        // create a int named i and increase that by 1 every loop, and run the l
        // oop as long as i is less that the enemiesToSpawn int created
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            // Instantiated/spawn the enemyPrefab at the position this
            // GenerateSpawnPosition generates, at the rpation of the
            // enemyPrefab (derived form its transform component)
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }
    }

    /// <summary>
    /// take in a paramerer named scoreToAdd as thea mount of scre to add
    /// take the score variable and add the scoreToAdd int to it (Hence updating
    /// the score UI, as the score UI is connected to this)
    /// get the text component of the scoreText and update is with the current
    /// score. e.g. is the score was 3 it would become "Score: 3".
    /// </summary>
    /// <param name="scoreToAdd">What integer you want to update the score by</param>
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
