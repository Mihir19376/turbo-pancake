using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 40f;
    GameManagerScript gameManagerScript;
    public GameObject gameManager;

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
            Destroy(gameObject);
        }

        else if (transform.position.z <= -gameManagerScript.maxZRange)
        {
            Destroy(gameObject);
        }

        else if (transform.position.x >= gameManagerScript.maxXRange)
        {
            Destroy(gameObject);
        }

        else if (transform.position.x <= -gameManagerScript.maxXRange)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("bullet") || collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

}
