using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class A_Building : MonoBehaviour, IInteractable
{
    [HideInInspector] public string buildingTag;
    [HideInInspector] public bool isAvailable;

    [Header("Necessary")]
    public Button.ButtonClickedEvent getMe;

    [Header("Profile")]
    [Space(15)]
    public string tileName = "NewBuilding";
    public int currentUpgradeLevel = 0;
    [TextArea]
    public string InfoBox;
    public string UL_Text, UR_Text, DL_Text, DR_Text;

    [Header("BuildCosts")]
    [Space(15)]
    public int soulCosts = 5;
    public int mushLogCosts = 5;
    public int foodCosts = 1;

    [Header("DefaultAttibutes")]
    [Space(15)]
    public int maxHealth = 10;
    public int currentHealth;
    public int armor = 0;

    [Space(25)]
    public bool canAttack = false;
    [Range(0, 100)]
    public int damage = 1;
    [Range(0.01f, 10.0f)]
    public float attackSpeed = 1;
    [Range(0.01f, 30.0f)]
    public float attackRange;

    [Space(25)]
    public bool canUpgrade = false;
    public Upgrading[] upgrade;

    [Header("Other")]
    [Space(15)]
    public Animator animator;

    public UnityEvent upgradeEvent;
    public UnityAction action_Upgrade;
    public UnityAction action_Repair;
    public UnityAction action_Cancel;
    public UnityAction action_Attack;


    [System.Serializable]
    public class Upgrading
    {
        public string newTileName;
       
        [Space(10)]
        public int soulsCosts;
        public int mushLogCosts;
        public int hungerCosts;

        [Space(10)]
        public int newMaxHealth;
        public int newArmor;

        [Space(10)]
        public bool canAttack = false;
        [Range(0, 100)]
        public int newDamage;
        [Range(0.01f, 10.0f)]
        public float newAttackSpeed;
        [Range(0.01f, 30.0f)]
        public float newAttackRange;
    }

    [SerializeField] private TileHandling tileHandling;

    private void Start()
    {
        tileHandling = GameObject.Find("GameManager").GetComponent<TileHandling>();
        //Debug.LogError(tileHandling);

        transform.position += new Vector3(0.0f, 0.0f, -0.00001f);
        
        action_Upgrade = Upgrade;
        action_Repair = Repair;
        action_Cancel = CancelAction;
        action_Attack = Attack;
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

    public void SetValuesToController()
    {
        //Name.text = tileName;
        //UL_Button_Text.text = UL_Text;
        //UR_Button_Text.text = UR_Text;
        //DL_Button_Text.text = DL_Text;
        //DR_Button_Text.text = DR_Text;
    }

    public void Interact()
    {
        SetValuesToController();
    }
}
