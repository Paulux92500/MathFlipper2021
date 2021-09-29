using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bumper : MonoBehaviour
{
    [System.NonSerialized]
    public float distanceBetBallAndBumper;

    public float rayon = 20;
    public int pointsOfBumper = 10 ;
    [Range(1,2)]
    public float bumpPower = 1;
}
