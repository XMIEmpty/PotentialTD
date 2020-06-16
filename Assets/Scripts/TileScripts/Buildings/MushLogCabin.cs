using UnityEngine;

public class MushLogCabin : MonoBehaviour, IPassMethods, IBuildingExtras
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
            
            case 3:
                
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
                        return "Gathers 6000 MushLogs and 250 Souls on Click";
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
                        return "Gathers 90000 MushLogs and 3750 Souls on Click";
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
                        return "Gathers 720000 MushLogs, 30000 Souls and 600 Food on Click";
                    case 1:
                        return "";
                    case 2:
                        return "";
                    case 3:
                        return "";
                }
                
                break;
            
            case 3:
                
                switch (methodNum)
                {
                    case 0:
                        return "Gathers 10800000 MushLogs, 450000 Souls and 6000 Food on Click";
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
        
    }


    private A_Building m_ABuilding;

    [SerializeField] private GameObject coolVfx;
    [SerializeField] private int mushLogGoods;
    [SerializeField] private int soulGoods;
    [SerializeField] private int constantMushLogGoods;
    [SerializeField] private int constantSoulGoods;
    [SerializeField] private int constantFoodGoods;

    
    private void Start()
    {
        if (!TryGetComponent<A_Building>(out var buildingFound)) return;
        m_ABuilding = buildingFound;
        InvokeRepeating(nameof(ContinuousIncome), 0f, 1f);
    }


    private void ContinuousIncome()
    {
        switch (m_ABuilding.currentUpgradeLevel)
        {
            case 0:
                m_ABuilding.tileHandling.resourceBarManager.AddMushLog(constantMushLogGoods);
                m_ABuilding.tileHandling.resourceBarManager.AddSouls(constantSoulGoods);

                Instantiate(coolVfx, transform.position + new Vector3(0f, 0.5f, 0f), Quaternion.identity,
                    gameObject.transform);
                break;
            case 1:
                m_ABuilding.tileHandling.resourceBarManager.AddMushLog(constantMushLogGoods * 5);
                m_ABuilding.tileHandling.resourceBarManager.AddSouls(constantSoulGoods * 5);
                Instantiate(coolVfx, transform.position + new Vector3(0f, 0.5f, 0f), Quaternion.identity,
                    gameObject.transform);
                break;
            case 2:
                m_ABuilding.tileHandling.resourceBarManager.AddMushLog(constantMushLogGoods * 50);
                m_ABuilding.tileHandling.resourceBarManager.AddSouls(constantSoulGoods * 50);
                m_ABuilding.tileHandling.resourceBarManager.AddFood(constantFoodGoods);
                Instantiate(coolVfx, transform.position + new Vector3(0f, 0.5f, 0f), Quaternion.identity,
                    gameObject.transform);
                break;
            case 3:
                m_ABuilding.tileHandling.resourceBarManager.AddMushLog(constantMushLogGoods * 300);
                m_ABuilding.tileHandling.resourceBarManager.AddSouls(constantSoulGoods * 300);
                m_ABuilding.tileHandling.resourceBarManager.AddFood(constantFoodGoods * 30);
                Instantiate(coolVfx, transform.position + new Vector3(0f, 0.5f, 0f), Quaternion.identity,
                    gameObject.transform);
                break;
        }
    }


    public void GatherGoods()
    {
        switch (m_ABuilding.currentUpgradeLevel)
        {
            case 0:
                Instantiate(coolVfx, transform.position + new Vector3(0f, 0.5f, 0f), Quaternion.identity,
                    gameObject.transform);
                m_ABuilding.tileHandling.resourceBarManager.AddMushLog(mushLogGoods);
                m_ABuilding.tileHandling.resourceBarManager.AddSouls(soulGoods);
                break;
            case 1:
                Instantiate(coolVfx, transform.position + new Vector3(0f, 0.5f, 0f), Quaternion.identity,
                    gameObject.transform);
                m_ABuilding.tileHandling.resourceBarManager.AddMushLog(mushLogGoods * 15);
                m_ABuilding.tileHandling.resourceBarManager.AddSouls(soulGoods * 15);
                break;
            case 2:
                Instantiate(coolVfx, transform.position + new Vector3(0f, 0.5f, 0f), Quaternion.identity,
                    gameObject.transform);
                m_ABuilding.tileHandling.resourceBarManager.AddMushLog(mushLogGoods * 120);
                m_ABuilding.tileHandling.resourceBarManager.AddSouls(soulGoods * 120);
                m_ABuilding.tileHandling.resourceBarManager.AddFood(mushLogGoods / 10);

                break;
            case 3:
                Instantiate(coolVfx, transform.position + new Vector3(0f, 0.5f, 0f), Quaternion.identity,
                    gameObject.transform);
                m_ABuilding.tileHandling.resourceBarManager.AddMushLog(mushLogGoods * 1800);
                m_ABuilding.tileHandling.resourceBarManager.AddSouls(soulGoods * 1800);
                m_ABuilding.tileHandling.resourceBarManager.AddFood(mushLogGoods);
                break;
        }
    }


    public void UpgradeGatheredMushLogs()
    {
        // Increase MushLog carry amount to Base Entity
    }
}
