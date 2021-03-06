using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Code to control the size of the players health bar
/// </summary>
public class PlayerHealthBar : MonoBehaviour
{
    // Gets the player controller script and puts it in a variable named playerController
    PlayerController playerController;
    // player used to get the health of it to determine the size of the healthbar
    public GameObject player;

    // the max size of the health bar as a float so I can change its size
    // relative to it oroginal size, to show loss in health later on
    private float healthbarMaxSize;
    // will be usnig the player health variables to derive it self
    private float healthPercentage;

    // Start is called before the first frame update
    void Start()
    {
        playerController = player.GetComponent<PlayerController>();

        // Because the starting size of the healthBar game oBject is its full
        // size, the health bar sizes are derived of the y omponent
        // of the local scale component of the transform component derived from
        // the healthbar(the game obejct this script is attached to)
        healthbarMaxSize = transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        // if the variable named currentHealth inside the script
        // playerController is equal to or less that 0 then carry out the
        // following code, if not then do nothing
        if (playerController.currentHealth <= 0)
        {
            // Destroy whatever object this script is attached to
            // (i.e. the health bar object)
            Destroy(gameObject);
        }

        // Derives a percentage based on the currenthealth (derived from the
        // playerController script) divided by the maxHealth (derived from the
        // playerController script)
        healthPercentage = playerController.currentHealth / playerController.maxHealth;
        // takes the localScale from the transform component this script is
        // attached to (the healthbar) and creates a new vector 3 to replace the
        // scale with the default healthbarXSize, default healthbarZSize, but
        // with the healthPercentage multiplied by the healthbarMaxSize from
        // above, hence making the y size of the health bar proportionate to the
        // health the player has left
        transform.localScale = new Vector3(transform.localScale.x, healthPercentage*healthbarMaxSize, transform.localScale.z);

    }
}
