using UnityEngine;

public class Farm : MonoBehaviour, IPassMethods, IBuildingExtras
{
    public string PassMethodName(int methodNum)
    {
        //ToggleGate();
        switch (m_ABuilding.currentUpgradeLevel)
        {
            case 0:
                
                switch (methodNum)
                {
                    case 0:
                        return nameof(GatherGoods);
                    case 1:
                        return "";
                    case 2:
                        return "";
                    case 3:
                        return "";
                }

                break;
            case 1:
                
                switch (methodNum)
                {
                    case 0:
                        return nameof(GatherGoods);
                    case 1:
                        return "";
                    case 2:
                        return "";
                    case 3:
                        return "";
                }
                
                break;
            case 2:
                
                switch (methodNum)
                {
                    case 0:
                        return nameof(GatherGoods);
                    case 1:
                        return "";
                    case 2:
                        return "";
                    case 3:
                        return "";
                }
                
                break;
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
        switch (m_ABuilding.currentUpgradeLevel)
        {
            case 0:
                switch (methodNum)
                {
                    case 0:
                        return "Gather " + amountOfGoods + " MushLogs on Click!";
                    case 1:
                        return "";
                    case 2:
                        return "";
                    case 3:
                        return "";
                }

                break;
            case 1:
                switch (methodNum)
                {
                    case 0:
                        return "Gather " + amountOfGoods*3 + " MushLogs on Click!";
                    case 1:
                        return "";
                    case 2:
                        return "";
                    case 3:
                        return "";
                }

                break;
            case 2:
                switch (methodNum)
                {
                    case 0:
                        return "Gather " + amountOfGoods*8 + " MushLogs on Click!";
                    case 1:
                        return "";
                    case 2:
                        return "";
                    case 3:
                        return "";
                }

                break;
        }

        return null;
    }


    public string GetScriptName()
    {
        return this.GetType().Name;
    }

    public void OnUpgrade()
    {
        switch (m_ABuilding.currentUpgradeLevel)
        {
            case 1: constantGoods *= 2; break;
            case 2: constantGoods *= 4; break;
        }
    }
    
    private A_Building m_ABuilding;
    [SerializeField] private GameObject coolVfx;
    [SerializeField] private GameObject coolerVfx;
    [SerializeField] private int amountOfGoods;
    [SerializeField] private int constantGoods;

    private void Start()
    {
        if (!TryGetComponent<A_Building>(out var buildingFound)) return;
        m_ABuilding = buildingFound;
        InvokeRepeating(nameof(ContinuousIncome), 0f, 1f);
    }

    private void ContinuousIncome()
    {
        m_ABuilding.tileHandling.resourceBarManager.AddMushLog(constantGoods);
        Instantiate(coolVfx, transform.position + new Vector3(0f, 0.5f, 0f), Quaternion.identity, gameObject.transform);
    }


    public void GatherGoods()
    {
        switch (m_ABuilding.currentUpgradeLevel)
        {
            case 0:
                Instantiate(coolVfx, transform.position + new Vector3(0f, 0.5f, 0f), Quaternion.identity,
                    gameObject.transform);
                m_ABuilding.tileHandling.resourceBarManager.AddMushLog(amountOfGoods);
                break;
            case 1:
                Instantiate(coolVfx, transform.position + new Vector3(0f, 0.5f, 0f), Quaternion.identity,
                    gameObject.transform);
                m_ABuilding.tileHandling.resourceBarManager.AddMushLog(amountOfGoods * 3);
                break;
            case 2:
                Instantiate(coolVfx, transform.position + new Vector3(0f, 0.5f, 0f), Quaternion.identity,
                    gameObject.transform);
                m_ABuilding.tileHandling.resourceBarManager.AddMushLog(amountOfGoods * 8);
                break;
        }


        
    }


    public void GenerateMissingPlantFields()
    {
        
    }

    
    public void DestroyAllPlantFields()
    {
        
    }

 
}
