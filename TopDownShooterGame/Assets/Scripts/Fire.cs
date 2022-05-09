using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The Fire Scipt attched to the fire point, where the shooting mechanism works
/// </summary>
public class Fire : MonoBehaviour
{
    // Will provide a spot in the unity inspector to attach a GameObject (the
    // projectle prefab, player and game manager).
    public GameObject projectilePrefab;
    public GameObject player;
    public GameObject gameManager;

    // Creates scripts vairable 
    PlayerController playerControllerScript;
    GameManagerScript gameManagerScript;

    // a public caufdio clip named shootSound where the audio clip is inserted
    // in the script component in the inspector
    public AudioClip shootSound;
    // The firePointAudio varible which stores the AudioSource
    private AudioSource firePointAudio;

    // Start is called before the first frame update
    void Start()
    {
        // Will Extract the AudioSource, of the GameObject this script has
        // attached to (the FirePoint), and store it for leater use in this
        // AudioSource Variable
        firePointAudio = GetComponent<AudioSource>();
        // Will extract the GameManagerScript from the gameManger GameObejct
        // and store it in a variable named gameManagerScript
        gameManagerScript = gameManager.GetComponent<GameManagerScript>();
        // Will extract the PlayerController Script from the player GameObejct
        // and store it in a variable named playerControllerScript
        playerControllerScript = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Checks If the fire Key is pressed
        // To do this, it Finds the Input, checks if that input is a KeyPress,
        // and then check if the Key pressed is the Fire1 key and if so,
        // carrys out the code below, if not then does nothing
        if (Input.GetButtonDown("Fire1"))
        {
            // Checks if the isGameActive Boolean in the gameManagerScript is
            // set to true and if so carrys out the follwoing code below, if not
            // then does nothing
            if (gameManagerScript.isGameActive == true)
            {
                // Will play the shootsound Clip clip from the firePointAudio
                // Source once at its set full volume
                firePointAudio.PlayOneShot(shootSound, 1);

                /* Check if the player rapidBullets boolean in the 
                 * playerContorllerScript is set to true (which means the 
                 * rapidBullets powerup is on), if so then if will carry out the
                 * code first below, if not then it will carry out the other 
                 * code below
                 */
                if (playerControllerScript.rapidBullets == true)
                {
                    /* with the bullet powerup set to true, the player will be
                     * able to shoot 10 bullets at once
                     * At the start of the loop, the interger i is set to 0 and
                     * evertime the loop executes, the value i is increased by
                     * one, and the loop will run as long as the value i is
                     * below a certain number (which is the number of bullet
                     * wanted to be fired).
                     * Everytime the loop runs, one projectile prefab will be 
                     * spawned (Instantiated) at the position of the obejct this
                     * Script is attached to (the Fire Point) and at the 
                     * rotation of the player game object (to get this cool 
                     * effect thats not really descriable, just trust that it 
                     * looks cool)
                     */
                    for (int i = 0; i <= 10; i++)
                    {
                        Instantiate(projectilePrefab, transform.position, player.transform.rotation);
                    }
                }
                else
                {
                    /* if the rapidBullet powerup is off the just one projectile 
                     * prefab will be spawned (Instantiated) at the position of 
                     * the obejct this Script is attached to (the Fire Point) 
                     * and at the rotation of the player game object (to get this 
                     * cool effect thats not really describible, just trust that
                     * it looks cool)
                     */
                    Instantiate(projectilePrefab, transform.position, transform.rotation);
                }
            }
            
        }
    }
}
