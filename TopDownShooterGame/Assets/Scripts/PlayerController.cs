using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float verticalInput;
    public float horizontalInput;

    private float rotateSpeed = 150f;
    public float maxHealth = 10f;
    public float currentHealth;

    public bool rapidBullets = false;

    public float powerUpSpeed = 20f;
    public float noPowerUpSpeed = 10f;
    public float playerSpeed;

    public float powerUpDuration = 5;

    public GameObject speedPowerUpIndicator;
    public GameObject bulletPowerUpIndicator;

    private Rigidbody rb;

    GameManagerScript gameManagerScript;
    public GameObject gameManager;

    public ParticleSystem hitByEnemyEffect;
    public Renderer playerRenderer;

    public Color playerDamageColour;
    public Color playerRegularColour;

    private AudioSource playerAudioSource;
    public AudioClip collectPowerupSound;
    public AudioClip playerDamageSound;
    public AudioClip playerKilledSound;


    // Start is called before the first frame update
    void Start()
    {
        playerAudioSource = GetComponent<AudioSource>();
        gameManagerScript = gameManager.GetComponent<GameManagerScript>();

        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody>();
        playerSpeed = noPowerUpSpeed;

        playerRenderer.material.color = playerRegularColour;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManagerScript.isGameActive == true)
        {
            transform.position = new Vector3(transform.position.x, transform.localScale.y / 2, transform.position.z);

            horizontalInput = Input.GetAxisRaw("Horizontal");
            verticalInput = Input.GetAxis("Vertical");

            transform.Translate(Vector3.forward * verticalInput * Time.deltaTime * playerSpeed);

            if (horizontalInput < 0f)
            {
                transform.Rotate(-Vector3.up * rotateSpeed * Time.deltaTime);
            }

            else if (horizontalInput > 0f)
            {
                transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
            }

            if (currentHealth <= 0)
            {
                playerAudioSource.PlayOneShot(playerKilledSound, 1);
                gameManagerScript.isGameActive = false;
                gameManagerScript.hasGameBeenPlayed = true;
            }

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


    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Health Pack"))
        {
            playerAudioSource.PlayOneShot(collectPowerupSound, 1);
            RefillHealth();
        }

        if (collision.gameObject.CompareTag("Speed Pack"))
        {
            playerAudioSource.PlayOneShot(collectPowerupSound, 1);
            speedPowerUpIndicator.gameObject.SetActive(true);
            StartCoroutine(ActivateSpeedPowerUp());
        }

        if (collision.gameObject.CompareTag("Bullet Pack"))
        {
            playerAudioSource.PlayOneShot(collectPowerupSound, 1);
            bulletPowerUpIndicator.gameObject.SetActive(true);
            StartCoroutine(ActivateBulletPowerUp());
        }

        if (collision.gameObject.CompareTag("Walls"))
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            StartCoroutine(TakeDamage(1));
        }

        if (playerSpeed == powerUpSpeed)
        {
            if (collision.gameObject.CompareTag("Walls"))
            {
                Physics.IgnoreCollision(collision.gameObject.GetComponent<Collider>(), GetComponent<Collider>());
            }
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Spikes"))
        {
            StartCoroutine(TakeDamage(1));
        }
    }


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

    void RefillHealth()
    {
        currentHealth = maxHealth;
    }

    IEnumerator ActivateBulletPowerUp()
    {
        rapidBullets = true;
        yield return new WaitForSeconds(powerUpDuration);
        bulletPowerUpIndicator.gameObject.SetActive(false);
        rapidBullets = false;
    }

    IEnumerator ActivateSpeedPowerUp()
    {
        playerSpeed = powerUpSpeed;
        yield return new WaitForSeconds(powerUpDuration);
        speedPowerUpIndicator.gameObject.SetActive(false);
        playerSpeed = noPowerUpSpeed;
    }
}
