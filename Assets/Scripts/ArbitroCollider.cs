using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArbitroCollider : MonoBehaviour
{
    public Arbitro arbitro;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Pelota")
        {
            arbitro.Goal(gameObject.name);
        }
    }
}
