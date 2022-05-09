using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script to control the player, its movement, powerup, and health
/// </summary>
public class PlayerController : MonoBehaviour
{
    // this bool sigmals whether the bullet powerup is on or not
    public bool rapidBullets = false;

    // w,s/up,down keys
    public float verticalInput;
    // a,d/left,rightkeys
    public float horizontalInput;
    // max health the player has
    public float maxHealth = 10f;
    // the current health the update every frame
    public float currentHealth;

    // speed at whcih the player rotates left or right
    private float rotateSpeed;
    // speed of player when they have the speed powerup
    public float powerUpSpeed;
    // speed the player has when they dont have the speed powerup
    public float noPowerUpSpeed = 10f;
    // speed that changes based on if they do or dont have the powerup
    public float playerSpeed;
    // How long each powerup lasts
    public float powerUpDuration = 5;

    // the powerup indicator game obejcts whcih switch on when you have specific
    // powerups and siwtch of when you dont
    public GameObject speedPowerUpIndicator;
    public GameObject bulletPowerUpIndicator;

    // the game maneger game obejct and script (I use the game manager object
    // to extract the script from and store in a in the script vairable)
    GameManagerScript gameManagerScript;
    public GameObject gameManager;

    // A particle effect
    public ParticleSystem hitByEnemyEffect;

    public Renderer playerRenderer;
    // Curse the American spelling of "colour", its so unatural for me to type
    public Color playerDamageColour;
    public Color playerRegularColour;

    // Audio source compoenent from the insprector
    private AudioSource playerAudioSource;
    // a bunch of sounds that are played from the audio source above
    public AudioClip collectPowerupSound;
    public AudioClip playerDamageSound;
    public AudioClip playerKilledSound;


