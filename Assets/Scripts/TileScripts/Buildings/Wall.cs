﻿using UnityEngine;

public class Wall : MonoBehaviour, IPassMethods
{
    public string PassMethodName(int methodNum)
    {
        //ToggleGate();

        switch (methodNum)
        {
            case 0:
                return nameof(Rotate);
            case 1:
                return "";
            case 2: 
                return "";
            case 3: 
                return "";
        }

        return null;
    }

    public Vector3Int PassMethodCosts(int methodNum)
    {
        switch (methodNum)
        {
            case 0:
                return Vector3Int.zero;
            case 1:
                return Vector3Int.zero;
            case 2: 
                return Vector3Int.zero;
            case 3: 
                return Vector3Int.zero;
        }

        return Vector3Int.zero;
    }

    public string PassMethodInfo(int methodNum)
    {
        switch (methodNum)
        {
            case 0:
                return nameof(Rotate);
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
    
    
    public void Rotate()
    {
        transform.Rotate(transform.rotation.z >= 90f ? Vector3.zero : new Vector3(0, 0f, 90f));
    }
}
