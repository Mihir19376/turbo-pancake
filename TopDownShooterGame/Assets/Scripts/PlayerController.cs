using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float verticalInput;
    public float horizontalInput;

    public float rotateSpeed = 400f;
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

    // Start is called before the first frame update
    void Start()
    {
        gameManagerScript = gameManager.GetComponent<GameManagerScript>();

        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody>();
        playerSpeed = noPowerUpSpeed;
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

            //if (currentHealth <= 0)
            //{
            //    Destroy(gameObject);
            //}
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(1);
        }

        if (collision.gameObject.CompareTag("Spikes"))
        {
            TakeDamage(1);
        }

        if (collision.gameObject.CompareTag("Health Pack"))
        {
            RefillHealth();
        }

        if (collision.gameObject.CompareTag("Speed Pack"))
        {
            speedPowerUpIndicator.gameObject.SetActive(true);
            StartCoroutine(ActivateSpeedPowerUp());
        }

        if (collision.gameObject.CompareTag("Bullet Pack"))
        {
            bulletPowerUpIndicator.gameObject.SetActive(true);
            StartCoroutine(ActivateBulletPowerUp());
        }

        if (collision.gameObject.CompareTag("Walls"))
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

    void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
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
