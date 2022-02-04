using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Declaracion del componente Rigidbody para mover los palos
    Rigidbody rigidBody;

    //parametros torque del palo, incrementar en Unity si se requiere giros mas rapidos
    float moveVel;
    float maxAngVel;

    // Start es llamado en el primer frame del juego
    void Start()
    {
        moveVel = 50;
        maxAngVel = 10;
        
        //conectamos el rigidbody con el componente rigidbody del palo
        rigidBody = GetComponent<Rigidbody>();
        
        //asignamos la velocidad del giro
        rigidBody.maxAngularVelocity = maxAngVel;
    }

    // Update es llamado durante cada frame del juego
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        rigidBody.AddForce(0,0, v * moveVel);
        rigidBody.AddTorque(transform.forward * h * 10000000 * Time.deltaTime);
    }
}
