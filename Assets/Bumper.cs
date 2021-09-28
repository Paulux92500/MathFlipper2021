using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bumper : MonoBehaviour
{
    float distanceBetBallAndBumper;

    public float rayon = 20;
    public int pointsOfBumper = 10 ;

    // Update is called once per frame
    void Update()
    {
        distanceBetBallAndBumper = (float)Math.Sqrt((float)Math.Pow((transform.position.x - Ball.Instance.transform.position.x), 2) + (float)Math.Pow((transform.position.y - Ball.Instance.transform.position.y), 2));

        if (distanceBetBallAndBumper < rayon + Ball.Instance.rayon)
        {
            Debug.Log("collision");
            Ball.Instance.speedY += 2;
            ManagerFlipper.Instance.addPoints(pointsOfBumper);
        }
    }
}
