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
            if (currentBumper.formeObject == Enums.Forme.Rond)
            {
                //////////////////////////////////////////////Rediviser par 100 la vitesse pour s'approcher au maximum//////////////////////////////////////////////////////////////////////////////
                for (int i = 0; i < 100; i++) // On rapproche au maximum notre balle de l'objet pour faire de bons calculs de collision
                {
                    float distanceBetBallAndBumper = Mathf.Sqrt(Mathf.Pow(currentBumper.transform.position.x - Ball.Instance.returnNextFramePositionXDiv100(), 2) + Mathf.Pow(currentBumper.transform.position.y - Ball.Instance.returnNextFramePositionYDiv100(), 2));
                    if (distanceBetBallAndBumper > currentBumper.rayon + Ball.Instance.rayon)
                    {
                        Ball.Instance.transform.position = new Vector3(Ball.Instance.transform.position.x + Ball.Instance.speedX / 100, Ball.Instance.transform.position.y + Ball.Instance.speedY / 100, Ball.Instance.transform.position.z);
                    }
                    else
                    {
                        break;
                    }
                }
                //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                
                Vector3 Ball2Bump = new Vector2(currentBumper.transform.position.x - Ball.Instance.transform.position.x, currentBumper.transform.position.y - Ball.Instance.transform.position.y);

                float AlphaR = Vector2.Angle(Vector2.right, new Vector2(Ball.Instance.speedX, Ball.Instance.speedY)); // - (Mathf.PI * Mathf.Rad2Deg)
                float BetaR = Vector2.Angle(Vector2.right, Ball2Bump);

                float angleAlpha2 = 2 * BetaR - AlphaR;

                //Calcul de la vitesse globale en fonction de nos vitesses en X et en Y.
                float globalSpeedOfBall = Mathf.Sqrt(Mathf.Pow(Ball.Instance.speedX, 2) + Mathf.Pow(Ball.Instance.speedY, 2));

                Ball.Instance.speedX = -globalSpeedOfBall * Mathf.Cos(angleAlpha2 * Mathf.Deg2Rad);
                Ball.Instance.speedY = globalSpeedOfBall * Mathf.Sin(angleAlpha2 * Mathf.Deg2Rad);

                Ball.Instance.speedX *= 0.95f;
                Ball.Instance.speedY *= 0.95f;
            }

            if (currentBumper.formeObject == Enums.Forme.Rectangle)
            {

            }

        }
    }


    public void addPoints(int points)
    {
        playerPoints += points;
    }

    public bool isBallToCloseOfABumper()
    {
        foreach (Bumper bumper in arrayOfAllBumper)
        {

            if (bumper.formeObject == Enums.Forme.Rond)
            {
                float distanceBetBallAndBumper = Mathf.Sqrt(Mathf.Pow((bumper.transform.position.x - Ball.Instance.returnNextFramePositionX()), 2) + Mathf.Pow((bumper.transform.position.y - Ball.Instance.returnNextFramePositionY()), 2));

                if (distanceBetBallAndBumper < (bumper.rayon + Ball.Instance.rayon))
                {
                    Debug.Log("collision rond");
                    addPoints(bumper.pointsOfBumper);
                    currentBumper = bumper;
                    return true;
                }
            }

            if (bumper.formeObject == Enums.Forme.Rectangle || bumper.formeObject == Enums.Forme.Flipper)
            {
                if (Math.Abs(bumper.transform.position.y - Ball.Instance.returnNextFramePositionY()) < (bumper.scaleY / 2) + Ball.Instance.rayon && bumper.transform.position.x - (bumper.scaleX/2) < Ball.Instance.transform.position.x + Ball.Instance.rayon && bumper.transform.position.x + (bumper.scaleX / 2) > Ball.Instance.transform.position.x - Ball.Instance.rayon)
                {
                    Debug.Log("collision rectangle haut ou bas");
                    addPoints(bumper.pointsOfBumper);
                    currentBumper = bumper;

                    Ball.Instance.speedY *= -1;

                    if (bumper.formeObject == Enums.Forme.Flipper && !bumper.poigneeOfBumper.stayDown)
                    {
                        Ball.Instance.speedX *= 1.15f;
                        Ball.Instance.speedY *= 1.15f;
                    }

                    return true;
                }

                if (Math.Abs(bumper.transform.position.x - Ball.Instance.returnNextFramePositionX()) < (bumper.scaleX / 2) + Ball.Instance.rayon && bumper.transform.position.y - (bumper.scaleY / 2) < Ball.Instance.transform.position.y + Ball.Instance.rayon && bumper.transform.position.y + (bumper.scaleY / 2) > Ball.Instance.transform.position.y - Ball.Instance.rayon)
                {
                    Debug.Log("collision rectangle lateral");
                    addPoints(bumper.pointsOfBumper);
                    currentBumper = bumper;

                    Ball.Instance.speedX *= -1;

                    return true;
                }

                
            }
        }

        currentBumper = null;
        return false;
    }
}
