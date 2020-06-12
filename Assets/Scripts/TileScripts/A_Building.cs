using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

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
    public Vector3Int allCosts;

    [Header("DefaultAttributes")] [Space(15)]
    public int maxHealth = 10;

    public int currentHealth;
    public int armor = 0;
    [Space(25)] public bool canCallForRepair = false;
    [Space(25)] public bool canStopActiveActions = false;
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
        [TextArea] public string infoBox;


        [Space(10)] public int soulCosts;
        public int mushLogCosts;
        public int foodCosts;
        public Vector3Int allCosts;

        [Space(10)] public int newMaxHealth;
        public int newArmor;
        [Space(10)] public bool canUpgrade;
        [Space(10)] public bool canCallForRepair = false;
        [Space(10)] public bool canStopActiveActions = false;
        [Space(10)] public bool canAttack = false;
        [Range(0, 100)] public int newDamage;
        [Range(0.01f, 10.0f)] public float newAttackSpeed;
        [Range(0.01f, 30.0f)] public float newAttackRange;
    }



    [HideInInspector] public TileHandling tileHandling;
    [SerializeField] private SelectTile selectTile;


    [Header("Sprite Stuff")] private Sprite m_Sprite;
    private static readonly int CloseInfoBox = Animator.StringToHash("Close_InfoBox");

    private void Start()
    {
        tileHandling = GameObject.Find("GameManager").GetComponent<TileHandling>();
        selectTile = tileHandling.GetComponent<SelectTile>();
        m_Sprite = GetComponent<SpriteRenderer>().sprite;
        // Instantiate();
        //Debug.LogError(tileHandling);

        transform.position += new Vector3(0.0f, 0.0f, -0.00001f);
        allCosts.x = mushLogCosts;
        allCosts.y = soulCosts;
        allCosts.z = foodCosts;
    }


    public void SelectIt() => transform.GetChild(0).gameObject.SetActive(true);
    public void UnselectIt() => transform.GetChild(0).gameObject.SetActive(false);
    
    
    private void Upgrade()
    {
        if (currentUpgradeLevel >= upgrade.Length)
        {
            Debug.Log("Max Upgrade Level Reached \n\r Upgrade *** failed ***");
            return;
        }

        tileHandling.resourceBarManager.SubtractAll(
            upgrade[currentUpgradeLevel].mushLogCosts,
            upgrade[currentUpgradeLevel].soulCosts,
            upgrade[currentUpgradeLevel].foodCosts);

        tileName = upgrade[currentUpgradeLevel].newTileName;
        infoBox = upgrade[currentUpgradeLevel].infoBox;

        mushLogCosts = upgrade[currentUpgradeLevel].mushLogCosts;
        soulCosts = upgrade[currentUpgradeLevel].soulCosts;
        foodCosts = upgrade[currentUpgradeLevel].foodCosts;
        allCosts.x = upgrade[currentUpgradeLevel].mushLogCosts;
        allCosts.y = upgrade[currentUpgradeLevel].soulCosts;
        allCosts.z = upgrade[currentUpgradeLevel].foodCosts;

        maxHealth = upgrade[currentUpgradeLevel].newMaxHealth;
        currentHealth = maxHealth;
        armor = upgrade[currentUpgradeLevel].newArmor;

        canUpgrade = upgrade[currentUpgradeLevel].canUpgrade;
        canCallForRepair = upgrade[currentUpgradeLevel].canCallForRepair;
        canStopActiveActions = upgrade[currentUpgradeLevel].canStopActiveActions;
        canAttack = upgrade[currentUpgradeLevel].canAttack;
        damage = upgrade[currentUpgradeLevel].newDamage;
        attackSpeed = upgrade[currentUpgradeLevel].newAttackSpeed;
        attackRange = upgrade[currentUpgradeLevel].newAttackRange;
        
        currentUpgradeLevel++;

        Debug.Log("Upgrade Complete \n\r Upgrade LvL " + currentUpgradeLevel);
        tileHandling.canvasComponents.tileActionControlGuiScript.CreateButtonList();
        UpdateDefaultButtonStatus();
        tileHandling.canvasComponents.OnClickChanges();
    }

    
    private void Repair()
    {
        // Call the closest agent found to repair this building

        // LATER UPDATE: The priorities jump from Idle < Health < Work < Build < Hunt
        Debug.Log("Repair call Completed");
    }

    
    private void StopAction()
    {
        // Add all local Action Cancelings
        Debug.Log("Stop actions call Complete");
    }

    
    private void Attack()
    {
        // Change Mouse to target icon
        // when play clicks something check if it within range && if it is an enemy entity
        Debug.Log("Attack call Complete");
    }


    public void SetBasicButtonsAttributes()
    {
        SetUpgradeButtonActions();
    }


    public void EmptyAndClosePrefabInfo()
    {
        tileHandling.canvasComponents.infoBoxText.text = "";
        tileHandling.canvasComponents.infoBoxGo.GetComponent<Animator>().SetTrigger(CloseInfoBox);
        // Empty Costs Values and Close that as well
        tileHandling.canvasComponents.costsBoxGo.SetActive(false);
    }


    public void RegisterCosts(Vector3Int costs)
    {
        
    }
    
    /// <summary>
    /// Add the appropriate (based on settings) Events to the Event Trigger component
    /// Set up the Events with the appropriately added methods 
    /// </summary>
    private void SetUpgradeButtonActions()
    {
        UpdateDefaultButtonStatus();
        
        // Get Event Triggers and Pass them it to their appropriate variables
        var upgradeTrigger = tileHandling.canvasComponents.tileUpgradeButton.GetComponent<EventTrigger>();
        var repairTrigger = tileHandling.canvasComponents.tileRepairButton.GetComponent<EventTrigger>();
        var cancelTrigger = tileHandling.canvasComponents.tileCancelButton.GetComponent<EventTrigger>();
        var attackTrigger = tileHandling.canvasComponents.tileAttackButton.GetComponent<EventTrigger>();
        var infoTrigger = tileHandling.canvasComponents.tileInfoButton.GetComponent<EventTrigger>();

        // Create variables for all possible Entry Types and pass them the appropriate entry type
        var onUpgradePointerClickEntry = new EventTrigger.Entry {eventID = EventTriggerType.PointerClick};
        var onUpgradePointerEnterEntry = new EventTrigger.Entry {eventID = EventTriggerType.PointerEnter};
        var onUpgradePointerExitEntry = new EventTrigger.Entry {eventID = EventTriggerType.PointerExit};
        
        var onRepairPointerClickEntry = new EventTrigger.Entry {eventID = EventTriggerType.PointerClick};
        var onRepairPointerEnterEntry = new EventTrigger.Entry {eventID = EventTriggerType.PointerEnter};
        var onRepairPointerExitEntry = new EventTrigger.Entry {eventID = EventTriggerType.PointerExit};
        
        var onStopPointerClickEntry = new EventTrigger.Entry {eventID = EventTriggerType.PointerClick};
        var onStopPointerEnterEntry = new EventTrigger.Entry {eventID = EventTriggerType.PointerEnter};
        var onStopPointerExitEntry = new EventTrigger.Entry {eventID = EventTriggerType.PointerExit};
        
        var onAttackPointerClickEntry = new EventTrigger.Entry {eventID = EventTriggerType.PointerClick};
        var onAttackPointerEnterEntry = new EventTrigger.Entry {eventID = EventTriggerType.PointerEnter};
        var onAttackPointerExitEntry = new EventTrigger.Entry {eventID = EventTriggerType.PointerExit};

        var onInfoPointerEnterEntry = new EventTrigger.Entry {eventID = EventTriggerType.PointerEnter};
        var onInfoPointerExitEntry = new EventTrigger.Entry {eventID = EventTriggerType.PointerExit};

        // Is Upgrade Button Interactable
        if (upgradeTrigger.GetComponent<Button>().interactable)
        {
            // DELETE OLD List
            upgradeTrigger.triggers.Clear();
            // Add to the appropriate entry's callback a Listener with all the actions(methods) it oughts to call    
            onUpgradePointerClickEntry.callback.AddListener(eventData =>
            {
                Upgrade();
                TriggerThisInAllSelectedUnits(nameof(Upgrade));
            });
            // Add the Entry to the event trigger list
            upgradeTrigger.triggers.Add(onUpgradePointerClickEntry);

            onUpgradePointerEnterEntry.callback.AddListener(evenData =>
            {
                // Display Costs, Display Info Box
                if (currentUpgradeLevel < upgrade.Length) SetUpgradeButtonInfo();
                // selectTile.Invoke("Get" + name.Replace("(Clone)", string.Empty) + "_InfoCosts", 0f);
            });
            upgradeTrigger.triggers.Add(onUpgradePointerEnterEntry);
            
            onUpgradePointerExitEntry.callback.AddListener(eventData =>
            {
                // if (eventData == null) throw new ArgumentNullException(nameof(eventData));
                // Hide Costs, Hide Info Box
                Invoke(nameof(EmptyAndClosePrefabInfo), 0f);
            });
            upgradeTrigger.triggers.Add(onUpgradePointerExitEntry);
        }

        // Is Repair Button Interactable
        if (repairTrigger.GetComponent<Button>().interactable)
        {
            repairTrigger.triggers.Clear();
            onRepairPointerClickEntry.callback.AddListener(eventData =>
            {
                Repair();
                TriggerThisInAllSelectedUnits(nameof(Repair));
            });
            repairTrigger.triggers.Add(onRepairPointerClickEntry);
            onRepairPointerEnterEntry.callback.AddListener(evenData =>
            {
                selectTile.Invoke("Get" + name.Replace("(Clone)", string.Empty) + "_InfoCosts", 0);
            });
            repairTrigger.triggers.Add(onRepairPointerEnterEntry);
            onRepairPointerExitEntry.callback.AddListener(eventData => { Invoke(nameof(EmptyAndClosePrefabInfo), 0f); });
            repairTrigger.triggers.Add(onRepairPointerExitEntry);
        }

        // Is Cancel Button Interactable
        if (cancelTrigger.GetComponent<Button>().interactable)
        {
            cancelTrigger.triggers.Clear();
            onStopPointerClickEntry.callback.AddListener(eventData =>
            {
                StopAction();
                TriggerThisInAllSelectedUnits(nameof(StopAction));
            });
            cancelTrigger.triggers.Add(onStopPointerClickEntry);
            onStopPointerEnterEntry.callback.AddListener(evenData =>
            {
                selectTile.Invoke("Get" + name.Replace("(Clone)", string.Empty) + "_InfoCosts", 0);
            });
            cancelTrigger.triggers.Add(onStopPointerEnterEntry);
            onStopPointerExitEntry.callback.AddListener(eventData => { Invoke(nameof(EmptyAndClosePrefabInfo), 0f); });
            cancelTrigger.triggers.Add(onStopPointerExitEntry);
        }

        // Is Attack Button Interactable
        if (attackTrigger.GetComponent<Button>().interactable)
        {
            attackTrigger.triggers.Clear();
            onAttackPointerClickEntry.callback.AddListener(eventData =>
            {
                Attack();
                TriggerThisInAllSelectedUnits(nameof(Attack));
            });
            attackTrigger.triggers.Add(onAttackPointerClickEntry);
            onAttackPointerEnterEntry.callback.AddListener(evenData =>
            {
                selectTile.Invoke("Get" + name.Replace("(Clone)", string.Empty) + "_InfoCosts", 0);
            });
            attackTrigger.triggers.Add(onAttackPointerEnterEntry);
            onAttackPointerExitEntry.callback.AddListener(eventData =>
            {
                Invoke(nameof(EmptyAndClosePrefabInfo), 0f);
            });
            attackTrigger.triggers.Add(onAttackPointerExitEntry);
        }

        if (infoTrigger.GetComponent<Button>().interactable)
        {
            infoTrigger.triggers.Clear();
            // onAttackPointerClickEntry.callback.AddListener(eventData => { Attack(); });
            // infoTrigger.triggers.Add(onAttackPointerClickEntry);
            onInfoPointerEnterEntry.callback.AddListener(evenData =>
            {
                SetInfoButtonInfo();
            });
            infoTrigger.triggers.Add(onInfoPointerEnterEntry);
            onInfoPointerExitEntry.callback.AddListener(eventData =>
            {
                Invoke(nameof(EmptyAndClosePrefabInfo), 0f);
            });
            infoTrigger.triggers.Add(onInfoPointerExitEntry);
        }
    }


    private void TriggerThisInAllSelectedUnits(string method)
    {
        for (var i = tileHandling.selectedUnits.Count - 1; i >= 1; i--)
        {
            tileHandling.selectedUnits[i].GetComponent<A_Building>().SendMessage(method);
        }
    }


    private void SetUpgradeButtonInfo()
    {
        tileHandling.canvasComponents.infoBoxText.text = upgrade[currentUpgradeLevel].infoBox;
        tileHandling.canvasComponents.infoBoxGo.GetComponent<Animator>().SetTrigger("Open_InfoBox");
        SetUpgradeCosts();
        tileHandling.canvasComponents.costsBoxGo.SetActive(true);
    }


    private void SetUpgradeCosts()
    {
        tileHandling.canvasComponents.mushLogCostsText.text = upgrade[currentUpgradeLevel].mushLogCosts.ToString();
        tileHandling.canvasComponents.soulCostsText.text = upgrade[currentUpgradeLevel].soulCosts.ToString();
        tileHandling.canvasComponents.foodCostsText.text = upgrade[currentUpgradeLevel].foodCosts.ToString();
    }

    
    private void SetInfoButtonInfo()
    {
        tileHandling.canvasComponents.infoBoxText.text = tileName + "\n\r" + infoBox;
        tileHandling.canvasComponents.infoBoxGo.GetComponent<Animator>().SetTrigger("Open_InfoBox");
    }


    private void UpdateDefaultButtonStatus()
    {
        if (!canUpgrade) tileHandling.canvasComponents.tileUpgradeButton.interactable = false;
        else if (canUpgrade) tileHandling.canvasComponents.tileUpgradeButton.interactable = true;
        if (!canCallForRepair) tileHandling.canvasComponents.tileRepairButton.interactable = false;
        if (canCallForRepair) tileHandling.canvasComponents.tileRepairButton.interactable = true;
        if (!canStopActiveActions) tileHandling.canvasComponents.tileCancelButton.interactable = false;
        if (canStopActiveActions) tileHandling.canvasComponents.tileCancelButton.interactable = true;
        if (!canAttack) tileHandling.canvasComponents.tileAttackButton.interactable = false;
        if (canAttack) tileHandling.canvasComponents.tileAttackButton.interactable = true;
    }
}