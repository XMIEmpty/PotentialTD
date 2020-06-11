using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildsBarButtonGUI : MonoBehaviour
{
    public GameObject tilePrefab;

    private SelectTile selectTile;

    private TileHandling tileHandling;

    
    private void Start()
    {
        tileHandling = GameObject.Find("GameManager").GetComponent<TileHandling>();
    }

    
    public void SetTileProperties(GameObject tilePrefabGo)
    {
        selectTile = GameObject.Find("GameManager").GetComponent<SelectTile>();
        tilePrefab = tilePrefabGo;

        SetIcon();

        SetOnClickFunction();
    }


    private void SetIcon()
    {
        GetComponent<Image>().sprite = tilePrefab.GetComponent<SpriteRenderer>().sprite;
    }


    private void SetOnClickFunction()
    {
        var eventTrigger = GetComponent<EventTrigger>();
        var onPointerClickEntry = new EventTrigger.Entry();
        var onPointerEnterEntry = new EventTrigger.Entry();
        var onPointerExitEntry = new EventTrigger.Entry();

        onPointerEnterEntry.eventID = EventTriggerType.PointerEnter;
        onPointerEnterEntry.callback.AddListener((eventData) =>
        {
            selectTile.Invoke("Get" + tilePrefab.name + "_InfoCosts", 0f);
            CheckPrices();
        });
        eventTrigger.triggers.Add(onPointerEnterEntry);

        onPointerExitEntry.eventID = EventTriggerType.PointerExit;
        onPointerExitEntry.callback.AddListener((eventData) =>
        {
            selectTile.Invoke("EmptyAndClosePrefabInfo", 0f);
            ResetValuesOnPointerExit();
        });
        eventTrigger.triggers.Add(onPointerExitEntry);

        onPointerClickEntry.eventID = EventTriggerType.PointerClick;
        onPointerClickEntry.callback.AddListener((eventData) => { selectTile.Invoke("Get" + tilePrefab.name, 0f); });
        eventTrigger.triggers.Add(onPointerClickEntry);
    }

    
    private void CheckPrices()
    {
        if (tileHandling.resourceBarManager.GetMushLogAmount() < tilePrefab.GetComponent<A_Building>().mushLogCosts)
        {
            tileHandling.canvasComponents.mushLogCostsText.GetComponent<Text>().color = new Color(0.94f, 0.26f, 0.18f);
        }

        if (tileHandling.resourceBarManager.GetSoulAmount() < tilePrefab.GetComponent<A_Building>().soulCosts)
        {
            tileHandling.canvasComponents.soulCostsText.GetComponent<Text>().color = new Color(0.94f, 0.26f, 0.18f);
        }
        
        if  (tileHandling.resourceBarManager.GetFoodAmount() < tilePrefab.GetComponent<A_Building>().foodCosts)
        {
            tileHandling.canvasComponents.foodCostsText.GetComponent<Text>().color = new Color(0.94f, 0.26f, 0.18f);
        }

        if (tileHandling.resourceBarManager.GetMushLogAmount() >= tilePrefab.GetComponent<A_Building>().mushLogCosts &&
            tileHandling.resourceBarManager.GetSoulAmount() >= tilePrefab.GetComponent<A_Building>().soulCosts &&
            tileHandling.resourceBarManager.GetFoodAmount() >= tilePrefab.GetComponent<A_Building>().foodCosts) return;
        
        var eventTrigger = GetComponent<EventTrigger>();
        for (var i = eventTrigger.triggers.Count - 1; i >= 0; i--)
        {
            if (eventTrigger.triggers[i].eventID != EventTriggerType.PointerClick) continue;
            eventTrigger.triggers[i].callback.RemoveAllListeners();
        }
    }

    
    public void ResetValuesOnPointerExit()
    {
        tileHandling.canvasComponents.mushLogCostsText.GetComponent<Text>().color = Color.white;
        tileHandling.canvasComponents.soulCostsText.GetComponent<Text>().color = Color.white;
        tileHandling.canvasComponents.foodCostsText.GetComponent<Text>().color = Color.white;

        var eventTrigger = GetComponent<EventTrigger>();
        var onPointerClickEntry = new EventTrigger.Entry();
        onPointerClickEntry.eventID = EventTriggerType.PointerClick;
        onPointerClickEntry.callback.AddListener((eventData) => { selectTile.Invoke("Get" + tilePrefab.name, 0f); });
        eventTrigger.triggers.Add(onPointerClickEntry);
    }
}
