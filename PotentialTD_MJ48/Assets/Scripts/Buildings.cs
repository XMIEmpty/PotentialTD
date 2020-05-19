using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buildings : MonoBehaviour
{
    public List<Hall> halls;
    public List<Farm> farms;
    public List<MushTree> mushTrees;

    public List<Harvestable> allHarvestables;
    public List<Harvestable> mushTree;
    public List<Harvestable> basicfarm;
    public List<Harvestable> tombstone;

    [SerializeField]
    GameObject grid, groundTM, wallsTM, plantsTM, buildingsTM;

    private void Start()
    {
        grid = GameObject.Find("Grid").gameObject;
        groundTM = grid.transform.GetChild(0).gameObject;
        wallsTM = grid.transform.GetChild(1).gameObject;
        plantsTM = grid.transform.GetChild(2).gameObject;
        buildingsTM = grid.transform.GetChild(3).gameObject;

        for (int i = 0; i < plantsTM.transform.childCount; i++)
        {
            if (plantsTM.transform.GetChild(i).tag == "MushTree")
                mushTrees.Add(plantsTM.transform.GetChild(i).GetComponent<MushTree>());
        }

        for (int i = 0; i < buildingsTM.transform.childCount; i++)
        {
            //if (buildingsTM.transform.GetChild(i).tag == "Hall")
            //    halls.Add(buildingsTM.transform.GetChild(i).GetComponent<Hall>());

            //if (buildingsTM.transform.GetChild(i).tag == "Farm")
            //    farms.Add(buildingsTM.transform.GetChild(i).GetComponent<Farm>());

            //if (buildingsTM.transform.GetChild(i).GetComponent<Harvestable>())
            //    allHarvestables.Add(buildingsTM.transform.GetChild(i).GetComponent<Harvestable>());

            switch(buildingsTM.transform.GetChild(i).GetComponent<Harvestable>().plantTag)
            {
                case "mushTree": mushTree.Add(buildingsTM.transform.GetChild(i).GetComponent<Harvestable>()); break;
                case "basicfarm": basicfarm.Add(buildingsTM.transform.GetChild(i).GetComponent<Harvestable>()); break;
                case "tombstone": tombstone.Add(buildingsTM.transform.GetChild(i).GetComponent<Harvestable>()); break;
            }
        }
    }
}
