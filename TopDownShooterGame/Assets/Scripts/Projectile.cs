using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float bulletSpeed = 40f;
    GameManagerScript gameManagerScript;
    public GameObject gameManager;
    public ParticleSystem explostionParticle;

    // Start is called before the first frame update
    void Start()
    {
        // Because this is a prfab, I cannot enter in objects from the heirachy
        // into the public box, instead:
        // the gameManager in assinged with the GameObject that has a
        // "Game Manager" tag on it
        gameManager = GameObject.Find("Game Manager");
        // the from the variable above, the GameManagerScript script component
        // is extracted and placed inside the gameManagerScript variable for
        // later use
        gameManagerScript = gameManager.GetComponent<GameManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        /// The frame the bullet is spawned/Instantiated, the transform
        /// compoenent of the bullet will be Translated forward on the Vector3
        /// by the bulletSpeed and change in time
        transform.Translate(Vector3.forward * Time.deltaTime * bulletSpeed);

        // For each of the below, either the z or x position of the bullet
        // is derived from the transform component and check if is
        // below/above or equal to a -or+maxZorXRange range derived fromt the
        // gameManagerScript, and if so, the either x or z position of these
        // will be frozen at the -or+maxZorXRange.
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

    /// <summary>
    /// On a collision, the info about the collision is stored in a variable
    /// named "collision" and if colliosn occured with something other than an
    /// object with the tag "bullet or "Player", the current game object this
    /// script is attached to (the bullet) will be destoryed (deleted from the
    /// heirachy)
    /// </summary>
    /// <param name="collision">collison info</param>
    private void OnCollisionEnter(Collision collision)
    {
        // if the collision is not (!) a gameObject with the tag "bullet",
        // and (||) not a gameObject with the tag "Player", then destroy the
        // game object (bullet), else do nothing
        if (!collision.gameObject.CompareTag("bullet") || collision.gameObject.CompareTag("Player"))
        {
            DestroyBullet();
        }
    }

    /// <summary>
    /// When called, this function will Destroy the game object this script is
    /// attached to (the bullet/projectile) and At the same time, this will
    /// spawn the explostionParticle effect at the current postion, and at the
    /// rotation of the explostionParticle
    /// </summary>
    void DestroyBullet()
    {
        Destroy(gameObject);
        Instantiate(explostionParticle, transform.position, explostionParticle.transform.rotation);
    }
}
