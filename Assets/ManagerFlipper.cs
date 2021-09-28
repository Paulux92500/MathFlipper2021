using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ManagerFlipper : MonoBehaviour
{

    public static ManagerFlipper Instance;

    public int playerPoints = 0;

    public Bumper[] arrayOfAllBumper;

    private Bumper currentBumper = null;

    private void Awake()
    {
        if (Instance)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    

    private void Update()
    {
        if (!isBallToCloseOfABumper()) // Si je m'apprete a ne pas rentrer en collision avec l'un des bumpers.
        {
            Ball.Instance.speedY += -((float)Math.Sin(Mathf.Deg2Rad * Ball.Instance.inclinaisonFlipper) * Ball.Instance.gravity) * Time.deltaTime;
            Ball.Instance.transform.position = new Vector3(Ball.Instance.transform.position.x, Ball.Instance.transform.position.y + Ball.Instance.speedY, Ball.Instance.transform.position.z);
        }

        else //Si je vais rentrer en collision avec un bumper a la prochaine frame.
        {
            for (int i = 0; i < 100; i++) // On rapproche au maximum notre balle de l'objet pour faire de bons calculs de collision
            {
                float distanceBetBallAndBumper = (float)Math.Sqrt((float)Math.Pow((currentBumper.transform.position.x - Ball.Instance.transform.position.x), 2) + (float)Math.Pow((currentBumper.transform.position.y - Ball.Instance.transform.position.y), 2));

                if (distanceBetBallAndBumper < currentBumper.rayon + Ball.Instance.rayon)
                {
                    Ball.Instance.transform.position = new Vector3(Ball.Instance.transform.position.x, Ball.Instance.transform.position.y + Ball.Instance.speedY / 100, Ball.Instance.transform.position.z);
                }
                else
                {
                    break;
                }
            }
        }
    }


    public void addPoints(int points)
    {
        playerPoints += points;
    }

    public bool isBallToCloseOfABumper()
    {
        foreach (Bumper item in arrayOfAllBumper)
        {
            item.distanceBetBallAndBumper = (float)Math.Sqrt((float)Math.Pow((item.transform.position.x - Ball.Instance.transform.position.x), 2) + (float)Math.Pow((item.transform.position.y - Ball.Instance.returnNextFramePositionY()), 2));

            if (item.distanceBetBallAndBumper < item.rayon + Ball.Instance.rayon)
            {
                Debug.Log("collision");
                addPoints(item.pointsOfBumper);
                currentBumper = item;
                return true;
            }
        }

        currentBumper = null;
        return false;
    }
}
