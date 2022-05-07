using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 40f;
    GameManagerScript gameManagerScript;
    public GameObject gameManager;
    public ParticleSystem explostionParticle;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager");
        gameManagerScript = gameManager.GetComponent<GameManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);

        if (transform.position.z >= gameManagerScript.maxZRange)
        {
            DestroyBullet();
        }

        else if (transform.position.z <= -gameManagerScript.maxZRange)
        {
            DestroyBullet();
        }

        else if (transform.position.x >= gameManagerScript.maxXRange)
        {
            DestroyBullet();
        }

        else if (transform.position.x <= -gameManagerScript.maxXRange)
        {
            DestroyBullet();
        }

        else if (transform.position.y >= 10)
        {
            DestroyBullet();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("bullet") || collision.gameObject.CompareTag("Player"))
        {
            DestroyBullet();
        }
    }

    void DestroyBullet()
    {
        Destroy(gameObject);
        Instantiate(explostionParticle, transform.position, explostionParticle.transform.rotation);
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (!other.gameObject.CompareTag("bullet") || other.gameObject.CompareTag("Player"))
    //    {
    //        Destroy(gameObject);
    //    }
    //}
}
