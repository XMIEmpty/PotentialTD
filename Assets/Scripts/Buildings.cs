using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buildings : MonoBehaviour
{
    public List<Farm> farms;
    public List<MushTree> mushTrees;

    public List<A_Building> allBuildings;
    public Hall mainHall;
    //public List<A_Building> Tent;
    //public List<A_Building> Cabin;

    public List<Harvestable> allHarvestables;
    public List<IdleGrownHarvestable> allIdleHarvestables;
    public List<Harvestable> mushTree;
    public List<Harvestable> basicfarm;
    public List<Harvestable> tombstone;

    [SerializeField]
    GameObject grid, groundTM, wallsTM, plantsTM, buildingsTM;


    private void Awake()
    {
        grid = GameObject.Find("Grid").gameObject;
        groundTM = grid.transform.Find("Grounds").gameObject;
        wallsTM = grid.transform.Find("Walls").gameObject;
        plantsTM = grid.transform.Find("Plants").gameObject;
        buildingsTM = grid.transform.Find("Buildings").gameObject;

        for (int i = 0; i < plantsTM.transform.childCount; i++)
        {
            if (plantsTM.transform.GetChild(i).GetComponent<Harvestable>())
                allHarvestables.Add(plantsTM.transform.GetChild(i).GetComponent<Harvestable>());

            if (plantsTM.transform.GetChild(i).GetComponent<IdleGrownHarvestable>())
                allIdleHarvestables.Add(plantsTM.transform.GetChild(i).GetComponent<IdleGrownHarvestable>());


            //switch (plantsTM.transform.GetChild(i).GetComponent<Harvestable>().plantTag)
            //{
            //    case "mushTree": mushTree.Add(plantsTM.transform.GetChild(i).GetComponent<Harvestable>()); break;
            //    case "basicfarm": basicfarm.Add(plantsTM.transform.GetChild(i).GetComponent<Harvestable>()); break;
            //    case "tombstone": tombstone.Add(plantsTM.transform.GetChild(i).GetComponent<Harvestable>()); break;
            //}
        }

        for (int i = 0; i < buildingsTM.transform.childCount; i++)
        {
            if (buildingsTM.transform.GetChild(i).GetComponent<Hall>() && mainHall == null)
                mainHall = buildingsTM.transform.GetChild(i).GetComponent<Hall>();

            if (buildingsTM.transform.GetChild(i).GetComponent<A_Building>())
                    allBuildings.Add(buildingsTM.transform.GetChild(i).GetComponent<A_Building>());
        }
    }


    public void AddIdleHarvy(IdleGrownHarvestable idleHarvy)
    {

    }
}
