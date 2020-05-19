using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class A_Building : MonoBehaviour, IInteractable
{
    [HideInInspector] public string buildingTag;
    [HideInInspector] public bool isAvailable;

    [Header("Profile")]
    [Space(15)]
    public string tileName = "NewBuilding";
    public int currentUpgradeLevel = 0;
    public string UL_Text, UR_Text, DL_Text, DR_Text;

    [Header("BuildCosts")]
    [Space(15)]
    public int soulsCosts = 5;
    public int mushLogCosts = 5;
    public int hungerCosts = 1;

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
    [SerializeField] private Button Upgrade_Button, AttackButton;
    [SerializeField] private Text Name, UL_Button_Text, UR_Button_Text, DL_Button_Text, DR_Button_Text;
    private void Start()
    {
        tileHandling = GameObject.Find("GameManager").GetComponent<TileHandling>();
        //Debug.LogError(tileHandling);

        Name = tileHandling.canvasComponents.tileController.transform.Find("Portrait").Find("Name").GetComponent<Text>();
        UL_Button_Text = tileHandling.canvasComponents.tileController.transform.Find("UL_Action_Button").Find("Text").GetComponent<Text>();
        UR_Button_Text = tileHandling.canvasComponents.tileController.transform.Find("UR_Action_Button").Find("Text").GetComponent<Text>();
        DL_Button_Text = tileHandling.canvasComponents.tileController.transform.Find("DL_Action_Button").Find("Text").GetComponent<Text>();
        DR_Button_Text = tileHandling.canvasComponents.tileController.transform.Find("DR_Action_Button").Find("Text").GetComponent<Text>();

    }



    public void Upgrade()
    {
        if (currentUpgradeLevel >= upgrade.Length)
        {
            Debug.Log("Max Upgrade Level Reached \n\r Upgrade *** failed ***");
            return;
        }

        tileName = upgrade[currentUpgradeLevel].newTileName;

        soulsCosts = upgrade[currentUpgradeLevel].soulsCosts;
        mushLogCosts = upgrade[currentUpgradeLevel].mushLogCosts;
        hungerCosts = upgrade[currentUpgradeLevel].hungerCosts;

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
