using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour, IPassMethods
{
    public string PassMethodName(int methodNum)
    {
        //ToggleGate();

        switch (m_ABuilding.currentUpgradeLevel)
        {
            case 0:
                
                switch (methodNum)
                {
                    case 0: return nameof(ToggleGate);
                    case 1: return nameof(Rotate);
                    case 2: return "";
                    case 3: return "";
                }

                break;
            case 1:

                switch (methodNum)
                {
                    case 0: return nameof(ToggleGate);
                    case 1: return "";
                    case 2: return "";
                    case 3: return "";
                }

                break;
        }
        
        return null;
    }


    public Vector3Int PassMethodCosts(int methodNum)
    {
        switch (m_ABuilding.currentUpgradeLevel)
                {
                    case 0:
                        
                        switch (methodNum)
                        {
                            case 0: return new Vector3Int(1, 1, 1);
                            case 1: return Vector3Int.zero;
                            case 2: return Vector3Int.zero;
                            case 3: return Vector3Int.zero;
                        }
        
                        break;
                    case 1:
        
                        switch (methodNum)
                        {
                            case 0: return Vector3Int.zero;
                            case 1: return Vector3Int.zero;
                            case 2: return Vector3Int.zero;
                            case 3: return Vector3Int.zero;
                        }
        
                        break;
                }
                
                return Vector3Int.zero;
    }


    public string PassMethodInfo(int methodNum)
    {
        switch (m_ABuilding.currentUpgradeLevel)
        {
            case 0:
                
                switch (methodNum)
                {
                    case 0: return "stuff stuff";
                    case 1: return "";
                    case 2: return "";
                    case 3: return "";
                }

                break;
            case 1:

                switch (methodNum)
                {
                    case 0: return "";
                    case 1: return "";
                    case 2: return "";
                    case 3: return "";
                }

                break;
        }
        
        return null;
    }


    public string GetScriptName()
    {
        return this.GetType().Name;
    }


    private A_Building m_ABuilding;

    private static readonly int CloseGate = Animator.StringToHash("CloseGate");
    private static readonly int OpenGate = Animator.StringToHash("OpenGate");
    public bool isOpen;


    private void Start()
    {
        if (!TryGetComponent<A_Building>(out var buildingFound)) return;
        m_ABuilding = buildingFound;
    }


    private void Update() // Use to run constant processes etc. that have been activated or changed based on actions taken etc.
    { 
        //TODO: If this condition case is true do this process etc.
    }


    // Toggles Gate to Open or Closed
    public void ToggleGate()
    {
        if (isOpen)
        {
            // Close gate
            m_ABuilding.animator.SetTrigger(CloseGate);
            Debug.Log("Gate Closed");
        }
        else
        {
            // Open Gate
            m_ABuilding.animator.SetTrigger(OpenGate);
            Debug.Log("Gate Opened");
        }

        isOpen = !isOpen;
    }

    public void Rotate()
    {
        transform.Rotate(transform.rotation.z >= 90f ? Vector3.zero : new Vector3(0, 0f, 90f));
    }
}
