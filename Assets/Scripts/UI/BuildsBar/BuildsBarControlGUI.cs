using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BuildsBarControlGUI : MonoBehaviour
{
    [SerializeField] private TileHandling tilesHandler;
    [SerializeField] private GameObject buttonTemplate;
    [SerializeField] private GridLayoutGroup gridGroup;

    private List<ActionItem> ActionInventory;
    private List<GameObject> buttons;


    private void Start()
    {
        tilesHandler = GameObject.Find("GameManager").GetComponent<TileHandling>();
        buttons = new List<GameObject>();
        ActionInventory = new List<ActionItem>();

        for (int i = 0; i < tilesHandler.prefabTileContainer.containers.Length; i++)
        {
            for (int j = 0; j < tilesHandler.prefabTileContainer.containers[i].containerItems.Length; j++)
            {// Run thru all items in containers
                ActionItem newItem = new ActionItem();
                newItem.tilePrefab = tilesHandler.prefabTileContainer.containers[i].containerItems[j]; // Pass prefab
                ActionInventory.Add(newItem);
            }
        }
        GenInventory();
    }


    void GenInventory()
    {
        if (buttons.Count > 0) // Destroy Old List
        {
            foreach (GameObject button in buttons) Destroy(button.gameObject);
            buttons.Clear();
        }

        if (ActionInventory.Count < 11) gridGroup.constraintCount = ActionInventory.Count;
        else gridGroup.constraintCount = 10; // Reset Contraint Bounds

        foreach (ActionItem newItem in ActionInventory) // Go thru the list of all tile Prefabs found in ActionInventory List
        {
            GameObject newButton = Instantiate(buttonTemplate) as GameObject; // Make Button
            newButton.SetActive(true);
            BuildsBarButtonGUI buildsBarButton = newButton.GetComponent<BuildsBarButtonGUI>();

            buildsBarButton.SetTileProperties(newItem.tilePrefab); // Pass the Prefab in the Button Script

            newButton.transform.SetParent(buttonTemplate.transform.parent, false); // Set Proper Parenting
            buttons.Add(newButton);
        }
    }


    public class ActionItem
    {
        public GameObject tilePrefab;
    }
}
