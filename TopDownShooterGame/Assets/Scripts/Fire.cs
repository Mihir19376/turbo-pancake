using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    // Will provide a spot in the unity inspector to attach a GameObject (the
    // projectle prefab).
    // And when done, it will assign that object to a prefab
    public GameObject projectilePrefab;

    public GameObject player;
    PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        playerController = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        // If the player presses down the spacebar then:
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // The Game object asigned as the projectile prefab will be
            // Instantiated (spawned) at the position and rotation of the object
            // that this script is attached to

            if (playerController.rapidBullets == true)
            {
                for (int i = 0; i < 10; i++)
                {
                    Instantiate(projectilePrefab, transform.position, transform.rotation);
                }
            }
            else
            {
                Instantiate(projectilePrefab, transform.position, transform.rotation);
            }
        }
    }
}
