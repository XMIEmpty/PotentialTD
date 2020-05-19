using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleGrownHarvestable : MonoBehaviour
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

    #region Extra Attributes
    [Header("Other Attributes")]
    public GameObject[] dropsOnDestroyed;
    public GameObject[] dropsOnFinalStage;
    #endregion

    private Harvestable harvy;

    [SerializeField]
    private Animator characterAnimator;
    [SerializeField]
    private bool isGrown, isAvailable;

    public void Awake()
    {
        harvy = GetComponent<Harvestable>();
        harvy.enabled = false;
        

        plantType = harvy.plantType;
        harvestableInteractions = harvy.harvestableInteractions;
        harvestableMaterial = harvy.harvestableMaterial;
        currentGrowthStatus = harvy.currentGrowthStatus;
        plantTag = harvy.plantTag;

        CurrentQuantity = harvy.currentQuantity;
        MaxQuantity = harvy.MaxQuantity;

        dropsOnDestroyed = harvy.dropsOnDestroyed;
        dropsOnFinalStage = harvy.dropsOnFinalStage;

        characterAnimator = harvy.characterAnimator;
        isGrown = harvy.isGrown;
        isAvailable = harvy.isAvailable;
    }


    public int Gather(int workValue)
    {
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
        return 0;
    }

    public void Place()
    {
        harvy.plantType = plantType;
        harvy.harvestableInteractions = harvestableInteractions;
        harvy.harvestableMaterial = harvestableMaterial;
        harvy.currentGrowthStatus = currentGrowthStatus;
        harvy.plantTag = plantTag;

        harvy.currentQuantity = CurrentQuantity;
        harvy.MaxQuantity = MaxQuantity;

        harvy.dropsOnDestroyed = dropsOnDestroyed;
        harvy.dropsOnFinalStage = dropsOnFinalStage;

        harvy.characterAnimator = characterAnimator;
        harvy.isGrown = isGrown;
        harvy.isAvailable = isAvailable;
    }
}
