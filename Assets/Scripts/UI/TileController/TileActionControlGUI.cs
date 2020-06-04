using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileActionControlGUI : MonoBehaviour
{
    [SerializeField] private TileHandling tilesHandler;
    [SerializeField] private GameObject buttonTemplate;
    [SerializeField] private GridLayoutGroup gridGroup;

    private List<ActionItem> m_ActionInventory;


    private void Awake()
    {
        tilesHandler = GameObject.Find("GameManager").GetComponent<TileHandling>();
    }


    public void CreateButtonList()
    {
        if (!tilesHandler.selectedUnit.TryGetComponent(out IPassMethods passMethod)) return;

        // Debug.Log(IPassMethods);

        Debug.Log("Creating TILE Button List");
        m_ActionInventory = new List<ActionItem>();

        for (var i = 0; i < 4; i++)
        {
            if (string.IsNullOrEmpty(passMethod.PassMethodName(i))) continue;

            var newItem = new ActionItem();
            newItem.ScriptName = passMethod.GetScriptName();
            newItem.ActionName = passMethod.PassMethodName(i);
            // newItem.ActionNames.Add(passMethod.PassMethodName(i));
            
            m_ActionInventory.Add(newItem);
        }

        GenInventory();
    }


    private void GenInventory()
    {
        if (tilesHandler.canvasComponents.tileContent.transform.childCount > 1) // Destroy Old List
        {// If there are more children to (GameObject)Content except the first one
            for (var index = tilesHandler.canvasComponents.tileContent.transform.childCount - 1; index >= 1; index--)
            {// Run thru all children of (GameObject)Content except first the one
                Destroy(tilesHandler.canvasComponents.tileContent.transform.GetChild(index).gameObject);
                // Destroy the found child
            }
        }

        gridGroup.constraintCount =
            m_ActionInventory.Count < 3 ? m_ActionInventory.Count : 2; // Reset Constraint Bounds 

        for (var index = m_ActionInventory.Count - 1; index >= 0; index--)
        {
            var newActionInvItem = m_ActionInventory[index];
            var newButton =
                Instantiate(buttonTemplate, buttonTemplate.transform.parent,
                    false); // Make Button, Set Proper Parenting, Pos
            newButton.SetActive(true);

            var tileActionButtonGui = newButton.GetComponent<TileActionButtonGUI>();

            tileActionButtonGui.SetTileProperties(tilesHandler.selectedUnit, newActionInvItem.ScriptName, newActionInvItem.ActionName);
            // 
        }
    }


    private class ActionItem
    {
        public string ScriptName;
        public string ActionName;
    }
}