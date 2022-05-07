using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPrefabScript : MonoBehaviour
{
    public float speed;
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

        if (gameManagerScript.isGameActive == true)
        {
            Vector3 lookDirection = (player.transform.position - transform.position).normalized;

            enemyRb.AddForce(lookDirection * speed);

            if (currentHealth <= 0)
            {
                StartCoroutine(KillEnemy());
            }
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Walls"))
        {
            Physics.IgnoreCollision(collision.gameObject.GetComponent<Collider>(), GetComponent<Collider>());
        }

        else if (collision.gameObject.CompareTag("bullet"))
        {
            Destroy(collision.gameObject);
            StartCoroutine(TakeDamage(1));
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Spikes"))
        {
            StartCoroutine(TakeDamage(1));
        }
    }

    IEnumerator TakeDamage(int damageAmount)
    {
        enemyAudioSource.PlayOneShot(enemyDamageSound, 1);
        currentHealth -= damageAmount;
        enemyRenderer.material.color = enemyDamagedColor;
        yield return new WaitForSeconds(1);
        enemyRenderer.material.color = enemyRegularcolour;
    }

    IEnumerator KillEnemy()
    {
        enemyAudioSource.PlayOneShot(enemyKilledSound, 1);
        yield return new WaitForSeconds(.2f);
        Instantiate(enemyKilledParticleEffect, transform.position, enemyKilledParticleEffect.transform.rotation);
        Destroy(gameObject);
    }

}