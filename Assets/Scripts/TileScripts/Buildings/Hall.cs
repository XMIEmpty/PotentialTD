using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hall : MonoBehaviour, IPassMethods
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
                        return nameof(ConvertMushLogsToSouls);
                    case 1:
                        return nameof(ConvertSoulsToFood);
                    case 2:
                        return nameof(ConvertFoodToMushLogs);
                    case 3:
                        return "";
                }
                
                break;
            
            case 1:
                
                switch (methodNum)
                {
                    case 0:
                        return nameof(ConvertMushLogsToSouls);
                    case 1:
                        return nameof(ConvertSoulsToFood);
                    case 2:
                        return nameof(ConvertFoodToMushLogs);
                    case 3:
                        return "";
                }
                
                break;
            case 2:
                
                switch (methodNum)
                {
                    case 0:
                        return nameof(ConvertMushLogsToSouls);
                    case 1:
                        return nameof(ConvertSoulsToFood);
                    case 2:
                        return nameof(ConvertFoodToMushLogs);
                    case 3:
                        return "";
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
                    case 0:
                        return new Vector3Int(2500, 0, 0);
                    case 1:
                        return new Vector3Int(0, 50, 0);
                    case 2:
                        return new Vector3Int(0, 0, 1);
                    case 3:
                        return Vector3Int.zero;
                }

                break;
            case 1: 
                
                switch (methodNum)
                {
                    case 0:
                        return new Vector3Int(250000, 0, 0);
                    case 1:
                        return new Vector3Int(0, 5000, 0);
                    case 2:
                        return new Vector3Int(0, 0, 100);
                    case 3:
                        return Vector3Int.zero;
                }
                
                break;
            case 2: 
                
                switch (methodNum)
                {
                    case 0:
                        return new Vector3Int(25000000, 0, 0);
                    case 1:
                        return new Vector3Int(0, 500000, 0);
                    case 2:
                        return new Vector3Int(0, 0, 10000);
                    case 3:
                        return Vector3Int.zero;
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
                    case 0:
                        return "Trades 2500 MushLogs for 25 Souls";
                    case 1:
                        return "Trades 50 Souls for 1 Food";
                    case 2:
                        return "Trades 1 Food for 4000 MushLogs";
                    case 3:
                        return "";
                }

                break;
            case 1:
                
                switch (methodNum)
                {
                    case 0:
                        return "Trades 250000 MushLogs for 2500 Souls";
                    case 1:
                        return "Trades 5000 Souls for 100 Food";
                    case 2:
                        return "Trades 100 Food for 400000 MushLogs";
                    case 3:
                        return "";
                }

                break;
            case 2:
                
                switch (methodNum)
                {
                    case 0:
                        return "Trades 25000000 MushLogs for 250000 Souls";
                    case 1:
                        return "Trades 500000 Souls for 10000 Food";
                    case 2:
                        return "Trades 10000 Food for 40000000 MushLogs";
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


    private A_Building m_ABuilding;


    private void Start()
    {
        if (!TryGetComponent<A_Building>(out var buildingFound)) return;
        m_ABuilding = buildingFound;
    }

    
    public void ConvertMushLogsToSouls()
    {
        if (m_ABuilding.currentUpgradeLevel == 0) m_ABuilding.tileHandling.resourceBarManager.AddSouls(25);
        if (m_ABuilding.currentUpgradeLevel == 1) m_ABuilding.tileHandling.resourceBarManager.AddSouls(2500);
        if (m_ABuilding.currentUpgradeLevel == 2) m_ABuilding.tileHandling.resourceBarManager.AddSouls(250000);
        
    }

    
    public void ConvertSoulsToFood()
    {
        if (m_ABuilding.currentUpgradeLevel == 0) m_ABuilding.tileHandling.resourceBarManager.AddFood(1);
        if (m_ABuilding.currentUpgradeLevel == 1) m_ABuilding.tileHandling.resourceBarManager.AddFood(100);
        if (m_ABuilding.currentUpgradeLevel == 2) m_ABuilding.tileHandling.resourceBarManager.AddFood(10000);
    }
    
    
    public void ConvertFoodToMushLogs()
    {
        if (m_ABuilding.currentUpgradeLevel == 0) m_ABuilding.tileHandling.resourceBarManager.AddMushLog(4000);
        if (m_ABuilding.currentUpgradeLevel == 1) m_ABuilding.tileHandling.resourceBarManager.AddMushLog(400000);
        if (m_ABuilding.currentUpgradeLevel == 2) m_ABuilding.tileHandling.resourceBarManager.AddMushLog(40000000);
    }
}