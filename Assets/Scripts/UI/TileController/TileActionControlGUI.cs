using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileActionControlGUI : MonoBehaviour
{
    [SerializeField] private TileHandling tilesHandler;
    [SerializeField] private GameObject buttonTemplate;
    [SerializeField] private GridLayoutGroup gridGroup;

    private List<ActionItem> m_ActionInventory;

    private A_Building m_ABuilding;

    private void Awake()
    {
        tilesHandler = GameObject.Find("GameManager").GetComponent<TileHandling>();
    }


    public void CreateButtonList()
    {
        SetUpDefaultValues();
        
        // If SelectedUnit contains IPassMethods interface
        if (!tilesHandler.selectedUnit.TryGetComponent(out IPassMethods passMethod)) return;

        m_ActionInventory = new List<ActionItem>();

        #region Create Only 4 Button and Set Values
        // Run 4 Times (for 4 Tile controller buttons)
        for (var i = 0; i < 4; i++)
        {
            // If current Custom-Method case is empty/null skip it 
            if (string.IsNullOrEmpty(passMethod.PassMethodName(i))) continue;

            // Create new ActionItem instance and pass it the current Custom-Method on the desired interaction method
            var newItem = new ActionItem();
            newItem.ScriptName = passMethod.GetScriptName();
            newItem.ClickActionName = passMethod.PassMethodName(i);
            
            // Finally add the ActionItem to the List with the others
            // (This represents the complete button containing all data it needs to contain)
            m_ActionInventory.Add(newItem);
        }

        GenInventory();
        #endregion
    }


    private void GenInventory()
    {
        #region Destroy Old Button List
        // If there are more children to (GameObject)Content except the first one
        if (tilesHandler.canvasComponents.tileContent.transform.childCount > 1)
        {
            // Run thru all children of (GameObject)Content except first the one
            for (var index = tilesHandler.canvasComponents.tileContent.transform.childCount - 1; index >= 1; index--)
            {
                // Destroy the found child
                Destroy(tilesHandler.canvasComponents.tileContent.transform.GetChild(index).gameObject);
            }
        }
        #endregion

        gridGroup.constraintCount =
            m_ActionInventory.Count < 3 ? m_ActionInventory.Count : 2; // Reset Constraint Bounds 

        #region Create New Button List & Pass Values
        // Run thru all items added to ActionInventory
        for (var index = m_ActionInventory.Count - 1; index >= 0; index--)
        {
            // Create New ActionInvItem, Create GO, instantiate Button and set it equal to Created GO, Active = true
            var newActionInvItem = m_ActionInventory[index];
            var newButton =
                Instantiate(buttonTemplate, buttonTemplate.transform.parent,
                    false);
            newButton.SetActive(true);

            // Get the ActionBtn Script from created GO(Button)
            var tileActionButtonGui = newButton.GetComponent<TileActionButtonGUI>(); 

            // Pass into the script all values necessary 
            tileActionButtonGui.SetOnPointerClickActionName(newActionInvItem.ClickActionName);
            tileActionButtonGui.SetTileMainProperties(tilesHandler.selectedUnit, newActionInvItem.ScriptName);
        }
        #endregion
    }


    private void SetUpDefaultValues()
    {
        //m_ABuilding
    }
    

    private class ActionItem
    {
        public string ScriptName;

        #region Action Names
        public string ClickActionName;
        // public string EnterctionName;
        // public string ExitActionName;
        // public string DownActionName;
        // public string UpActionName;
        // public string BeginDragActionName;
        // public string InitializePotentialDragActionName;
        // public string MoveActionName;
        // public string EndDragActionName;
        // public string DropActionName;
        // public string SelectActionName;
        // public string UpdateSelectedActionName;
        // public string DeselectActionName;
        // public string ScrollActionName;
        // public string CancelActionName;
        #endregion
    }
}