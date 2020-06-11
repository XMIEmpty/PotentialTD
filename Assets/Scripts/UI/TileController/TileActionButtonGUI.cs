using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TileActionButtonGUI : MonoBehaviour
{
    private GameObject selectedBuilding;
    private string selectedScriptName;
    private TileHandling tileHandling;
    private Vector3Int actionCosts;
    private string actionInfo;
    

    #region Create variables for all possible Interaction types
    private string pointerClickAction, pointerEnterAction, pointerExitAction, pointerDownAction, pointerUpAction;
    private string pointerBeginDragAction, pointerInitializePotentialDragAction, pointerMoveAction;
    private string pointerEndDragAction, pointerDropAction, pointerSelectAction, pointerUpdateSelectedAction;
    private string pointerDeselectAction, pointerScrollAction, pointerCancelAction;
    private static readonly int CloseInfoBox = Animator.StringToHash("Close_InfoBox");
    private static readonly int OpenInfoBox = Animator.StringToHash("Open_InfoBox");

    #endregion


    private void Start()
    {
        tileHandling = GameObject.Find("GameManager").GetComponent<TileHandling>();
    }


    public void SetTileMainProperties(GameObject selectedBuildingTile, string selectedBuildingScriptName)
    {
        // Set the passed values to the Class's equivalent values
        selectedBuilding = selectedBuildingTile;
        selectedScriptName = selectedBuildingScriptName;

        SetButtonText();
        SetActions();
    }
    
    private void SetButtonText()
    {
        transform.GetChild(0).gameObject.GetComponent<Text>().text = Regex.Replace(pointerClickAction, "(\\B[A-Z])", " $1");
    }

    public void PassMethodCostsAndInfo(Vector3Int costs, string info)
    {
        actionCosts = costs;
        actionInfo = info;
    }


    #region Only for External Call from Tile-Action-Controller Figure
    public void SetOnPointerClickActionName(string actionName) => pointerClickAction = actionName;
    // public void SetOnPointerEnterActionName(string actionName) => pointerEnterAction = actionName;
    // public void SetOnPointerExitActionName(string actionName) => pointerExitAction = actionName;
    // public void SetOnPointerDownActionName(string actionName) => pointerDownAction = actionName;
    // public void SetOnPointerUpActionName(string actionName) => pointerUpAction = actionName;
    // public void SetOnPointerBeginDragActionName(string actionName) => pointerBeginDragAction = actionName;
    // public void SetOnPointerInitializePotentialDragActionName(string actionName) => pointerInitializePotentialDragAction = actionName;
    // public void SetOnPointerMoveActionName(string actionName) => pointerMoveAction = actionName;
    // public void SetOnPointerEndDragActionName(string actionName) => pointerEndDragAction = actionName;
    // public void SetOnPointerDropActionName(string actionName) => pointerDropAction = actionName;
    // public void SetOnPointerSelectActionName(string actionName) => pointerSelectAction = actionName;
    // public void SetOnPointerUpdateSelectedActionName(string actionName) => pointerUpdateSelectedAction = actionName;
    // public void SetOnPointerDeselectActionName(string actionName) => pointerDeselectAction = actionName;
    // public void SetOnPointerScrollActionName(string actionName) => pointerScrollAction = actionName;
    // public void SetOnPointerCancelActionName(string actionName) => pointerCancelAction = actionName;
    #endregion


    private void SetActionButtonCostsAndInfo()
    {
        tileHandling.canvasComponents.infoBoxText.text = actionInfo;
        tileHandling.canvasComponents.infoBoxGo.GetComponent<Animator>().SetTrigger(OpenInfoBox);
        ApplyCosts();
        tileHandling.canvasComponents.costsBoxGo.SetActive(true);
    }


    private void ApplyCosts()
    {
        tileHandling.canvasComponents.mushLogCostsText.text = actionCosts.x.ToString(); // MushLog
        tileHandling.canvasComponents.soulCostsText.text = actionCosts.y.ToString(); // Soul
        tileHandling.canvasComponents.foodCostsText.text = actionCosts.z.ToString(); // Food
    }
    
    private void EmptyAndCloseCostsAndInfoBox()
    {
        tileHandling.canvasComponents.infoBoxText.text = "";
        tileHandling.canvasComponents.infoBoxGo.GetComponent<Animator>().SetTrigger(CloseInfoBox);
        // Empty Costs Values and Close that as well
        tileHandling.canvasComponents.costsBoxGo.SetActive(false);
    }
    
    
    /// <summary>
    /// Add the appropriate (based on settings) Events to the Event Trigger component
    /// Set up the Events with the appropriately added methods 
    /// </summary>
    private void SetActions()
    {
        // Get Event Trigger and Pass it to a variable 
        var eventTrigger = GetComponent<EventTrigger>();
        
        // Create variables for all possible Entry Types and pass them the appropriate entry type
        var onPointerClickEntry = new EventTrigger.Entry {eventID = EventTriggerType.PointerClick};
        var onPointerEnterEntry = new EventTrigger.Entry {eventID = EventTriggerType.PointerEnter};
        var onPointerExitEntry = new EventTrigger.Entry {eventID = EventTriggerType.PointerExit};
        // var onPointerDownEntry = new EventTrigger.Entry {eventID = EventTriggerType.PointerDown};
        // var onPointerUpEntry = new EventTrigger.Entry {eventID = EventTriggerType.PointerUp};
        // var onPointerBeginDragEntry = new EventTrigger.Entry {eventID = EventTriggerType.BeginDrag};
        // var onPointerInitializePotentialDragEntry = new EventTrigger.Entry {eventID = EventTriggerType.InitializePotentialDrag};
        // var onPointerMoveEntry = new EventTrigger.Entry {eventID = EventTriggerType.Move};
        // var onPointerEndDragEntry = new EventTrigger.Entry {eventID = EventTriggerType.EndDrag};
        // var onPointerDropEntry = new EventTrigger.Entry {eventID = EventTriggerType.Drop};
        // var onPointerSelectEntry = new EventTrigger.Entry {eventID = EventTriggerType.Select};
        // var onPointerUpdateSelectedEntry = new EventTrigger.Entry {eventID = EventTriggerType.UpdateSelected};
        // var onPointerDeselectEntry = new EventTrigger.Entry {eventID = EventTriggerType.Deselect};
        // var onPointerScrollEntry = new EventTrigger.Entry {eventID = EventTriggerType.Scroll};
        // var onPointerCancelEntry = new EventTrigger.Entry {eventID = EventTriggerType.Cancel};

        // Check if the action string is NOT null/empty
        if (!string.IsNullOrEmpty(pointerClickAction)) // On CLICK
        {
            // Add to the appropriate entry's callback a Listener with all the actions(methods) it oughts to call    
            onPointerClickEntry.callback.AddListener((eventData) => {
                selectedBuilding.GetComponent(selectedScriptName).SendMessage(pointerClickAction);
                tileHandling.resourceBarManager.SubtractAll(
                    actionCosts.x, actionCosts.y, actionCosts.z); });
            // Add the Entry to the event trigger list
            eventTrigger.triggers.Add(onPointerClickEntry);
        }

        if (!string.IsNullOrEmpty(pointerClickAction)) // On Pointer Enter
        {
            onPointerEnterEntry.callback.AddListener((eventData) => { SetActionButtonCostsAndInfo(); });
            eventTrigger.triggers.Add(onPointerEnterEntry);
        }
        
        if (!string.IsNullOrEmpty(pointerClickAction)) // On Pointer Exit
        {
            onPointerExitEntry.callback.AddListener((eventData) => { EmptyAndCloseCostsAndInfoBox();});
            eventTrigger.triggers.Add(onPointerExitEntry);
        }
        //
        // if (!string.IsNullOrEmpty(pointerDownAction)) // On Pointer Down
        // {
        //     onPointerDownEntry.callback.AddListener((eventData) => {
        //         selectedBuilding.GetComponent(selectedScriptName).SendMessage(pointerDownAction); });
        //     eventTrigger.triggers.Add(onPointerDownEntry);
        // }
        //
        // if (!string.IsNullOrEmpty(pointerUpAction)) // On Pointer Up
        // {
        //     onPointerUpEntry.callback.AddListener((eventData) => {
        //         selectedBuilding.GetComponent(selectedScriptName).SendMessage(pointerUpAction); });
        //     eventTrigger.triggers.Add(onPointerUpEntry);
        // }
        //
        //
        // if (!string.IsNullOrEmpty(pointerBeginDragAction)) // On Pointer BeginDrag
        // {
        //     onPointerBeginDragEntry.callback.AddListener((eventData) => {
        //         selectedBuilding.GetComponent(selectedScriptName).SendMessage(pointerBeginDragAction); });
        //     eventTrigger.triggers.Add(onPointerBeginDragEntry);
        // }
        //
        // if (!string.IsNullOrEmpty(pointerInitializePotentialDragAction)) // On Pointer InitializePotentialDrag
        // {
        //     onPointerInitializePotentialDragEntry.callback.AddListener((eventData) => {
        //         selectedBuilding.GetComponent(selectedScriptName).SendMessage(pointerInitializePotentialDragAction); });
        //     eventTrigger.triggers.Add(onPointerInitializePotentialDragEntry);
        // }
        //
        // if (!string.IsNullOrEmpty(pointerMoveAction)) // On Pointer Move
        // {
        //     onPointerMoveEntry.callback.AddListener((eventData) => {
        //         selectedBuilding.GetComponent(selectedScriptName).SendMessage(pointerMoveAction); });
        //     eventTrigger.triggers.Add(onPointerMoveEntry);
        // }
        //
        // if (!string.IsNullOrEmpty(pointerEndDragAction)) // On Pointer EndDrag
        // {
        //     onPointerEndDragEntry.callback.AddListener((eventData) => {
        //         selectedBuilding.GetComponent(selectedScriptName).SendMessage(pointerEndDragAction); });
        //     eventTrigger.triggers.Add(onPointerEndDragEntry);
        // }
        //
        // if (!string.IsNullOrEmpty(pointerDropAction)) // On Pointer Drop
        // {
        //     onPointerDropEntry.callback.AddListener((eventData) => {
        //         selectedBuilding.GetComponent(selectedScriptName).SendMessage(pointerDropAction); });
        //     eventTrigger.triggers.Add(onPointerDropEntry);
        // }
        //
        // if (!string.IsNullOrEmpty(pointerSelectAction)) // On Pointer Select
        // {
        //     onPointerSelectEntry.callback.AddListener((eventData) => {
        //         selectedBuilding.GetComponent(selectedScriptName).SendMessage(pointerSelectAction); });
        //     eventTrigger.triggers.Add(onPointerSelectEntry);
        // }
        //
        // if (!string.IsNullOrEmpty(pointerUpdateSelectedAction)) // On Pointer UpdateSelected
        // {
        //     onPointerUpdateSelectedEntry.callback.AddListener((eventData) => {
        //         selectedBuilding.GetComponent(selectedScriptName).SendMessage(pointerUpdateSelectedAction); });
        //     eventTrigger.triggers.Add(onPointerUpdateSelectedEntry);
        // }
        //
        // if (!string.IsNullOrEmpty(pointerDeselectAction)) // On Pointer Deselect
        // {
        //     onPointerDeselectEntry.callback.AddListener((eventData) => {
        //         selectedBuilding.GetComponent(selectedScriptName).SendMessage(pointerDeselectAction); });
        //     eventTrigger.triggers.Add(onPointerDeselectEntry);
        // }
        //
        // if (!string.IsNullOrEmpty(pointerScrollAction)) // On Pointer Scroll
        // {
        //     onPointerScrollEntry.callback.AddListener((eventData) => {
        //         selectedBuilding.GetComponent(selectedScriptName).SendMessage(pointerScrollAction); });
        //     eventTrigger.triggers.Add(onPointerScrollEntry);
        // }
        //
        // if (!string.IsNullOrEmpty(pointerCancelAction)) // On Pointer Cancel
        // {
        //     onPointerCancelEntry.callback.AddListener((eventData) => {
        //         selectedBuilding.GetComponent(selectedScriptName).SendMessage(pointerCancelAction); });
        //     eventTrigger.triggers.Add(onPointerCancelEntry);
        // }
    }
}
