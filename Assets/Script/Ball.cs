using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Ball : MonoBehaviour
{
    public static Ball Instance;

    public float speedY;
    public float speedX;
    private float inclinaisonFlipper = 20;
    private float gravity = 9.8f;
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

    // Update is called once per frame
    void Update()
    {
        speedY += -((float)Math.Sin(Mathf.Deg2Rad * inclinaisonFlipper) * gravity) * Time.deltaTime;
        transform.position = new Vector3(transform.position.x, transform.position.y + speedY, transform.position.z);
    }
}
