using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Declaracion del componente Rigidbody para mover los palos
    Rigidbody rbody;

    //parametros torque del palo, incrementar en Unity si se requiere giros mas rapidos
    float maxAngVel;

    // Start es llamado en el primer frame del juego
    void Start()
    {
        //conectamos el rbody con el componente rigidbody del palo
        rbody = GetComponent<Rigidbody>();
        
        //asignamos la velocidad del giro
        rbody.maxAngularVelocity = 10;
    }

    // Update es llamado durante cada frame del juego
    void Update()
    {
        float h = Input.GetAxis("Horizontal") * 10000 * Time.deltaTime;
        float v = Input.GetAxis("Vertical");
        rbody.AddForce(0,0, v * 100);
        rbody.AddTorque(transform.forward * h * 1000);
    }
}
