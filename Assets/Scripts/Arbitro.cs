using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arbitro : MonoBehaviour
{
    public Ball ball;
    public PlayerAgent playerAgentBlue;
    public PlayerAgent playerAgentRed;

    string lastTouched;

    int maxGoalCount;

    float lastTouchedTime;
    float timeToRestart;

    int blueGoalCount;
    int redGoalCount;

    void Start()
    {
        lastTouched = "";

        maxGoalCount = 3;

        lastTouchedTime = Time.time;
        timeToRestart = 15;

        blueGoalCount = 0;
        redGoalCount = 0;
    }

    void Update()
    {
        if (Time.time - lastTouchedTime > timeToRestart)
        {
            playerAgentBlue.RestartedBall();
            playerAgentRed.RestartedBall();

            RestartBall();
        }

        if (blueGoalCount >= maxGoalCount)
        {
            playerAgentBlue.WinMatch();
            playerAgentRed.LooseMatch();

            RestartMatch();
            Debug.Log("Blue wins!!!");
        }
        else if (redGoalCount >= maxGoalCount)
        {
            playerAgentBlue.LooseMatch();
            playerAgentRed.WinMatch();

            RestartMatch();
            Debug.Log("Red wins!!!");
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

            blueGoalCount++;
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

            redGoalCount++;
        }

        RestartBall();

        Debug.Log("Blue: " + blueGoalCount + " - Red: " + redGoalCount);
    }

    void RestartBall()
    {
        ball.RestartPosition();
        ball.ApplyRandomForce();
        
        lastTouched = "";

        lastTouchedTime = Time.time;
    }

    void RestartMatch()
    {
        RestartBall();

        blueGoalCount = 0;
        redGoalCount = 0;

        playerAgentBlue.EndMatch();
        playerAgentRed.EndMatch();
    }

    public void TouchedBall(string whoTouched)
    {
        if (whoTouched == "PlayerRed")
        {
            if (lastTouched == "PlayerBlue")
            {
                playerAgentRed.BlockedEnemyShot();
            }

            lastTouched = "PlayerRed";
            playerAgentRed.TouchedBall();

            lastTouchedTime = Time.time;
        }
        else if (whoTouched == "PlayerBlue")
        {
            if (lastTouched == "PlayerRed")
            {
                playerAgentBlue.BlockedEnemyShot();
            }

            lastTouched = "PlayerBlue";
            playerAgentBlue.TouchedBall();

            lastTouchedTime = Time.time;
        }
    }

    public void NotTouching()
    {
        playerAgentRed.NotTouching();
        playerAgentBlue.NotTouching();
    }
}
