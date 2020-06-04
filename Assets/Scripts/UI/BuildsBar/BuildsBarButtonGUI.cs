using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildsBarButtonGUI : MonoBehaviour
{
    public GameObject tilePrefab;

    private SelectTile selectTile;


    public void SetTileProperties(GameObject tilePrefabGo)
    {
        selectTile = GameObject.Find("GameManager").GetComponent<SelectTile>();
        tilePrefab = tilePrefabGo;

        SetIcon();

        SetOnClickFunction();
    }


    private void SetIcon()
    {
        transform.GetChild(0).gameObject.GetComponent<Image>().sprite = tilePrefab.GetComponent<SpriteRenderer>().sprite;
    }


    private void SetOnClickFunction()
    {
        var eventTrigger = GetComponent<EventTrigger>();
        var onPointerClickEntry = new EventTrigger.Entry();
        var onPointerEnterEntry = new EventTrigger.Entry();
        var onPointerExitEntry = new EventTrigger.Entry();

        onPointerEnterEntry.eventID = EventTriggerType.PointerEnter;
        onPointerEnterEntry.callback.AddListener((eventData) => { selectTile.Invoke("Get" + tilePrefab.name + "_InfoCosts", 0f); });
        eventTrigger.triggers.Add(onPointerEnterEntry);

        onPointerExitEntry.eventID = EventTriggerType.PointerExit;
        onPointerExitEntry.callback.AddListener((eventData) => { selectTile.Invoke("EmptyAndClosePrefabInfo", 0f); });
        eventTrigger.triggers.Add(onPointerExitEntry);

        onPointerClickEntry.eventID = EventTriggerType.PointerClick;
        onPointerClickEntry.callback.AddListener((eventData) => { selectTile.Invoke("Get" + tilePrefab.name, 0f); });
        eventTrigger.triggers.Add(onPointerClickEntry);
    }
}
