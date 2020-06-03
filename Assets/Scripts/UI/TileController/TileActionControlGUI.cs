using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileActionControlGUI : MonoBehaviour
{
    [SerializeField] private TileHandling tilesHandler;
    [SerializeField] private GameObject buttonTemplate;
    [SerializeField] private GridLayoutGroup gridGroup;
    [SerializeField] private Sprite[] iconSprites;


    private List<ActionItem> ActionInventory;
    [SerializeField]
    private List<GameObject> buttons;


    private void Awake()
    {
        tilesHandler = GameObject.Find("GameManager").GetComponent<TileHandling>();
        buttons = new List<GameObject>();
        ActionInventory = new List<ActionItem>();
    }


    public void CreateButtonList()
    {
        Debug.Log("Creating TILE Button List");
        for (int i = 0; i < 4; i++)
        {
            ActionItem newItem = new ActionItem();
            newItem.iconSprite = iconSprites[UnityEngine.Random.Range(0, iconSprites.Length)];
            var passMethod = tilesHandler.selectedUnit.GetComponent<IPassMethods>();
            if (passMethod != null) newItem.action = passMethod.PassMethods(i);

             // for every item call the passmethod(i);
            ActionInventory.Add(newItem);
        }

        GenInventory();
    }

    
    void GenInventory()
    {
        if (buttons.Count > 0)
        {
            //for (int i = 0; i < buttons.Count; i++)
            //{
            //    Destroy(buttons[i]);
            //    Debug.Log("Button Destroyed");
            //}
            foreach (GameObject button in buttons)
            {
                Destroy(button.gameObject);
                Debug.Log("Button Destroyed");
            }
            buttons.Clear();
            Debug.Log(buttons.Count);
        }

        if (ActionInventory.Count < 3) gridGroup.constraintCount = ActionInventory.Count;
        else gridGroup.constraintCount = 2;

        foreach (ActionItem newItem in ActionInventory)
        {
            GameObject newButton = Instantiate(buttonTemplate) as GameObject;
            newButton.SetActive(true);

            newButton.transform.SetParent(buttonTemplate.transform.parent, false);
            buttons.Add(newButton.gameObject);
        }
    }


    public class ActionItem
    {
        public Sprite iconSprite;
        public Action action;
    }
}
