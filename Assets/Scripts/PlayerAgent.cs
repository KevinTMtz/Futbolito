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

    float moveVel;
    float maxAngVel;

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
        acciones[0] = Input.GetAxis("Horizontal");
        acciones[1] = Input.GetAxis("Vertical");
    }

    public override void OnActionReceived(ActionBuffers acc)
    {
        for (int i=0; i<palos.Length; i++)
        {
            palosRigidbodys[i].AddForce(0, 0, acc.ContinuousActions[1] * moveVel);
            palosRigidbodys[i].AddTorque(acc.ContinuousActions[0] * transform.forward * Time.deltaTime * 10000000);
        }
    }

    public void EndMatch()
    {
        EndEpisode();
    }

    public void WinMatch()
    {
        SetReward(10);
    }

    public void Goal()
    {
        SetReward(6);
    }

    public void BlockedEnemyShot()
    {
        SetReward(2);
    }

    public void TouchedBall()
    {
        SetReward(1);
    }

    public void LooseMatch()
    {
        SetReward(-10);
    }

    public void OwnGoal()
    {
        SetReward(-6);
    }

    public void RestartedBall()
    {
        SetReward(-1);
    }
}
