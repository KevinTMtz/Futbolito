using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    void Start()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();

        float directionX = (Random.Range(0,2) * 2 - 1) * Random.Range(10, 15);
        float directionZ = (Random.Range(0,2) * 2 - 1) * Random.Range(10, 15);
 
        rigidbody.AddForce(new Vector3(directionX, 0, directionZ));
    }
}
