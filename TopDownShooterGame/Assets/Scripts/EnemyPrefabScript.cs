using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPrefabScript : MonoBehaviour
{
    public float enemySpeed;
    private float maxHealth = 3f;
    public float currentHealth;

    private Rigidbody enemyRb;
    private GameObject player;

    GameManagerScript gameManagerScript;
    public GameObject gameManager;

    public ParticleSystem enemyKilledParticleEffect;

    public Renderer enemyRenderer;
    public Color enemyRegularcolour;
    public Color enemyDamagedColor;

    private AudioSource enemyAudioSource;
    public AudioClip enemyDamageSound;
    public AudioClip enemyKilledSound;

    // Start is called before the first frame update
    void Start()
    {
        // Will Extract the AudioSource, of the GameObject this script has
        // attached to (the the enemyPrefab), and store it for later use in this
        // AudioSource Variable
        enemyAudioSource = GetComponent<AudioSource>();
        // Will Find the gameObject in the Heicarchy whcih has the tag
        // "Game Manager" and store this game object in a variable named
        // gameManager fo later use
        gameManager = GameObject.Find("Game Manager");
        // Will extract the GameManagerScript component from teh gameManager
        // variable assinged jusat above and store the script in a variable
        // named gameManagerScript
        gameManagerScript = gameManager.GetComponent<GameManagerScript>();

        //Finds the rigidbody component of the gameObject this Script is
        //attached to (the enemy prefab) and stores it in a variable named enemyRb
        enemyRb = GetComponent<Rigidbody>();
        // Will Find the gameObject in the Heicarchy whcih has the tag
        // "Player" and store this game object in a variable named
        // player fo later use
        player = GameObject.Find("Player");
        // Sets the current health variable to the max health the player can
        // have as soon as the object this script (the enemy prefab)
        // is attached to starts existingin the heirachy 
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        // If the game is active, follow the player, and when all health is
        // lost, Kill the enemy
        // checks if the isGameActive boolean is set to true, and if so carry
        // out the follwoing code, if not then do nothing
        if (gameManagerScript.isGameActive == true)
        {
            // Follows the player, by creating a Vector3 vairable named
            // lookDirection, which includes the Vector3 players' positin minus
            // the Vector3 position of the EnemyPrefab (the object this script
            // is attached to). And normalises this value
            Vector3 lookDirection = (player.transform.position - transform.position).normalized;
            //adds a force to the enemies rigidbody in the direction of the
            //lookdirection Vector3 multiplied by the speed (move the enemy
            //forward at its enemySpeed)
            enemyRb.AddForce(lookDirection * enemySpeed);

            // if the current int variable stores a value less than 0 then it
            // starts the Killing enemy coroutine 
            if (currentHealth <= 0)
            {
                // Will excute the KillEnemy method
                StartCoroutine(KillEnemy());
            }
        }
        
    }


    /// <summary>
    /// This method is called when ever this game objects box collider collides
    /// with another box collider and It will store all the info about the
    /// collision in a variable named "collision". using the info in collison,
    /// it will check if collied with a Wall (and ignore the walls box collider
    /// to pass straigh through) or a bullet (and destroy the bullet and take
    /// some damage)
    /// </summary>
    /// <param name="collision">will take the all the info of the Collision</param>
    private void OnCollisionEnter(Collision collision)
    {
        // if the gameObject this collided with has the tag "Walls" then it will
        // ignore the cillider of the Walls and go through it
        if (collision.gameObject.CompareTag("Walls"))
        {
            //Ignores collison by using the built in method that takes in the
            //parameter which are the colliders of object I want the game objct
            //to ignore. Inthis case it takes in the Walls collider, and the
            //enemyPrefabs collider.
            Physics.IgnoreCollision(collision.gameObject.GetComponent<Collider>(), GetComponent<Collider>());
        }

        // If the game obejct it collided with has the tag "bullet" then it
        // will destroy the bullet and take damage
        else if (collision.gameObject.CompareTag("bullet"))
        {
            // Destroy the gameObject it has collided with
            Destroy(collision.gameObject);
            // Executre the Take Damage method(IENumerator)
            StartCoroutine(TakeDamage(1));
        }

    }

    /// <summary>
    /// Everytime the object this script is attached to (the enemy prefab)
    /// enters a trigger, it will store all the info about the other collider
    /// in a variable named "other"
    /// if the other variable has the tag "Spikes" then it will Take Damage from the player
    /// </summary>
    /// <param name="other">the Collider</param>
    private void OnTriggerEnter(Collider other)
    {
        // Compares the tag in the other variable for "Spikes" and if it does
        // have the tag, then it starts the Coroutine of taking damage (to check
        // what that does, check that methods' comments)
        if (other.CompareTag("Spikes"))
        {
            StartCoroutine(TakeDamage(1));
        }
    }

    /// <summary>
    /// This method will Take damage and play all of the damage effects
    /// First it plays the damage sound, then if subtracts health, then it turns
    /// the colour into the damaged colour, waits for a second, and then
    /// reverts the coulour back to the regular colour
    /// </summary>
    /// <param name="damageAmount"></param>
    /// <returns>a timer that waits for 1 second and carroys out the rest of
    /// the code after it</returns>
    IEnumerator TakeDamage(int damageAmount)
    {
        // from the enemyAudioSource it will play the enemy damage sound once at
        // its set full volume
        enemyAudioSource.PlayOneShot(enemyDamageSound, 1);
        // Deducts a damage amount int from the current Health of the enemy
        currentHealth -= damageAmount;
        // Acceses the colour of the material in the enemies reneder and turns
        // that colour its damages colour 
        enemyRenderer.material.color = enemyDamagedColor;
        //Returns the wait value of 1 second
        yield return new WaitForSeconds(1);
        // And then Acceses the colour of the material in the enemies reneder
        // and turns that colour back to its regular colour 
        enemyRenderer.material.color = enemyRegularcolour;
    }


    /// <summary>
    /// This is the enemies death method, it is called when the enenmy loses all
    /// its health. WHat it does is playes the enmies deatrh sound, and awits
    /// .2 seconds for it finish and then spawns the enemy death particle effect
    /// and destroys the object this script is attached to (the enemy prefab)
    /// </summary>
    /// <returns>a timer that waits for .2 second and carroys out the rest of
    /// the code after it</returns>
    IEnumerator KillEnemy()
    {
        // from the enemyAudioSource it will play the enemy killed sound once at
        // its set full volume
        enemyAudioSource.PlayOneShot(enemyKilledSound, 1);
        //Returns the wait value of .2 seconds
        yield return new WaitForSeconds(.2f);
        //Spawns (Instantiates) the enemyKilledParticleEffect at the position of
        //this gameObject (the enemy) and at the rotation of the
        //nemyKilledParticleEffect
        Instantiate(enemyKilledParticleEffect, transform.position, enemyKilledParticleEffect.transform.rotation);
        // Destroys (meanins removes from the heirachy) this gameObject
        // (the enemy)
        Destroy(gameObject);
    }

}