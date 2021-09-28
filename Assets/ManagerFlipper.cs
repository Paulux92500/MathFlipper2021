using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerFlipper : MonoBehaviour
{

    public static ManagerFlipper Instance;

    public int playerPoints = 0;

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

    public void addPoints(int points)
    {
        playerPoints += points;
    }
}