    // Start is called before the first frame update
    // All that happens below occurs as soon as the Player is in the Heirachy
    // when the game starts
    void Start()
    {
        // Sets the powerUpSpeed int to be 2 times that of the noPowerUpSpeed int
        powerUpSpeed = noPowerUpSpeed * 2;
        // Sets the rotateSpeed int to be 15 times that of the noPowerUpSpeed int
        rotateSpeed = noPowerUpSpeed * 15;
        // Sets the current health int of the player to be the maxHealth int 
        currentHealth = maxHealth;
        // Will Extract the AudioSource, of the GameObject this script has
        // attached to (the the player), and store it for later use in this
        // AudioSource Variable
        playerAudioSource = GetComponent<AudioSource>();
        // Will Extract the GameManagerScript component from the gameManager
        // gameObject and store it in a variable named gameManagerScript
        gameManagerScript = gameManager.GetComponent<GameManagerScript>();
        // Sets the players' speed int to be the noPowerUpSpeed int at the
        // beggining of the game
        playerSpeed = noPowerUpSpeed;
        // Gets the playerRenderer materials colour and sets it to be the
        // playerRegularColour Color variable
        playerRenderer.material.color = playerRegularColour;
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
            // Will extract the position of the player from its trasnform
            // component, and will turn it into a new vector 3 with the
            // follwoing info:
            // its current position on the x axis (hence not changing it)
            // its current position on the z axis (hence not changing it)
            // its Y position will be equal to the width of the player divided
            // by 2 so tht the player stays at the same y position the entire
            // game, to make sure it doesnt not fall through the ground or fly upwards
            transform.position = new Vector3(transform.position.x, transform.localScale.y / 2, transform.position.z);

            // Rotates the player and moves it worward in the direction its facing
            // Get the Horizontal Input (right/left arrow keys, and a or d keys)
            // as a values either -1(left) or 1(right) and stores this value in
            // the float variable "horizontalInput"
            horizontalInput = Input.GetAxisRaw("Horizontal");
            // Get the Vetical Input (up/down arrow keys, and w or s keys)
            // as a values between -1(down) or 1(up) and stores this value in
            // the float variable "verticalInput"
            verticalInput = Input.GetAxis("Vertical");
            // Takes the transform component of the player and Translates it
            // forward on the vector 3 axis multiplied by the verticle input
            // (so if it was negative, the forward would mean back) multiplied
            // by changine in time so no weird movement happens inbetween the
            // frames multiplied the player speed int so it goes at certain speed
            transform.Translate(Vector3.forward * verticalInput * Time.deltaTime * playerSpeed);
            // Takes the transform variable and rotates it up on the Vector3
            // (dont ask why its up even though it would make sense if it was
            // right or left) and the rest is the same as above, expect for using
            // a different speed variable named rotate speed
            transform.Rotate(Vector3.up * horizontalInput * rotateSpeed * Time.deltaTime);

            // if the currentHealth int is less than or equal to 0 then the
            // player gets killed. To siganl this, a playerKilledSound is played
            // once (PlayOneShot) at full volume from the Audio Source (playerAudioSource)
            // Next the isGameActive boolean is taken from the gameManagerScript
            // and is set to false (and other script will now use this to switch
            // off certain gameplay fucntions), and hasGameBeenPlayed boolean is
            // set to true in the same way
            if (currentHealth <= 0)
            {
                playerAudioSource.PlayOneShot(playerKilledSound, 1);
                gameManagerScript.isGameActive = false;
                gameManagerScript.hasGameBeenPlayed = true;
            }

            // For each of the below, either the z or x position of the player
            // is derived from the transform component and chekc if is
            // below/above or equal to a -or+maxZorXRange range derived fromt the
            // gameManagerScript, and if so, the either x or z position of these
            // will be frozen at the -or+maxZorXRange.
            if (transform.position.z >= gameManagerScript.maxZRange)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, gameManagerScript.maxZRange);
            }
            else if (transform.position.z <= -gameManagerScript.maxZRange)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, -gameManagerScript.maxZRange);
            }
            else if (transform.position.x >= gameManagerScript.maxXRange)
            {
                transform.position = new Vector3(gameManagerScript.maxXRange, transform.position.y, transform.position.z);
            }
            else if (transform.position.x <= -gameManagerScript.maxXRange)
            {
                transform.position = new Vector3(-gameManagerScript.maxXRange, transform.position.y, transform.position.z);
            }
        }

    }

    /// <summary>
    /// On a collsion the info about the collion and collider is stored in the
    /// "collision" variable. if this collision was to a game obejct with the tag
    /// "Health pack", it would refill the health. if it was to a tag with
    /// "Speed Pack" it would give the player a speed powerup. if it was to a
    /// tag with "Bullet Pack" it would give the player the bullet powerup. if
    /// the collison was to gameObject with the Tag "Walls", if the playerSpeed
    /// was eqaual to the powerUpSpeed then ignore the ocllisions between the
    /// two colliders, hence letting the player pass straight through, if no do
    /// nothing. If the collison was to gameObject with the Tag "Enemy" then take damage
    /// </summary>
    /// <param name="collision">Info about the Collision</param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Health Pack"))
        {
            // From the playerAudioSource player the collectPowerupSound at full
            // volume once (which is what PlayOneShot means)
            playerAudioSource.PlayOneShot(collectPowerupSound, 1); 
            // Execute the Refill health method
            RefillHealth();
        }
        if (collision.gameObject.CompareTag("Speed Pack"))
        {
            // From the playerAudioSource player the collectPowerupSound at full
            // volume once (which is what PlayOneShot means)
            // I cannot turn this into a method, because it would break the
            // Coroutines below
            playerAudioSource.PlayOneShot(collectPowerupSound, 1);
            // Start the ActivateSpeedPowerUp Coroutine (meaning excute the
            // ActivateSpeedPowerUp method/IEnumerator) 
            StartCoroutine(ActivateSpeedPowerUp());
        }
        if (collision.gameObject.CompareTag("Bullet Pack"))
        {
            // From the playerAudioSource player the collectPowerupSound at full
            // volume once (which is what PlayOneShot means)
            playerAudioSource.PlayOneShot(collectPowerupSound, 1);
            // Start the ActivateBulletPowerUp Coroutine (meaning excute the
            // ActivateBulletPowerUp method/IEnumerator) 
            StartCoroutine(ActivateBulletPowerUp());
        }
        if (collision.gameObject.CompareTag("Walls"))
        {
            // if the playerSpeed int is equal to the powerUpSpeed (meaning
            // the player currently has the speed powerup) then carry out the
            // following code, if no then do nothing
            if (playerSpeed == powerUpSpeed)
            {
                //Ignores collison by using the built in method that takes in the
                //parameter which are the colliders of object I want the gameobject
                //to ignore. In this case it takes in the Walls collider, and the
                //player (the obejct this script is attachetd to) collider.
                Physics.IgnoreCollision(collision.gameObject.GetComponent<Collider>(), GetComponent<Collider>());
            }
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Starts the caroutine, exceutes the TakeDamage method/IEneumerator
            StartCoroutine(TakeDamage(1));
        }

    }

    /// <summary>
    /// Evertime this gameObject (the player) enters trigger, its stores that
    /// about the other Collider into a variable named "other"
    /// and if this trigger heppens to be a Spike, it Takes damage from the
    /// player
    /// </summary>
    /// <param name="other">info About the Collider</param>
    private void OnTriggerEnter(Collider other)
    {
        // if the Colliders info (stored in the other variable) contains the tag
        // "Spikes" then starts the Coroutine of the takedmage method
        if (other.gameObject.CompareTag("Spikes"))
        {
            StartCoroutine(TakeDamage(1));
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="damageAmount">the amount of damage you want to do to the
    /// player</param>
    /// <returns></returns>
    IEnumerator TakeDamage(int damageAmount)
    {
        playerAudioSource.PlayOneShot(playerDamageSound, 1);
        hitByEnemyEffect.Play();
        currentHealth -= damageAmount;
        playerRenderer.material.color = playerDamageColour;
        yield return new WaitForSeconds(.5f);
        playerRenderer.material.color = playerRegularColour;
        Debug.Log("Hit");
    }

    /// <summary>
    /// Just takes the current health of the object and resets it to the maxHealth
    /// </summary>
    void RefillHealth()
    {
        // The currentHealth int contains the into value of the maxHealth int
        currentHealth = maxHealth;
    }

    /// <summary>
    /// Activates the bullet powerup (The names' kind of self-explanitory)
    /// It sets the bulletPowerUpIndicator game obvejct to true, whcih makes it
    /// apper on the screen. And then sets the rapidBullets to true, so that
    /// other function in this and other code can use it. Then it returns a
    /// timer that waits for the powerupduration, and then after the timer ends,
    /// the bulletPowerUpIndicator is set back to false, removing from the
    /// screen and then turns the rapidBullets boolean to false
    /// </summary>
    /// <returns>a timer that waits for the powerupDuration and carroys out the
    /// rest of the code after it</returns>
    IEnumerator ActivateBulletPowerUp()
    {
        bulletPowerUpIndicator.gameObject.SetActive(true);
        rapidBullets = true;
        yield return new WaitForSeconds(powerUpDuration);
        bulletPowerUpIndicator.gameObject.SetActive(false);
        rapidBullets = false;
    }

    /// <summary>
    /// Activates the speed powerup (The names' kind of self-explanitory)
    /// It sets the speedPowerUpIndicator game obvejct to true, which makes it
    /// apper on the screen. And then sets the player speed to be equal to the
    /// powerUpSpeed int. Then it returns a timer that waits for the
    /// powerupduration, and then after the timer ends, the speedPowerUpIndicator
    /// is set back to false, removing from the screen and then turns the player
    /// speed eqaul to the value inside the noPowerUpSpeed int hence returning
    /// the playerspeed to its normal speed so the player move around on the
    /// screen at its normal pace the rest of the code after it
    /// </summary>
    /// <returns>a timer that waits for the powerupDuration and carroys out
    /// </returns>
    IEnumerator ActivateSpeedPowerUp()
    {
        speedPowerUpIndicator.gameObject.SetActive(true);
        playerSpeed = powerUpSpeed;
        yield return new WaitForSeconds(powerUpDuration);
        speedPowerUpIndicator.gameObject.SetActive(false);
        playerSpeed = noPowerUpSpeed;
    }
}
