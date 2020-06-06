using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hall : MonoBehaviour, IPassMethods
{
    public string PassMethodName(int methodNum)
    {
        //ToggleGate();

        switch (methodNum)
        {
            case 0:
                return "";
            case 1:
                return "";
            case 2:
                return "";
            case 3:
                return "";
        }

        return null;
    }


    public string GetScriptName()
    {
        return this.GetType().Name;
    }


    private A_Building m_ABuilding;


    private void Start()
    {
        if (!TryGetComponent<A_Building>(out var buildingFound)) return;
        m_ABuilding = buildingFound;
    }
}