using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthBar : MonoBehaviour
{
    // Gets the player controller script and puts it in a variable named playerController
    PlayerController playerController;
    // Will provide a spot in the unity inspector to attach a GameObject. (the
    // player)
    // And when done, it will assign that object to a prefab
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        // makes the playerController variable equal to the Component script
        // named PlayerController (attached to the player game object variable)
        playerController = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        // if the variable named currentHealth inside the script
        // playerController is equal to or less that 0 then:
        if (playerController.currentHealth <= 0)
        {
            // Destroy whatever object this script is attached to
            // (i.e. the health bar object)
            Destroy(gameObject);
        }

        // every frame, get the player current Health and max health and dived
        // them to get a float which is asisgned as the y size of the object
        // this script is attached to (i.e. the health bar)
        transform.localScale = new Vector3(.5f, playerController.currentHealth / playerController.maxHealth, .5f);

    }
}
