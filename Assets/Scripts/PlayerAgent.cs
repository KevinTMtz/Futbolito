using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class PlayerAgent : Agent
{
    public GameObject[] palos;
    Rigidbody[] palosRigidbodys;

    public Rigidbody[] enemyPalosRigidbodys;

    public Transform ball;
    public Rigidbody ballRigidBody;

    public string axisXName;
    public string axisYName;

    public int switchControlOffset;

    float moveVel;
    float maxAngVel;

    // Posicion del palo que se esta moviendo.
    private int paloSeleccionado;
    private bool touching;

    public override void Initialize()
    {
        // Empezamos moviendo el palo de en medio.
        this.paloSeleccionado = 1;
    }

    void Start()
    {
        moveVel = 50;
        maxAngVel = 10;
        
        palosRigidbodys = new Rigidbody[4];
        
        for (int i=0; i<palos.Length; i++)
        {
            palosRigidbodys[i] = palos[i].GetComponent<Rigidbody>();
            palosRigidbodys[i].maxAngularVelocity = maxAngVel;
        }
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // For the ball
        sensor.AddObservation(ball.localPosition);
        sensor.AddObservation(ballRigidBody.velocity);

        // For our team
        for (int i=0; i<palos.Length; i++)
        {
            sensor.AddObservation(palosRigidbodys[i].rotation.z);
            sensor.AddObservation(palosRigidbodys[i].position);
        }

        // For the other team
        for (int i=0; i<enemyPalosRigidbodys.Length; i++)
        {
            sensor.AddObservation(enemyPalosRigidbodys[i].rotation.z);
            sensor.AddObservation(enemyPalosRigidbodys[i].position);
        }
    }

    public override void Heuristic(in ActionBuffers salidaAcciones)
    {
        var acciones = salidaAcciones.ContinuousActions;
        acciones[0] = Input.GetAxis(this.axisXName);
        acciones[1] = Input.GetAxis(this.axisYName);

        // Cambiar el palo que se controla.
        var accion_discreta = salidaAcciones.DiscreteActions;
        // Se cambia el palo con las teclas de numeros
        for (int i = 1; i <= this.palos.Length; i++)
        {
            if (Input.GetKeyDown(""+(i+this.switchControlOffset)))
            {
                Debug.Log("Captando"+(i+this.switchControlOffset));
                accion_discreta[0] = i;
            }
        }

    }

    public override void OnActionReceived(ActionBuffers acc)
    {
        // Cambiar palo seleccionado, teclas 1 - num.palos
        if (acc.DiscreteActions[0] > 0)
        {
            this.paloSeleccionado = acc.DiscreteActions[0] - 1;

            // Recompensa si el palo seleccionado esta cerca de la pelota.
            var ballX = this.ballRigidBody.transform.position.x;
            var paloX = this.palosRigidbodys[this.paloSeleccionado].transform.position.x;
            if (ballX > paloX - 2.0f && ballX < paloX + 2.0f)
            {
                AddReward(0.2f);
            }
        }


        // Mover el palo seleccionado
        var palo = this.palosRigidbodys[this.paloSeleccionado];
        palo.AddForce(0, 0, acc.ContinuousActions[1] * moveVel);
        palo.AddTorque(acc.ContinuousActions[0] * transform.forward * Time.deltaTime * 10000000);

        // Penalizar si hubo disparo sin tocar la pelota
        if (!touching && (acc.ContinuousActions[0] != 0))
        {
            AddReward(-0.005f);
        }
    }

    public void EndMatch()
    {
        EndEpisode();
    }

    public void WinMatch()
    {
        AddReward(10);
    }

    public void Goal()
    {
        AddReward(6);
    }

    public void BlockedEnemyShot()
    {
        AddReward(2);
    }

    public void TouchedBall()
    {
        AddReward(1);
        this.touching = true;
    }

    public void NotTouching()
    {
        this.touching = false;
    }

    public void LooseMatch()
    {
        AddReward(-10);
    }

    public void OwnGoal()
    {
        AddReward(-6);
    }

    public void RestartedBall()
    {
        AddReward(-1);
    }
}
