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
    public int forceTilt = 1;

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
        return transform.position.y + speedY;
    }
    public float returnNextFramePositionX()
    {
        return transform.position.x + speedX;
    }

    public float returnNextFramePositionYDiv100()
    {
        return transform.position.y + speedY / 100;
    }
    public float returnNextFramePositionXDiv100()
    {
        return transform.position.x + speedX / 100;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            speedX -= forceTilt;
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            speedX += forceTilt;
        }
    }
}
