using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasComponents : MonoBehaviour
{
    
    [Header("Generic")]
    public TileHandling tileHandling;

    [Header("Tile Controller Section")]
    public GameObject tileController;
    public Image tilePortrait_Image, tileFrame_Image;
    public Text tileName_Text;
    public Button tileUpgrade_Button, tileRepair_Button, tileCancel_Button, tileAttack_Button;
    public Button tileUL_Button, tileUR_Button, tileDL_Button, tileDR_Button;
    public Text tileUL_Text, tileUR_Text, tileDL_Text, tileDR_Text;


    [Header("Entity Controller Section")]
    public GameObject entityController;
    public Image entityPortrait_Image, entityFrame_Image;
    public Text entityName_Text;
    public Button entityLeftArrow_Button, entityRightArrow_Button;
    public Text entityActionCategory_Text;
    public Text entityHealth_Text, entityHunger_Text, entityEnergy_Text;
    public Button entityAbout_Button;
    public Button entityAction1_Button, entityAction2_Button, entityAction3_Button, entityAction4_Button;
    public Text entityAction1_Text, entityAction2_Text, entityAction3_Text, entityAction4_Text;

    [Header("Tile Building Bar Section")]
    public GameObject tileBuilder;




    private void Awake()
    {
        GetAndSetComponents(); // Very Slow that's why one call is enough

        InvokeRepeating("UpdateValues", 0, 0.2f);
    }


    /// <summary>
    /// SLOW AF CALL ONLY ONCE!
    /// </summary>
    private void GetAndSetComponents()
    {
        tileHandling = GameObject.Find("GameManager").GetComponent<TileHandling>();

        tileController = transform.Find("TileController").gameObject;
        tilePortrait_Image = tileController.transform.Find("Portrait").GetComponent<Image>();
        tileFrame_Image = tilePortrait_Image.transform.Find("Frame").GetComponent<Image>();
        tileName_Text = tilePortrait_Image.transform.Find("Name").GetComponent<Text>();
        tileUpgrade_Button = tileController.transform.Find("Upgrade_Button").GetComponent<Button>();
        tileRepair_Button = tileController.transform.Find("Repair_Button").GetComponent<Button>();
        tileCancel_Button = tileController.transform.Find("Stop-Cancel_Button").GetComponent<Button>();
        tileAttack_Button = tileController.transform.Find("Attack_Button").GetComponent<Button>();
        tileUL_Button = tileController.transform.Find("UL_Action_Button").GetComponent<Button>();
        tileUR_Button = tileController.transform.Find("UR_Action_Button").GetComponent<Button>();
        tileDL_Button = tileController.transform.Find("DL_Action_Button").GetComponent<Button>();
        tileDR_Button = tileController.transform.Find("DR_Action_Button").GetComponent<Button>();
        tileUL_Text = tileUL_Button.transform.Find("Text").GetComponent<Text>();
        tileUR_Text = tileUR_Button.transform.Find("Text").GetComponent<Text>();
        tileDL_Text = tileDL_Button.transform.Find("Text").GetComponent<Text>();
        tileDR_Text = tileDR_Button.transform.Find("Text").GetComponent<Text>();

        entityController = transform.Find("EntityController").gameObject;
        entityPortrait_Image = entityController.transform.Find("Portrait").GetComponent<Image>();
        entityFrame_Image = entityPortrait_Image.transform.Find("Frame").GetComponent<Image>();
        entityName_Text = entityPortrait_Image.transform.Find("Name").GetComponent<Text>();
        entityLeftArrow_Button = entityController.transform.Find("Left_Arrow_Button").GetComponent<Button>();
        entityRightArrow_Button = entityController.transform.Find("Right_Arrow_Button").GetComponent<Button>();
        entityActionCategory_Text = entityController.transform.Find("ActionCategory").GetComponent<Text>();
        entityHealth_Text = entityController.transform.Find("Health").Find("Text").GetComponent<Text>();
        entityHunger_Text = entityController.transform.Find("Hunger").Find("Text").GetComponent<Text>();
        entityEnergy_Text = entityController.transform.Find("Energy").Find("Text").GetComponent<Text>();
        entityAbout_Button = entityController.transform.Find("About_Button").GetComponent<Button>();
        entityAction1_Button = entityController.transform.Find("1_Action_Button").GetComponent<Button>();
        entityAction2_Button = entityController.transform.Find("2_Action_Button").GetComponent<Button>();
        entityAction3_Button = entityController.transform.Find("3_Action_Button").GetComponent<Button>();
        entityAction4_Button = entityController.transform.Find("4_Action_Button").GetComponent<Button>();
        entityAction1_Text = entityAction1_Button.transform.Find("Text").GetComponent<Text>();
        entityAction2_Text = entityAction2_Button.transform.Find("Text").GetComponent<Text>();
        entityAction3_Text = entityAction3_Button.transform.Find("Text").GetComponent<Text>();
        entityAction4_Text = entityAction4_Button.transform.Find("Text").GetComponent<Text>();
    }


    private void OpenTileController()
    {

    }


    private void OpenEntityController()
    {

    }


    public void OnClickChanges()
    {
        if (tileHandling.selectedUnit != null &&
            tileHandling.LastSelectedUnit != null &&
            tileHandling.selectedUnit.layer == tileHandling.LastSelectedUnit.layer) return;

        switch (tileHandling.selectedUnit.layer)
        {
            case 8: // Entity
                Debug.Log("Switching To Entity");
                // Animator Trigger Close TileController
                // Animator Trigger Open EntityController
                break;


            case 9: // Tile
                Debug.Log("Switching To Tile");
                // Animator Trigger Open TileController
                // Animator Trigger Close EntityController
                break;
        }
    }


    private void UpdateValues()
    {
        Debug.Log("Values Updated");

        if (!tileHandling.selectedUnit) return;

        switch (tileHandling.selectedUnit.layer)
        {
            case 8: // Entity
                Debug.Log("It's an Entity");
                break;


            case 9: // Tile
                Debug.Log("It's a Tile");

                break;
        }
    }
}
