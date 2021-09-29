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
    

    private void FixedUpdate()
    {
        if (!isBallToCloseOfABumper()) // Si je m'apprete a ne pas rentrer en collision avec l'un des bumpers.
        {
            Ball.Instance.speedY += -(Mathf.Sin(Mathf.Deg2Rad * Ball.Instance.inclinaisonFlipper) * Ball.Instance.gravity) * Time.deltaTime;
            Ball.Instance.transform.position = new Vector3(Ball.Instance.transform.position.x + Ball.Instance.speedX, Ball.Instance.transform.position.y + Ball.Instance.speedY, Ball.Instance.transform.position.z);
        }

        else //Si je vais rentrer en collision avec un bumper a la prochaine frame.
        {
            //////////////////////////////////////////////Rediviser par 100 la vitesse pour s'approcher au maximum//////////////////////////////////////////////////////////////////////////////
            for (int i = 0; i < 100; i++) // On rapproche au maximum notre balle de l'objet pour faire de bons calculs de collision
            {
                float distanceBetBallAndBumper = Mathf.Sqrt(Mathf.Pow(currentBumper.transform.position.x - Ball.Instance.returnNextFramePositionXDiv100(), 2) + Mathf.Pow(currentBumper.transform.position.y - Ball.Instance.returnNextFramePositionYDiv100(), 2));
                Debug.Log("distance between Ball And Bumper : " + distanceBetBallAndBumper + " et somme des rayons :" + (currentBumper.rayon + Ball.Instance.rayon).ToString());
                if (distanceBetBallAndBumper > currentBumper.rayon + Ball.Instance.rayon)
                {
                    Debug.Log("i = " + i);
                    Ball.Instance.transform.position = new Vector3(Ball.Instance.transform.position.x + Ball.Instance.speedX / 100, Ball.Instance.transform.position.y + Ball.Instance.speedY / 100, Ball.Instance.transform.position.z);
                }
                else
                {
                    break;
                }
            }
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            Vector3 Bump2Ball = new Vector3(Ball.Instance.transform.position.x - currentBumper.transform.position.x, Ball.Instance.transform.position.y - currentBumper.transform.position.y, Ball.Instance.transform.position.z - currentBumper.transform.position.z);
            float Beta = Mathf.Atan(Bump2Ball.x / Bump2Ball.y);
            float Alpha = Mathf.Atan(Ball.Instance.speedX / Ball.Instance.speedY);
            Debug.Log(Alpha);

            /*float distanceBallEtBumper = Ball.Instance.transform.position.y - currentBumper.transform.position.y;
            if (distanceBallEtBumper < 0)
            {
                Alpha += Mathf.PI;
            }
*/
            Debug.Log(Alpha);
            float angleAlpha2 = 2 * Beta - Alpha;

            float globalSpeedOfBall = Mathf.Sqrt(Mathf.Pow(Ball.Instance.speedX, 2) + Mathf.Pow(Ball.Instance.speedY, 2));

            Ball.Instance.speedX = globalSpeedOfBall * Mathf.Sin(angleAlpha2);
            Ball.Instance.speedY = globalSpeedOfBall * Mathf.Cos(angleAlpha2);
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///

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
            float distanceBetBallAndBumper = Mathf.Sqrt(Mathf.Pow((item.transform.position.x - Ball.Instance.returnNextFramePositionX()), 2) + Mathf.Pow((item.transform.position.y - Ball.Instance.returnNextFramePositionY()), 2));

            if (distanceBetBallAndBumper < (item.rayon + Ball.Instance.rayon))
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
