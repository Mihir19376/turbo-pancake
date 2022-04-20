using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPacks : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] point;

    void Start()
    {
        int pointIndex = Random.Range(0, point.Length);

        transform.position = new Vector3(point[pointIndex].transform.position.x, 0, point[pointIndex].transform.position.z);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
