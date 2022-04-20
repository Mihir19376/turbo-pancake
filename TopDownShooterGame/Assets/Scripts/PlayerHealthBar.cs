using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthBar : MonoBehaviour
{
    // Gets the player controller script and puts it in a variable named playerController
    PlayerController playerController;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        playerController = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
