using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arbitro : MonoBehaviour
{

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnTriggerEnter(Collider ball)
    {
        if (ball.gameObject.tag == "Pelota")
        {
            ball.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            ball.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            ball.gameObject.GetComponent<Rigidbody>().transform.localPosition = new Vector3(0, 5.0f, 0);
        }
    }
}
