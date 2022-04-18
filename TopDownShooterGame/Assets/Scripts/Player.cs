using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float verticalInput;
    public float speed = 10f;
    public float rotateSpeed = 200f;
    private float maxHealth = 10f;
    public float currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        verticalInput = Input.GetAxis("Vertical");

        transform.Translate(Vector3.forward * verticalInput * Time.deltaTime * speed);

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
    }

    void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
    }
}
