using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arbitro : MonoBehaviour
{
    public Ball ball;
    public PlayerAgent playerAgentBlue;
    public PlayerAgent playerAgentRed;

    string lastTouched;

    float lastTouchedTime;
    float timeToRestart;

    void Start()
    {
        lastTouched = "";

        lastTouchedTime = Time.time;
        timeToRestart = 15;
    }

    void Update()
    {
        if (Time.time - lastTouchedTime > timeToRestart)
        {
            RestartMatch();
        }
    }

    public void Goal(string teamGoal)
    {
        if (teamGoal == "RedGoal")
        {
            if (lastTouched == "PlayerRed")
            {
                Debug.Log("Red own goal");
                playerAgentRed.OwnGoal();
            }
            else
            {
                Debug.Log("Blue goal");
                playerAgentBlue.Goal();
            }
        }
        else if (teamGoal == "BlueGoal")
        {
            if (lastTouched == "PlayerBlue")
            {
                Debug.Log("Blue own goal");
                playerAgentBlue.OwnGoal();
            }
            else
            {
                Debug.Log("Red goal");
                playerAgentRed.Goal();
            }
        }

        RestartMatch();
    }

    void RestartMatch()
    {
        ball.RestartPosition();
        ball.ApplyRandomForce();
        
        lastTouched = "";

        lastTouchedTime = Time.time;

        playerAgentBlue.EndMatch();
        playerAgentRed.EndMatch();
    }

    public void TouchedBall(string whoTouched)
    {
        if (whoTouched == "PlayerRed")
        {
            lastTouched = "PlayerRed";
            playerAgentRed.TouchedBall();

            lastTouchedTime = Time.time;
        }
        else if (whoTouched == "PlayerBlue")
        {
            lastTouched = "PlayerBlue";
            playerAgentBlue.TouchedBall();

            lastTouchedTime = Time.time;
        }
    }
}
