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
        BallAndBumper();
        //BallAndWall();

    }


    public void addPoints(int points)
    {
        playerPoints += points;
    }

    public bool isBallToCloseOfABumper()
    {
        foreach (Bumper bumper in arrayOfAllBumper)
        {
            float distanceBetBallAndBumper = Mathf.Sqrt(Mathf.Pow((bumper.transform.position.x - Ball.Instance.returnNextFramePositionX()), 2) + Mathf.Pow((bumper.transform.position.y - Ball.Instance.returnNextFramePositionY()), 2));
            Debug.Log(bumper + " est a telle distance : " + distanceBetBallAndBumper);

            if (distanceBetBallAndBumper < (bumper.rayon + Ball.Instance.rayon))
            {
                Debug.Log("collision");
                addPoints(bumper.pointsOfBumper);
                currentBumper = bumper;
               
                return true;
            }
        }

        currentBumper = null;
        return false;
    }

    /// <summary>
    /// Regarde les collisions entre la balle et le bumper et addapte le fonctionnement en fonction de la position
    /// </summary>
    public void BallAndBumper()
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
                    Debug.Log("END OF COLLISION /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////");
                    break;
                }
            }
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            Vector3 Ball2Bump = new Vector2(currentBumper.transform.position.x - Ball.Instance.transform.position.x, currentBumper.transform.position.y - Ball.Instance.transform.position.y);

            /*float Beta = Mathf.Atan(Ball2Bump.x / Ball2Bump.y);
            float Alpha = Mathf.Atan(Ball.Instance.speedX / Ball.Instance.speedY);*/

            float AlphaR = Vector2.Angle(Vector2.right, new Vector2(Ball.Instance.speedX, Ball.Instance.speedY)); // - (Mathf.PI * Mathf.Rad2Deg)
            Debug.Log("AlphaR : " + AlphaR);
            float BetaR = Vector2.Angle(Vector2.right, Ball2Bump);
            Debug.Log("BetaR : " + BetaR);

            float angleAlpha2 = 2 * BetaR - AlphaR;
            Debug.Log("angleAlpha2 : " + angleAlpha2);
            //Calcul de la vitesse globale en fonction de nos vitesses en X et en Y.
            float globalSpeedOfBall = Mathf.Sqrt(Mathf.Pow(Ball.Instance.speedX, 2) + Mathf.Pow(Ball.Instance.speedY, 2));

            Ball.Instance.speedX = -globalSpeedOfBall * Mathf.Cos(angleAlpha2 * Mathf.Deg2Rad);
            Ball.Instance.speedY = globalSpeedOfBall * Mathf.Sin(angleAlpha2 * Mathf.Deg2Rad);
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        }
    }


    /// <summary>
    /// Regarde les collisions entre la balle et les mur horizontaux et addapte le fonctionnement en fonction de la position
    /// </summary>
    public void BallAndHorizontalWall()
    {

        // Reprise du code du bumber, à adapter pour les walls



        /*
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
                    Debug.Log("END OF COLLISION /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////");
                    break;
                }
            }
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            Vector3 Ball2Bump = new Vector2(currentBumper.transform.position.x - Ball.Instance.transform.position.x, currentBumper.transform.position.y - Ball.Instance.transform.position.y);

            //float Beta = Mathf.Atan(Ball2Bump.x / Ball2Bump.y);
            //float Alpha = Mathf.Atan(Ball.Instance.speedX / Ball.Instance.speedY);

            float AlphaR = Vector2.Angle(Vector2.right, new Vector2(Ball.Instance.speedX, Ball.Instance.speedY)); // - (Mathf.PI * Mathf.Rad2Deg)
            Debug.Log("AlphaR : " + AlphaR);
            float BetaR = Vector2.Angle(Vector2.right, Ball2Bump);
            Debug.Log("BetaR : " + BetaR);

            float angleAlpha2 = 2 * BetaR - AlphaR;
            Debug.Log("angleAlpha2 : " + angleAlpha2);
            //Calcul de la vitesse globale en fonction de nos vitesses en X et en Y.
            float globalSpeedOfBall = Mathf.Sqrt(Mathf.Pow(Ball.Instance.speedX, 2) + Mathf.Pow(Ball.Instance.speedY, 2));

            Ball.Instance.speedX = -globalSpeedOfBall * Mathf.Cos(angleAlpha2 * Mathf.Deg2Rad);
            Ball.Instance.speedY = globalSpeedOfBall * Mathf.Sin(angleAlpha2 * Mathf.Deg2Rad);
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        }

        */
    }
}
