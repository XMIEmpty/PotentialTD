using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildsBarControlGUI : MonoBehaviour
{
    private TileHandling m_tilesHandler;
    [SerializeField] private GameObject buttonTemplate;
    [SerializeField] private GridLayoutGroup gridGroup;

    private List<ActionItem> m_ActionInventory;
    // private TileHandling m_TileHandling;


    private void Start()
    {
        m_tilesHandler = GameObject.Find("GameManager").GetComponent<TileHandling>();
    }


    private void Update()
    {
        StartsIs();
    }


    private void StartsIs()
    {
        if (!Input.GetKeyDown(KeyCode.O)) return;
        m_ActionInventory = new List<ActionItem>();

        for (var i = m_tilesHandler.prefabTileContainer.containers.Length - 1; i >= 0; i--)
        {
            for (var j = m_tilesHandler.prefabTileContainer.containers[i].containerItems.Length - 1; j >= 0; j--)
            {
                // Run thru all items in containers
                var newItem = new ActionItem
                {
                    TilePrefab = m_tilesHandler.prefabTileContainer.containers[i].containerItems[j] // Pass prefab
                };
                m_ActionInventory.Add(newItem);
            }
        }

        GenInventory();
    }


    void GenInventory()
    {    
        // If there are more children to (GameObject)Content except the first one
        if (buttonTemplate.transform.parent.childCount > 1) // Destroy Old List
        {
            // Run thru all children of (GameObject)Content except first the one
            for (var index = buttonTemplate.transform.parent.childCount - 1; index >= 1; index--)
            {
                // Destroy the found child
                Destroy(buttonTemplate.transform.parent.GetChild(index).gameObject);
            }
        }

        gridGroup.constraintCount =
            m_ActionInventory.Count < 11 ? m_ActionInventory.Count : 10; // Reset Constraint Bounds

        
        for (var index = m_ActionInventory.Count - 1; index >= 0; index--)
        {
            var newItem = m_ActionInventory[index];
            var newButton =
                Instantiate(buttonTemplate, buttonTemplate.transform.parent,
                    false); // Make Button, Set Proper Parenting, Pos
            newButton.SetActive(true);

            var buildsBarButton = newButton.GetComponent<BuildsBarButtonGUI>();

            buildsBarButton.SetTileProperties(newItem.TilePrefab); // Pass the Prefab in the Button Script
        }
    }


    private class ActionItem
    {
        public GameObject TilePrefab;
    }
}