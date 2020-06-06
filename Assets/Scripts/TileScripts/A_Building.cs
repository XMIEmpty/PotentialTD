using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class A_Building : MonoBehaviour
{
    [HideInInspector] public string buildingTag;
    [HideInInspector] public bool isAvailable;

    [Header("Necessary")] public Button.ButtonClickedEvent getMe;

    [Header("Profile")]
    [Space(15)]
    [Tooltip("Ideal size 300x width 165px height\n\rMinimum size 100px width 55px height")]
    public Sprite portrait;

    public string tileName = "NewBuilding";
    public int currentUpgradeLevel = 0;

    [TextArea] public string infoBox;
    // public string UL_Text, UR_Text, DL_Text, DR_Text;

    [Header("BuildCosts")] [Space(15)] public int soulCosts = 5;
    public int mushLogCosts = 5;
    public int foodCosts = 1;

    [Header("DefaultAttributes")] [Space(15)]
    public int maxHealth = 10;

    public int currentHealth;
    public int armor = 0;

    [Space(25)] public bool canAttack = false;
    [Range(0, 100)] public int damage = 1;
    [Range(0.01f, 10.0f)] public float attackSpeed = 1;
    [Range(0.01f, 30.0f)] public float attackRange;

    [Space(25)] public bool canUpgrade = false;
    public Upgrading[] upgrade;

    [Header("Other")] [Space(15)] public Animator animator;

    [System.Serializable]
    public class Upgrading
    {
        public string newTileName;

        [Space(10)] public int soulsCosts;
        public int mushLogCosts;
        public int hungerCosts;

        [Space(10)] public int newMaxHealth;
        public int newArmor;

        [Space(10)] public bool canAttack = false;
        [Range(0, 100)] public int newDamage;
        [Range(0.01f, 10.0f)] public float newAttackSpeed;
        [Range(0.01f, 30.0f)] public float newAttackRange;
    }

    [SerializeField] private TileHandling tileHandling;
    [SerializeField] private SelectTile selectTile;


    [Header("Sprite Stuff")] private Sprite m_Sprite;

    private void Start()
    {
        tileHandling = GameObject.Find("GameManager").GetComponent<TileHandling>();
        selectTile = tileHandling.GetComponent<SelectTile>();
        m_Sprite = GetComponent<SpriteRenderer>().sprite;
        // Instantiate();
        //Debug.LogError(tileHandling);

        transform.position += new Vector3(0.0f, 0.0f, -0.00001f);
    }


    public void Upgrade()
    {
        if (currentUpgradeLevel >= upgrade.Length)
        {
            Debug.Log("Max Upgrade Level Reached \n\r Upgrade *** failed ***");
            return;
        }

        tileName = upgrade[currentUpgradeLevel].newTileName;

        soulCosts = upgrade[currentUpgradeLevel].soulsCosts;
        mushLogCosts = upgrade[currentUpgradeLevel].mushLogCosts;
        foodCosts = upgrade[currentUpgradeLevel].hungerCosts;

        maxHealth = upgrade[currentUpgradeLevel].newMaxHealth;
        currentHealth = maxHealth;
        armor = upgrade[currentUpgradeLevel].newArmor;

        canAttack = upgrade[currentUpgradeLevel].canAttack;
        damage = upgrade[currentUpgradeLevel].newDamage;
        attackSpeed = upgrade[currentUpgradeLevel].newAttackSpeed;
        attackRange = upgrade[currentUpgradeLevel].newAttackRange;

        currentUpgradeLevel++;

        Debug.Log("Upgrade Complete \n\r Upgrade LvL *** " + currentUpgradeLevel + " ***");
    }

    public void Repair()
    {
        // Call the closest agent found to repair this building

        // LATER UPDATE: The priorities jump from Idle < Health < Work < Build < Hunt
    }

    public void CancelAction()
    {
        // Add all local Action Cancelings
    }

    public void Attack()
    {
        // Change Mouse to target icon
        // when play clicks something check if it within range && if it is an enemyentity
    }


    public void SetBasicButtonsAttributes()
    {
        Debug.Log("Setting Up Basic Buttons");
        SetUpgradeButtonActions();
    }
    
    
    
    

    private string pointerClickAction, pointerEnterAction, pointerExitAction;


    public void EmptyAndClosePrefabInfo()
    {
        tileHandling.canvasComponents.infoBoxText.text = "";
        tileHandling.canvasComponents.infoBoxGo.GetComponent<Animator>().SetTrigger("Close_InfoBox");
        // Empty Costs Values and Close that as well
        tileHandling.canvasComponents.costsBoxGo.SetActive(false);
    }


    /// <summary>
    /// Add the appropriate (based on settings) Events to the Event Trigger component
    /// Set up the Events with the appropriately added methods 
    /// </summary>
    private void SetUpgradeButtonActions()
    {        
        
        // Get Event Trigger and Pass it to a variable
        var eventTrigger = tileHandling.canvasComponents.tileUpgradeButton.GetComponent<EventTrigger>();

        // Create variables for all possible Entry Types and pass them the appropriate entry type
        var onPointerClickEntry = new EventTrigger.Entry {eventID = EventTriggerType.PointerClick};
        var onPointerEnterEntry = new EventTrigger.Entry {eventID = EventTriggerType.PointerEnter};
        var onPointerExitEntry = new EventTrigger.Entry {eventID = EventTriggerType.PointerExit};



        #region DELETE OLD

        // for (var i = eventTrigger.triggers.Count - 1; i >= 0; i--)
        // {
            eventTrigger.triggers.Clear();
        // }
        

        #endregion
        
        
        
        
        
        
        // Check if the action string is NOT null/empty
        // if (!string.IsNullOrEmpty(pointerClickAction)) // On CLICK
        // {
            // Add to the appropriate entry's callback a Listener with all the actions(methods) it oughts to call    
            onPointerClickEntry.callback.AddListener((eventData) => { Upgrade(); });
            // Add the Entry to the event trigger list
            eventTrigger.triggers.Add(onPointerClickEntry);
        // }

        // if (!string.IsNullOrEmpty(pointerEnterAction)) // On Pointer Enter
        // {
            onPointerEnterEntry.callback.AddListener((eventData) =>
            {
                selectTile.Invoke("Get" + name + "_InfoCosts", 0f);

                // Display Info Box
                // Display Costs
            });
            eventTrigger.triggers.Add(onPointerEnterEntry);
        // }

        // if (!string.IsNullOrEmpty(pointerExitAction)) // On Pointer Exit
        // {
            onPointerExitEntry.callback.AddListener((eventData) =>
            {
                selectTile.Invoke("EmptyAndClosePrefabInfo", 0f);

                // Hide Info Box
                // Hide Costs
                // SendMessage(pointerExitAction);
            });
            eventTrigger.triggers.Add(onPointerExitEntry);
        // }
    }
}