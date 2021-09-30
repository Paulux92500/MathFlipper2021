using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bumper : MonoBehaviour
{
    [System.NonSerialized]
    public float distanceBetBallAndBumper;

    public Enums.Forme formeObject;

    [Header("Rond")]
    public float rayon = 20;

    [Header("Rectangle")]
    public float scaleX;
    public float scaleY;

    [Header("Gameplay")]
    public int pointsOfBumper = 10 ;
    [Range(1,2)]
    public float bumpPower = 1;

    public Poignee poigneeOfBumper;

    private void Start()
    {
        switch (formeObject)
        {
            case Enums.Forme.Rond:
                rayon = transform.localScale.x / 2;
                break;
            case Enums.Forme.Rectangle:
                scaleX = transform.localScale.x;
                scaleY = transform.localScale.y;
                break;
            case Enums.Forme.Flipper:
                scaleX = transform.localScale.x * 10;
                scaleY = transform.localScale.y * 10;
                break;
            default:
                break;
        }
    }
}
