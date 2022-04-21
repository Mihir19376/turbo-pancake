using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPacks : MonoBehaviour
{
    // Start is called before the first frame update
    // Crates a list in which game object can be stored (in this case it is
    // inteneded for a set of empty game object placed arosund at specific spots
    // on the game, I want the health box to spawn at random at one of these spots)
    public GameObject[] points;

    void Start()
    {
        // Quite Self Explanitory
        SpawnHealthBoxAtRandomPoint();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Check for collisiosns, and everytime it does, then it will store all the
    // info about the collision in the variable "collision"
    private void OnCollisionEnter(Collision collision)
    {
        // checks against the "ollision" if the Tag of that collided gameObject
        // is "Player" and if so:
        if (collision.gameObject.CompareTag("Player"))
        {
            // Again, Self Explanitory
            SpawnHealthBoxAtRandomPoint();
        }
    }

    // a fucntion that will spawn a Health Box at a one of the random positions
    // inseted into the points variable
    void SpawnHealthBoxAtRandomPoint()
    {
        // Creates a interger variable names pointIndex and sets it to be a
        // random number aout of the amount of points there are in the points
        // varaiable
        int pointIndex = Random.Range(0, points.Length);
        // Now it will move the position of the object this script is attached
        // to (the health box), to new vector 3 of the x position of the random
        // point chosen in the code right above, and same for the z position.
        // the y position remains the same
        transform.position = new Vector3(points[pointIndex].transform.position.x, 0, points[pointIndex].transform.position.z);
    }
}
