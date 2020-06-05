using UnityEngine;

public class Farm : MonoBehaviour, IPassMethods
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
    
    
    void Start()
    {
        if (!TryGetComponent<A_Building>(out var buildingFound)) return;
        m_ABuilding = buildingFound;
    }
    

    public void GenerateMissingPlantFields()
    {
        
    }

    
    public void DestroyAllPlantFields()
    {
        
    }
}
