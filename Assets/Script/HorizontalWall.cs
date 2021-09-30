using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HorizontalWall : MonoBehaviour
{
    [System.NonSerialized]
    public float distanceBetBallAndWall;

    //public float rayon = 20;
    public int pointsOfWall = 5;
    [Range(1, 2)]
    public float bumpPower = 1;
}
