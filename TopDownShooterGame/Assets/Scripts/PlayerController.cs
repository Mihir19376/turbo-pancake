using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float verticalInput;
    public float rotateSpeed = 400f;
    public float maxHealth = 10f;
    public float currentHealth;

    public float powerUpSpeed = 20f;
    public float noPowerUpSpeed = 10f;
    public float playerSpeed;

    public float powerUpDuration = 5;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody>();
        playerSpeed = noPowerUpSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.localScale.y / 2, transform.position.z);
        verticalInput = Input.GetAxis("Vertical");

        transform.Translate(Vector3.forward * verticalInput * Time.deltaTime * playerSpeed);

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(-Vector3.up * rotateSpeed * Time.deltaTime);
        }
            
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
        }

        //if (currentHealth <= 0)
        //{
        //    Destroy(gameObject);
        //}
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
            StartCoroutine(ActivateSpeedPowerUp());
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

    IEnumerator ActivateSpeedPowerUp()
    {
        playerSpeed = powerUpSpeed;
        yield return new WaitForSeconds(powerUpDuration);
        playerSpeed = noPowerUpSpeed;
    }
}
