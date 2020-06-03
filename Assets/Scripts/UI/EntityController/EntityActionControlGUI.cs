using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EntityActionControlGUI : MonoBehaviour
{

    [SerializeField] private GameObject buttonTemplate;
    [SerializeField] private GridLayoutGroup gridGroup;
    [SerializeField] private Sprite[] iconSprites; // Just a temporal lib of images to randomly assign on a EntityActionButton


    private List<ActionItem> ActionInventory;
    private List<GameObject> buttons;


    private void Start()
    {
        buttons = new List<GameObject>();
        ActionInventory = new List<ActionItem>();

        for (int i = 0; i < 4; i++)
        {
            ActionItem newItem = new ActionItem();
            newItem.iconSprite = iconSprites[Random.Range(0, iconSprites.Length)];

            ActionInventory.Add(newItem);
        }

        GenInventory();
    }


    void GenInventory()
    {
        if (buttons.Count > 0)
        {
            foreach (GameObject button in buttons) Destroy(button.gameObject);
            buttons.Clear();
        }


        if (ActionInventory.Count < 2) gridGroup.constraintCount = ActionInventory.Count;
        else gridGroup.constraintCount = 1;

        foreach (ActionItem newItem in ActionInventory)
        {
            GameObject newButton = Instantiate(buttonTemplate) as GameObject;
            newButton.SetActive(true);

            //newButton.GetComponent<EntityActionButtonGUI>().SetIcon(newItem.iconSprite);
            newButton.transform.SetParent(buttonTemplate.transform.parent, false);
            buttons.Add(newButton.gameObject);
        }
    }


    public class ActionItem
    {
        public Sprite iconSprite;
    }
}
