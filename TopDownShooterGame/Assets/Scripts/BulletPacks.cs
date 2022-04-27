using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPacks : MonoBehaviour
{
    public GameObject[] bulletPoints;
    // Start is called before the first frame update
    void Start()
    {
        SpawnBulletPackAtRandomPoint();
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
            SpawnBulletPackAtRandomPoint();
        }
    }
    void SpawnBulletPackAtRandomPoint()
    {
        int pointIndex = Random.Range(0, bulletPoints.Length);
        transform.position = new Vector3(bulletPoints[pointIndex].transform.position.x, 0, bulletPoints[pointIndex].transform.position.z);
    }
}
