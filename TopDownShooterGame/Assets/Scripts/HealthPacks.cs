using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPacks : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject point;

    void Start()
    {
        transform.position = new Vector3(point.transform.position.x, 0, point.transform.position.z);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
