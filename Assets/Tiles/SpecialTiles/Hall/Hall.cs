using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hall : MonoBehaviour
{
    public int MushLogs = 0, Souls = 0, Food = 0;


    public void GetMushLogs(int amount)
    {
        MushLogs += amount;
    }


    public void GetSouls(int amount)
    {
        Souls += amount;
    }


    public void GetFood(int amount)
    {
        Food += amount;
    }
}
