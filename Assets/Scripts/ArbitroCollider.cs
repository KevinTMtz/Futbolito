using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArbitroCollider : MonoBehaviour
{
    Arbitro arbitro;

    void Start()
    {
        arbitro = GetComponentInParent<Arbitro>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Pelota")
        {
            arbitro.Goal(gameObject.name);
        }
    }
}
