using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Ball : MonoBehaviour
{
    public static Ball Instance;

    public float speedY;
    public float speedX;
    public float inclinaisonFlipper = 20;
    public float gravity = 9.8f;
    public float rayon = 2;

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

    public float returnNextFramePositionY()
    {
        return transform.position.y + (speedY - (Mathf.Sin(Mathf.Deg2Rad * inclinaisonFlipper) * gravity) * Time.deltaTime);
    }
    public float returnNextFramePositionX()
    {
        return transform.position.x + speedX * Time.deltaTime;
    }

    public float returnNextFramePositionYDiv100()
    {
        return transform.position.y + ((speedY - (Mathf.Sin(Mathf.Deg2Rad * inclinaisonFlipper) * gravity)) * Time.deltaTime) / 100;
    }
    public float returnNextFramePositionXDiv100()
    {
        return transform.position.x + (speedX  * Time.deltaTime) / 100;
    }
}
