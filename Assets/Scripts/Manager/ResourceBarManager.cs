using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class ResourceBarManager : MonoBehaviour
{
    private TileHandling m_TileHandling;
    private Text m_MushLog, m_Soul, m_Food;

    private void Start()
    {
        m_TileHandling = GetComponent<TileHandling>();
        m_MushLog = m_TileHandling.canvasComponents.mushLogText;
        m_Soul = m_TileHandling.canvasComponents.soulsText;
        m_Food = m_TileHandling.canvasComponents.foodText;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            AddMushLog(5);
            AddSouls(5);
            AddFood(5);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            SubtractMushLog(5);
            SubtractSouls(5);
            SubtractFood(5);
        }
    }

    #region Get

    public int GetMushLogAmount() => int.Parse(m_MushLog.text);
    public int GetSoulAmount() => int.Parse(m_Soul.text);
    public int GetFoodAmount() => int.Parse(m_Food.text);

    #endregion


    #region Add

    public void AddMushLog(int amount)
    {
        var newAmount = int.Parse(m_MushLog.text) + Math.Abs(amount);
        m_MushLog.text = newAmount.ToString();
    }

    public void AddSouls(int amount)
    {
        var newAmount = int.Parse(m_Soul.text) + Math.Abs(amount);
        m_Soul.text = newAmount.ToString();
    }

    public void AddFood(int amount)
    {
        var newAmount = int.Parse(m_Food.text) + Math.Abs(amount);
        m_Food.text = newAmount.ToString();
    }
    
    public void AddAll(int mushLogAmount, int soulAmount, int foodAmount)
    {
        var newMushLogAmount = int.Parse(m_MushLog.text) + Math.Abs(mushLogAmount);
        m_MushLog.text = newMushLogAmount.ToString();
        
        var newSoulAmount = int.Parse(m_Soul.text) + Math.Abs(soulAmount);
        m_Soul.text = newSoulAmount.ToString();
        
        var newFoodAmount = int.Parse(m_Food.text) + Math.Abs(foodAmount);
        m_Food.text = newFoodAmount.ToString();
    }
    #endregion


    #region Subtract

    public void SubtractMushLog(int amount)
    {
        var newAmount = int.Parse(m_MushLog.text) - Math.Abs(amount);
        m_MushLog.text = newAmount.ToString();
    }

    public void SubtractSouls(int amount)
    {
        var newAmount = int.Parse(m_Soul.text) - Math.Abs(amount);
        m_Soul.text = newAmount.ToString();
    }

    public void SubtractFood(int amount)
    {
        var newAmount = int.Parse(m_Food.text) - Math.Abs(amount);
        m_Food.text = newAmount.ToString();
    }
    
    public void SubtractAll(int mushLogAmount, int soulAmount, int foodAmount)
    {
        var newMushLogAmount = int.Parse(m_MushLog.text) - Math.Abs(mushLogAmount);
        m_MushLog.text = newMushLogAmount.ToString();
        
        var newSoulAmount = int.Parse(m_Soul.text) - Math.Abs(soulAmount);
        m_Soul.text = newSoulAmount.ToString();
        
        var newFoodAmount = int.Parse(m_Food.text) - Math.Abs(foodAmount);
        m_Food.text = newFoodAmount.ToString();
    }
    
    #endregion


    #region Convert

    #endregion
}