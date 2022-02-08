using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Arbitro arbitro;
    PlayerAgent playerAgentBlue;
    PlayerAgent playerAgentRed;

    Rigidbody rigidBody;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();

        ApplyRandomForce();
    }

    void FixedUpdate() {
        arbitro.NotTouching();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Untagged")
        {
            Debug.Log("Ball touch: " + other.gameObject.tag);
        }

        if (other.gameObject.tag == "PlayerRed")
        {
            arbitro.TouchedBall("PlayerRed");
        }
        else if (other.gameObject.tag == "PlayerBlue")
        {
            arbitro.TouchedBall("PlayerBlue");
        }
    }

    public void RestartPosition()
    {
        rigidBody.angularVelocity = Vector3.zero;
        rigidBody.velocity = Vector3.zero;
        rigidBody.transform.localPosition = new Vector3(0, 5, 3);
    }

    public void ApplyRandomForce()
    {
        float directionX = (Random.Range(0, 2) * 2 - 1) * Random.Range(10, 15);
        float directionZ = (Random.Range(0, 2) * 2 - 1) * Random.Range(10, 15);
 
        rigidBody.AddForce(new Vector3(directionX, 0, directionZ));
    }
}
