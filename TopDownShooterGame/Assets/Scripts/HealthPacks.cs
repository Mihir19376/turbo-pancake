using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPacks : MonoBehaviour
{

    // // a public GameObejct list where you can add in multiple game objects and
    // they all get stored in a list called healthPackPositions.
    // This intened for a bunch of empty game object placed along the arena
    public GameObject[] healthPackPositions;

    // Start is called before the first frame update
    void Start()
    {
        // Quite Self Explanitory
        SpawnHealthBoxAtRandomPoint();
    }

    /// <summary>
    /// On a collison, this method will take the info of the collison and store
    /// it in a variable named "collision". If the collison was a game object
    /// with the Tag "Player" the it would call the SpawnHealthBoxAtRandomPoint
    /// method and so spawn the health pack at a random position (which could be
    /// the same position)
    /// </summary>
    /// <param name="collision">icollision info</param>
    private void OnCollisionEnter(Collision collision)
    {
        // checks if the tage of the gameObject of the collison if "Player" and
        // so carrys out the following code, if not, does nothing
        if (collision.gameObject.CompareTag("Player"))
        {
            // Again, Self Explanitory
            SpawnHealthBoxAtRandomPoint();
        }
    }

    /// <summary>
    /// This method will create a int Varible named pointIndex and assined it a value
    /// This value is a random number between 0 and the amount of gameObjects in
    /// the healthPackPositions gameObject list.
    /// Next the method will take the position of this gameObject,(the one the
    /// script is attched to, the healthPack) (position dervied from the
    /// transform componenet on the HealthPack), and turn it into a new Vector3
    /// with the x position of one of the healthPackPositions gameObjects, the y
    /// position of 0, and the z position of one of the healthPackPositions
    /// gameObjects
    /// </summary>
    void SpawnHealthBoxAtRandomPoint()
    {

        int pointIndex = Random.Range(0, healthPackPositions.Length); // the .Length will count how many gameObjects are in that list, and will turn it into a int
        // helathPackPositions[pointIndex] will choose the pointIndex^st or th (1st, 2nd, 3rd, ...)
        // from the list of gameObjects and transform will get .transform.position.z-or-x
        // which takes the x or x position from the positon from the transform
        // component of the gameObject
        transform.position = new Vector3(healthPackPositions[pointIndex].transform.position.x, 0, healthPackPositions[pointIndex].transform.position.z);
    }
}
