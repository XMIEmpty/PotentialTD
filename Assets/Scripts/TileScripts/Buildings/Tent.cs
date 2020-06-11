using UnityEngine;

public class Tent : MonoBehaviour,IPassMethods
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
