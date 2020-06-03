using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour, IInteractable, IPassMethods
{
    public bool isOpen = false;
    private A_Building a_Building;
    private Action action1, action2, action3, action4; 

    private void Start()
    {
        if (TryGetComponent<A_Building>(out A_Building buildingFound)) a_Building = buildingFound;
        if (a_Building.animator)
        {

        }
    }


    private void Update() // Use to run constant processes etc. that have been activated or changed based on actions taken etc.
    { 
        // If this condition case is true do this process etc.
    }


    public Action PassMethods(int methodNum)
    {
        //ToggleGate();

        switch (methodNum)
        {
            case 1: ToggleGate();



                break;
            case 2:
                
                

                break;
            case 3: 
                
                
                
                break;
            case 4: 
                
                
                
                break;
        }

        return null;
    }


    public void Interact()
    {
        //Switch();
    }


    public Action CurrentAction1() { return action1; }
    public Action CurrentAction2() { return action2; }
    public Action CurrentAction3() { return action3; }
    public Action CurrentAction4() { return action4; }


    // Toggles Gate to Open or Closed
    public void ToggleGate()
    {
        if (isOpen)
        {
            // Close gate
        }
        else
        {
            // Open Gate
        }

        isOpen = !isOpen;
    }


}