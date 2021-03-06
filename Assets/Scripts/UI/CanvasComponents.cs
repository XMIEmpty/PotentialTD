﻿using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class CanvasComponents : MonoBehaviour
{
    [Header("Generic")] public TileHandling tileHandling;

    public GameObject costsBoxGo;
    public Text mushLogCostsText, soulCostsText, foodCostsText;

    public GameObject infoBoxGo;
    [FormerlySerializedAs("InfoBox_Text")] public Text infoBoxText;

    [Header("Tile Controller Section")] public GameObject tileController;
    public Image tilePortraitImage, tileFrameImage;
    public Text tileNameText;
    public Button tileUpgradeButton, tileRepairButton, tileCancelButton, tileAttackButton, tileInfoButton;
    public GameObject tileActionsMenu, tileViewport, tileContent;


    [Header("Entity Controller Section")] public GameObject entityController;
    public Image entityPortraitImage, entityFrameImage;
    public Text entityNameText;
    public Button entityLeftArrowButton, entityRightArrowButton;
    public Text entityActionCategoryText;
    public Text entityHealthText, entityHungerText, entityEnergyText;
    public Button entityAboutButton;
    public GameObject entityActionsMenu, entityViewport, entityContent;

    [Header("Resource Bar Section")] public GameObject resourceBar;
    public Button menuButton;
    public Button mushLogButton, soulButton, foodButton;
    public Text mushLogText, soulsText, foodText;

    [Header("Tile Building Bar Section")] public GameObject tileBuilder;
    public Image buildBarBgImage;
    public Image buildBarBuildButtonBgImage;
    public Button buildBarBuildButtonButton;
    public GameObject buildBarMainButtonsGo;
    public Button buildBarLeftArrowButton, buildBarRightArrowButton;
    public GameObject buildBarBuildingsMenu, buildBarViewport, buildBarContent;

    [Header("List Scripts")] public EntityActionControlGUI entityActionControlGuiScript;
    public TileActionControlGUI tileActionControlGuiScript;
    public BuildsBarControlGUI buildsBarControlGuiScript;

    private static readonly int CloseEntityController = Animator.StringToHash("Close_EntityController");
    private static readonly int IsControllerOpen = Animator.StringToHash("isControllerOpen");
    private static readonly int CloseTileController = Animator.StringToHash("Close_TileController");
    private static readonly int OpenEntityController = Animator.StringToHash("Open_EntityController");
    private static readonly int OpenTileController = Animator.StringToHash("Open_TileController");

    private Animator m_AnimatorInfoBoxGo, m_AnimatorEntityController, m_AnimatorTileController;

    // private Animator 

    private void Awake()
    {
        GetAndSetComponents(); // Very Slow that's why one call is enough
        GetAnimators();

        InvokeRepeating(nameof(UpdateValues), 0, 0.2f);
    }

    private void GetAnimators()
    {
        m_AnimatorTileController = tileController.GetComponent<Animator>();
        m_AnimatorEntityController = entityController.GetComponent<Animator>();
        m_AnimatorInfoBoxGo = infoBoxGo.GetComponent<Animator>();
    }


    /// <summary>
    /// SLOW AF CALL ONLY ONCE!
    /// </summary>
    private void GetAndSetComponents()
    {
        tileHandling = GameObject.Find("GameManager").GetComponent<TileHandling>();

        costsBoxGo = transform.Find("Costs-Box").gameObject;
        mushLogCostsText = costsBoxGo.transform.Find("MushLog").GetComponent<Text>();
        soulCostsText = costsBoxGo.transform.Find("Soul").GetComponent<Text>();
        foodCostsText = costsBoxGo.transform.Find("Food").GetComponent<Text>();

        infoBoxGo = transform.Find("Info-Box").gameObject;
        infoBoxText = infoBoxGo.transform.Find("Text").GetComponent<Text>();

        tileController = transform.Find("TileController").gameObject;
        tilePortraitImage = tileController.transform.Find("Portrait").GetComponent<Image>();
        tileFrameImage = tilePortraitImage.transform.Find("Frame").GetComponent<Image>();
        tileNameText = tilePortraitImage.transform.Find("Name").GetComponent<Text>();
        tileUpgradeButton = tileController.transform.Find("Upgrade_Button").GetComponent<Button>();
        tileRepairButton = tileController.transform.Find("Repair_Button").GetComponent<Button>();
        tileCancelButton = tileController.transform.Find("Stop-Cancel_Button").GetComponent<Button>();
        tileAttackButton = tileController.transform.Find("Attack_Button").GetComponent<Button>();
        tileInfoButton = tileController.transform.Find("Info_Button").GetComponent<Button>();
        tileActionsMenu = tileController.transform.Find("ActionsMenu").gameObject;
        tileViewport = tileActionsMenu.transform.Find("Viewport").gameObject;
        tileContent = tileViewport.transform.Find("Content").gameObject;

        entityController = transform.Find("EntityController").gameObject;
        entityPortraitImage = entityController.transform.Find("Portrait").GetComponent<Image>();
        entityFrameImage = entityPortraitImage.transform.Find("Frame").GetComponent<Image>();
        entityNameText = entityPortraitImage.transform.Find("Name").GetComponent<Text>();
        entityLeftArrowButton = entityController.transform.Find("Left_Arrow_Button").GetComponent<Button>();
        entityRightArrowButton = entityController.transform.Find("Right_Arrow_Button").GetComponent<Button>();
        entityActionCategoryText = entityController.transform.Find("ActionCategory").GetComponent<Text>();
        entityHealthText = entityController.transform.Find("Health").Find("Text").GetComponent<Text>();
        entityHungerText = entityController.transform.Find("Hunger").Find("Text").GetComponent<Text>();
        entityEnergyText = entityController.transform.Find("Energy").Find("Text").GetComponent<Text>();
        entityAboutButton = entityController.transform.Find("About_Button").GetComponent<Button>();
        entityActionsMenu = entityController.transform.Find("ActionsMenu").gameObject;
        entityViewport = entityActionsMenu.transform.Find("Viewport").gameObject;
        entityContent = entityViewport.transform.Find("Content").gameObject;

        resourceBar = transform.Find("ResourcesBar").gameObject;
        menuButton = resourceBar.transform.Find("Menu_Button").GetComponent<Button>();
        mushLogButton = resourceBar.transform.Find("MushLog_Button").GetComponent<Button>();
        soulButton = resourceBar.transform.Find("Soul_Button").GetComponent<Button>();
        foodButton = resourceBar.transform.Find("Food_Button").GetComponent<Button>();
        mushLogText = mushLogButton.transform.Find("Text").GetComponent<Text>();
        soulsText = soulButton.transform.Find("Text").GetComponent<Text>();
        foodText = foodButton.transform.Find("Text").GetComponent<Text>();

        tileBuilder = transform.Find("TileBuilder").gameObject;
        buildBarBgImage = tileBuilder.transform.Find("BuildsBar").GetComponent<Image>();
        buildBarBuildButtonBgImage = tileBuilder.transform.Find("Build_Button_Box").GetComponent<Image>();
        buildBarBuildButtonButton = buildBarBuildButtonBgImage.transform.Find("Build_Button").GetComponent<Button>();
        buildBarMainButtonsGo = tileBuilder.transform.Find("Buttons").gameObject;
        buildBarLeftArrowButton = buildBarMainButtonsGo.transform.Find("L_Arrow").GetComponent<Button>();
        buildBarRightArrowButton = buildBarMainButtonsGo.transform.Find("R_Arrow").GetComponent<Button>();
        buildBarBuildingsMenu = tileBuilder.transform.Find("BuildingsMenu").gameObject;
        buildBarViewport = buildBarBuildingsMenu.transform.Find("Viewport").gameObject;
        buildBarContent = buildBarViewport.transform.Find("Content").gameObject;
        
        entityActionControlGuiScript = entityActionsMenu.GetComponent<EntityActionControlGUI>();
        tileActionControlGuiScript = tileActionsMenu.GetComponent<TileActionControlGUI>();
        buildsBarControlGuiScript = buildBarBuildingsMenu.GetComponent<BuildsBarControlGUI>();
    }


    public void OnClickChanges()
    {
        UpdateBuildsBar();
        
        // Attempt to Select/Deselect
        if (tileHandling.selectedTileToBuild == null)
        {
            // Close Controllers
            // Is nothing selected or multiple selected
            if (tileHandling.selectedUnits.Count == 0) // When ground Selected
            {
                Debug.Log("selectedUnit == null");
                if (tileHandling.lastSelectedUnit != null)
                {
                    switch (tileHandling.lastSelectedUnit.layer)
                    {
                        case 8: // Entity
                            // Closing Controller Entity

                            m_AnimatorEntityController.SetTrigger(CloseEntityController);
                            m_AnimatorInfoBoxGo.SetBool(IsControllerOpen, false);

                            break;
                        case 9: // Tile
                            // Closing Controller Tile
                            
                            m_AnimatorTileController.SetTrigger(CloseTileController);
                            m_AnimatorInfoBoxGo.SetBool(IsControllerOpen, false);

                            Debug.Log("ClosingTile");
                            break;
                    }
                }

                return;
            }

            // Edit Controllers
            if (tileHandling.selectedUnits.Count > 0)
            {
                // Multiple Selected Units
                switch (tileHandling.selectedUnits[0].layer)
                {
                    case 8: // Entity

                        if (tileHandling.lastSelectedUnit == null)
                        {
                            // Opening Controller Entity
                            m_AnimatorEntityController.SetTrigger(OpenEntityController);
                            m_AnimatorInfoBoxGo.SetBool(IsControllerOpen, true);
                            return;
                        }

                        if (tileHandling.lastSelectedUnit.layer == 9) // Tile
                        {
                            // Switching from Tile to Entity
                            m_AnimatorEntityController.SetTrigger(OpenEntityController);
                            m_AnimatorTileController.SetTrigger(CloseTileController);
                            m_AnimatorInfoBoxGo.SetBool(IsControllerOpen, true);
                        }

                        if (tileHandling.lastSelectedUnit.layer == 8) // Entity
                        {
                            // Remaining on Entity
                            m_AnimatorInfoBoxGo.SetBool(IsControllerOpen, true);
                        }
                        
                        break;
                    case 9: // Tile

                        if (tileHandling.lastSelectedUnit == null) // Null
                        {
                            // Opening Controller Tile
                            tileNameText.text = tileHandling.selectedUnits[0].GetComponent<A_Building>().tileName;
                            tilePortraitImage.sprite = tileHandling.selectedUnits[0].GetComponent<SpriteRenderer>().sprite;
                            m_AnimatorTileController.SetTrigger(OpenTileController);
                            m_AnimatorInfoBoxGo.SetBool(IsControllerOpen, true);
                            return;
                        }

                        if (tileHandling.lastSelectedUnit.layer == 8) // Entity
                        {
                            // Switching from Entity To Tile
                            tileNameText.text = tileHandling.selectedUnits[0].GetComponent<A_Building>().tileName;
                            tilePortraitImage.sprite = tileHandling.selectedUnits[0].GetComponent<SpriteRenderer>().sprite;
                            m_AnimatorTileController.SetTrigger(OpenTileController);
                            m_AnimatorEntityController.SetTrigger(CloseEntityController);
                            m_AnimatorInfoBoxGo.SetBool(IsControllerOpen, true);
                        }

                        if (tileHandling.lastSelectedUnit.layer == 9) // Tile
                        {
                            // Remaining on Entity
                            tileNameText.text = tileHandling.selectedUnits[0].GetComponent<A_Building>().tileName;
                            tilePortraitImage.sprite = tileHandling.selectedUnits[0].GetComponent<SpriteRenderer>().sprite;
                            m_AnimatorInfoBoxGo.SetBool(IsControllerOpen, true);
                        }
                        //tileUpgrade_Button.onClick.AddListener(tileHandling.selectedUnit.GetComponent<A_Building>().action_Upgrade);
                        
                        break;
                    
                }
                return;
            }
            
            // Open Controllers
            if (tileHandling.selectedUnits.Count > 0)
            {
                switch (tileHandling.selectedUnits[0].layer) // When Entity Or Tile Selected
                {
                    case 8: // Entity

                        if (tileHandling.lastSelectedUnit == null) // Null
                        {
                            // Opening Controller Entity
                            m_AnimatorEntityController.SetTrigger(OpenEntityController);
                            m_AnimatorInfoBoxGo.SetBool(IsControllerOpen, true);
                            return;
                        }

                        if (tileHandling.lastSelectedUnit.layer == 9) // Tile
                        {
                            // Switching from Tile To Entity
                            m_AnimatorTileController.SetTrigger(CloseTileController);
                            m_AnimatorEntityController.SetTrigger(OpenEntityController);
                            m_AnimatorInfoBoxGo.SetBool(IsControllerOpen, true);
                        }

                        if (tileHandling.lastSelectedUnit.layer == 8) // Entity
                        {
                            // Remaining on Entity
                            m_AnimatorInfoBoxGo.SetBool(IsControllerOpen, true);
                        }

                        break;
                    case 9: // Tile

                        if (tileHandling.lastSelectedUnit == null) // Null
                        {
                            // Opening Controller Tile
                            tileNameText.text = tileHandling.selectedUnits[0].GetComponent<A_Building>().tileName;
                            tilePortraitImage.sprite = tileHandling.selectedUnits[0].GetComponent<SpriteRenderer>().sprite;
                            m_AnimatorTileController.SetTrigger(OpenTileController);
                            m_AnimatorInfoBoxGo.SetBool(IsControllerOpen, true);
                            return;
                        }

                        if (tileHandling.lastSelectedUnit.layer == 8) // Entity
                        {
                            // Switching from Entity To Tile
                            tileNameText.text = tileHandling.selectedUnits[0].GetComponent<A_Building>().tileName;
                            tilePortraitImage.sprite = tileHandling.selectedUnits[0].GetComponent<SpriteRenderer>().sprite;
                            m_AnimatorTileController.SetTrigger(OpenTileController);
                            m_AnimatorEntityController.SetTrigger(CloseEntityController);
                            m_AnimatorInfoBoxGo.SetBool(IsControllerOpen, true);
                        }

                        if (tileHandling.lastSelectedUnit.layer == 9) // Tile
                        {
                            // Remaining on Entity
                            tileNameText.text = tileHandling.selectedUnits[0].GetComponent<A_Building>().tileName;
                            tilePortraitImage.sprite = tileHandling.selectedUnits[0].GetComponent<SpriteRenderer>().sprite;
                            m_AnimatorInfoBoxGo.SetBool(IsControllerOpen, true);
                        }
                        //tileUpgrade_Button.onClick.AddListener(tileHandling.selectedUnit.GetComponent<A_Building>().action_Upgrade);

                        break;
                }
            }
        }
    }


    private void UpdateValues()
    {
        if (tileHandling.selectedUnits.Count > 0)
        {
            Debug.Log("Values Updated");

            if (tileHandling.selectedUnits[0] != null) return;

            switch (tileHandling.selectedUnits[0].layer)
            {
                case 8: // Entity
                    //Debug.Log("It's an Entity");
                    break;


                case 9: // Tile
                    //Debug.Log("It's a Tile");

                    break;
            }
        }
    }

    
    private void UpdateBuildsBar()
    {
        // Probably code that updates buildbar based on changes (Upgrades Complete, Buildings Built, etc.) 
    }
}


//if (tileHandling.selectedUnit != null &&
//    tileHandling.LastSelectedUnit != null &&
//    tileHandling.selectedUnit.layer == tileHandling.LastSelectedUnit.layer) return;

//if (tileHandling.selectedTileToBuild != null)
//{
//    switch (tileHandling.LastSelectedUnit.layer || tileHandling.selectedUnit.layer)
//    {
//        case 8: // Entity

//            Debug.Log("Closing Controller Entity");
//            entityController.GetComponent<Animator>().SetTrigger("Close_EntityController");

//            break;
//        case 9: // Tile

//            Debug.Log("Closing Controller Tile");
//            tileController.GetComponent<Animator>().SetTrigger("Close_TileController");


//            break;
//    }
//}