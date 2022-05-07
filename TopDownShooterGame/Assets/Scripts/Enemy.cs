using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
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


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager");
        gameManagerScript = gameManager.GetComponent<GameManagerScript>();

        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");

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
                Instantiate(enemyKilledParticleEffect, transform.position, enemyKilledParticleEffect.transform.rotation);
                Destroy(gameObject);
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
        currentHealth -= damageAmount;
        enemyRenderer.material.color = enemyDamagedColor;
        yield return new WaitForSeconds(.2f);
        enemyRenderer.material.color = enemyRegularcolour;
    }

}