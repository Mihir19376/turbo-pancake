using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPacks : MonoBehaviour
{
    public GameObject[] speedPoints;
    // Start is called before the first frame update
    void Start()
    {
        SpawnSpeedBoxAtRandomPoint();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Again, Self Explanitory
            SpawnSpeedBoxAtRandomPoint();
        }
    }

    void SpawnSpeedBoxAtRandomPoint()
    {
        int pointIndex = Random.Range(0, speedPoints.Length);
        transform.position = new Vector3(speedPoints[pointIndex].transform.position.x, 0, speedPoints[pointIndex].transform.position.z);
    }
}
