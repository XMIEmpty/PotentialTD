using System;
using UnityEngine;

public class Gate : MonoBehaviour, IPassMethods
{
    public bool isOpen = false;
    private A_Building m_ABuilding;
    private Action action1, action2, action3, action4;
    
    
    private void Start()
    {
        if (!TryGetComponent<A_Building>(out var buildingFound)) return;
        m_ABuilding = buildingFound;
    }


    private void Update() // Use to run constant processes etc. that have been activated or changed based on actions taken etc.
    { 
        //TODO: If this condition case is true do this process etc.
    }


    public string PassMethodName(int methodNum)
    {
        //ToggleGate();

        switch (methodNum)
        {
            case 0:
                return nameof(ToggleGate);
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


    // Toggles Gate to Open or Closed
    public void ToggleGate()
    {
        if (isOpen)
        {
            // Close gate
            Debug.Log("Gate Closed");
        }
        else
        {
            // Open Gate
            Debug.Log("Gate Opened");
        }

        isOpen = !isOpen;
    }


}