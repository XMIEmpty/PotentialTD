using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// What activity shall the Entity Perform with this Harvestable
/// </summary>
public enum H_Interactions { none = 0, gather = 1, place = 2 }
/// <summary>
/// What material shall the Harvestable return everytime
/// </summary>
public enum H_Return { none = 0, souls = 1, mushLogs = 2, food = 3, }
/// <summary>
/// What is the current stage of growth (alsp helps with current animation)
/// </summary>
public enum H_GrowthStages { none = 0, firstStage = 1, midStage = 2, finalStage = 3 }
/// <summary>
/// What type of harvestable it is, to determine way of harvesting
/// </summary>
public enum H_Type { none = 0, dirt = 1, plant = 2, shrub = 3, tree = 4, stone = 5 }

public class Harvestable : MonoBehaviour
{
    #region ID Properties
    [Header("ID Properties")]
    [Tooltip("dirt/plant Harvesttime, TimeToPlant dependent")]
    public H_Type plantType = H_Type.none;
    public H_Interactions harvestableInteractions = H_Interactions.none;
    public H_Return harvestableMaterial = H_Return.none;
    public H_GrowthStages currentGrowthStatus = H_GrowthStages.none;
    public string plantTag;
    #endregion

    #region General Status
    [Header("General Status")]
    public int CurrentQuantity;
    public int MaxQuantity;
    #endregion

    #region Timers
    [Header("Growth Timers")]
    public float currentTime;
    [Range(0.0f, 60.0f)]
    [Tooltip("Every Stage stacks with the previous one. \n\r 0 = Instant Growth")]
    public float timeToFirstStage, timeToMiddleStage, timeToFinalStage;
    #endregion

    #region Extra Attributes
    [Header("Other Attributes")]
    public GameObject[] dropsOnDestroyed;
    public GameObject[] dropsOnFinalStage;
    #endregion

    public Animator characterAnimator;
    public bool isGrown;
    public IdleGrownHarvestable idleGrownHarvestable;
    Buildings buildings;

    private void Start()
    {
        CurrentQuantity = MaxQuantity;
        characterAnimator = GetComponent<Animator>();
        buildings = GameObject.Find("GameManager").transform.GetChild(0).gameObject.GetComponent<Buildings>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Gather(20);
        }

