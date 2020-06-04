using UnityEngine;
using UnityEngine.EventSystems;

public class TileActionButtonGUI : MonoBehaviour
{
    private GameObject selectedBuilding;
    private string actionName, selectedScriptName;

    public void SetTileProperties(GameObject selectedBuildingTile, string selectedBuildingScriptName, string actionNamesPassed)
    {
        selectedBuilding = selectedBuildingTile;
        selectedScriptName = selectedBuildingScriptName;
        actionName = actionNamesPassed;
        
        SetActions();
    }

    
    private void SetActions()
    {
        var eventTrigger = GetComponent<EventTrigger>();
        var onPointerClickEntry = new EventTrigger.Entry {eventID = EventTriggerType.PointerClick};

        onPointerClickEntry.callback.AddListener((eventData) => { selectedBuilding.GetComponent(selectedScriptName).SendMessage(actionName); });
        eventTrigger.triggers.Add(onPointerClickEntry);
    }
}