        switch (plantType)
        {
            case H_Type.none:                   break;
            case H_Type.dirt:   Dirt_Growth();  break;
            case H_Type.plant:  Plant_Growth(); break;
            case H_Type.shrub:  Shrub_Growth(); break;
            case H_Type.tree:   Tree_Growth();  break;
            case H_Type.stone:  Stone_Growth(); break;
        }
    }


    public int Gather(int workValue)
    {
        switch (plantType)
        {
            case H_Type.none: break;

            case H_Type.dirt:   // Stage 0
                characterAnimator.SetInteger("Stage", 0);
                currentGrowthStatus = H_GrowthStages.none;
                harvestableInteractions = H_Interactions.place;
                currentTime = 0;
                if (workValue <= CurrentQuantity)
                {
                    CurrentQuantity -= workValue;
                    return workValue;
                }
                if (workValue > CurrentQuantity)
                {
                    workValue = CurrentQuantity;
                    CurrentQuantity = 0;
                    return workValue;
                }
                break;

            case H_Type.plant:
                if (workValue <= CurrentQuantity)
                {
                    CurrentQuantity -= workValue;
                    return workValue;
                }
                if (workValue > CurrentQuantity)
                {
                    workValue = CurrentQuantity;
                    CurrentQuantity = 0;
                    return workValue;
                }
                Destroy(gameObject, 0.05f);
                break;

            case H_Type.shrub:
                characterAnimator.SetInteger("Stage", 1);
                currentGrowthStatus = H_GrowthStages.firstStage;
                currentTime = 0; Debug.Log("SHRUB BEEN Gathered!!!");
                harvestableInteractions = H_Interactions.none;
                isGrown = false;
                if (workValue <= CurrentQuantity)
                {
                    CurrentQuantity -= workValue;
                    Debug.Log("work SMALLER currquant returning: " + workValue);
                    return workValue;
                }
                if (workValue > CurrentQuantity)
                {
                    workValue = CurrentQuantity;
                    CurrentQuantity = 0;
                    Debug.Log("work BIGGER currquant returning: " + workValue);
                    return workValue;
                }
                break;


            case H_Type.tree:
                if (workValue <= CurrentQuantity)
                {
                    CurrentQuantity -= workValue;
                    return workValue;
                }
                else if (workValue > CurrentQuantity)
                {
                    Destroy(gameObject, 0.05f);
                    return CurrentQuantity;
                }
                break;

            case H_Type.stone:
                if (workValue <= CurrentQuantity)
                {
                    CurrentQuantity -= workValue;
                    return workValue;
                }
                else if (workValue > CurrentQuantity)
                {
                    Destroy(gameObject, 0.05f);
                    return CurrentQuantity;
                }
                break;
        }
        return 0;
    }


    public void Place()
    {
        switch (plantType)
        {
            case H_Type.none: break;

            case H_Type.dirt:
                characterAnimator.SetInteger("Stage", 1);
                currentGrowthStatus = H_GrowthStages.firstStage;
                harvestableInteractions = H_Interactions.none;
                isGrown = false;
                break;

            case H_Type.plant: break;
            case H_Type.shrub: break;
            case H_Type.tree: break;
            case H_Type.stone: break;
        }
    }


    private void Dirt_Growth()
    {
        //if (harvester == null) gameObject.layer = LayerMask.NameToLayer("Available");
        //else if (harvester != null) gameObject.layer = LayerMask.NameToLayer("Unavailable");
        if (!isGrown)
        {
            currentTime += Time.deltaTime;

            if (currentTime >= timeToMiddleStage)
            {// Stage 2
                characterAnimator.SetInteger("Stage", 2);
                currentGrowthStatus = H_GrowthStages.midStage;

                if (currentTime >= timeToMiddleStage + timeToFinalStage)
                {// Stage 3
                    characterAnimator.SetInteger("Stage", 3);
                    currentGrowthStatus = H_GrowthStages.finalStage;
                    harvestableInteractions = H_Interactions.gather;
                    isGrown = true;
                }
            }
            else if (currentTime < timeToFirstStage)
            {// Stage 1
                characterAnimator.SetInteger("Stage", 1);
                currentGrowthStatus = H_GrowthStages.firstStage;
            }
        }
    }


    private void Plant_Growth()
    {
        //if (harvester == null) gameObject.layer = LayerMask.NameToLayer("Available");
        //else if (harvester != null) gameObject.layer = LayerMask.NameToLayer("Unavailable");
        if (!isGrown)
        {
            currentTime += Time.deltaTime;

            if (currentTime >= timeToMiddleStage)
            {// Stage 2
                characterAnimator.SetInteger("Stage", 2);
                currentGrowthStatus = H_GrowthStages.midStage;

                if (currentTime >= timeToMiddleStage + timeToFinalStage)
                {// Stage 3
                    characterAnimator.SetInteger("Stage", 3);
                    currentGrowthStatus = H_GrowthStages.finalStage;
                    harvestableInteractions = H_Interactions.gather;
                    isGrown = true;
                    idleGrownHarvestable = gameObject.AddComponent<IdleGrownHarvestable>();
                }
            }
            else if (currentTime < timeToFirstStage)
            {// Stage 1
                characterAnimator.SetInteger("Stage", 1);
                currentGrowthStatus = H_GrowthStages.firstStage;
            }
        }
    }


    private void Shrub_Growth()
    {        
        //if (harvester == null) gameObject.layer = LayerMask.NameToLayer("Available");
        //else if (harvester != null) gameObject.layer = LayerMask.NameToLayer("Unavailable");
        if (!isGrown)
        {
            currentTime += Time.deltaTime;

            if (currentTime >= timeToMiddleStage)
            {// Stage 2
                characterAnimator.SetInteger("Stage", 2);
                currentGrowthStatus = H_GrowthStages.midStage;

                if (currentTime >= timeToMiddleStage + timeToFinalStage)
                {// Stage 3
                    characterAnimator.SetInteger("Stage", 3);
                    currentGrowthStatus = H_GrowthStages.finalStage;
                    harvestableInteractions = H_Interactions.gather;
                    CurrentQuantity = MaxQuantity;
                    isGrown = true;
                }
            }
            else if (currentTime < timeToFirstStage)
            {// Stage 1
                characterAnimator.SetInteger("Stage", 1);
                currentGrowthStatus = H_GrowthStages.firstStage;
            }
        }
    }


    private void Tree_Growth() {
        //if (harvester == null) gameObject.layer = LayerMask.NameToLayer("Available");
        //else if (harvester != null) gameObject.layer = LayerMask.NameToLayer("Unavailable");
        if (!isGrown)
        {
            currentTime += Time.deltaTime;

            if (currentTime >= timeToMiddleStage)
            {// Stage 2
                characterAnimator.SetInteger("Stage", 2);
                currentGrowthStatus = H_GrowthStages.midStage;

                if (currentTime >= timeToMiddleStage + timeToFinalStage)
                {// Stage 3
                    characterAnimator.SetInteger("Stage", 3);
                    currentGrowthStatus = H_GrowthStages.finalStage;
                    harvestableInteractions = H_Interactions.gather;
                    CurrentQuantity = MaxQuantity;
                    isGrown = true;
                    idleGrownHarvestable = gameObject.AddComponent<IdleGrownHarvestable>();
                }
            }
            else if (currentTime < timeToFirstStage)
            {// Stage 1
                characterAnimator.SetInteger("Stage", 1);
                currentGrowthStatus = H_GrowthStages.firstStage;
            }
        }
    }


    private void Stone_Growth()
    {
        Debug.Log("BirthOfStone");
        idleGrownHarvestable = gameObject.AddComponent<IdleGrownHarvestable>();
    }
}
