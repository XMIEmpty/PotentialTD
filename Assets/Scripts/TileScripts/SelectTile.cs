using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


/// <summary>
/// It's automatic, on the process, it just needs 3 steps <para></para>
/// Step 1: Add the tile you've made in the tilesContainer (the scriptable object ofc.) <para></para>
/// Step 2: Add the method public void GetItemName() => GetAndProcessTile("TileNameFromStep_1"); <para></para>
/// Step 3: Add a button component or any other trigger to your ui el. & Add the GO holding this Script & Select on the "No Function" dropdown your method name from step 2
/// </summary>
public class SelectTile : MonoBehaviour
{
    private TileHandling tilesHandler;


    private void Start()
    {
        tilesHandler = GetComponent<TileHandling>();
    }


    public GameObject GetPrefabTile(string containsInName)
    {
        for (int i = 0; i < tilesHandler.prefabTileContainer.containers.Length; i++)
        {
            for (int j = 0; j < tilesHandler.prefabTileContainer.containers[i].containerItems.Length; j++)
            {
                if (tilesHandler.prefabTileContainer.containers[i].containerItems[j].name.Equals(containsInName))
                {
                    return tilesHandler.prefabTileContainer.containers[i].containerItems[j];
                }
            }
        }
        return null;
    }


    public void GetAndProcessPrefabData(string containsInName)
    {
        GameObject foundTile = GetPrefabTile(containsInName);
        tilesHandler.selectedTileToBuild = foundTile;
        tilesHandler.mouseTileHighlighter.GetComponent<SpriteRenderer>().sprite = foundTile.GetComponent<SpriteRenderer>().sprite;
    }


    public void GetAndProcessPrefabInfo(string containsInName)
    {
        GameObject foundTile = GetPrefabTile(containsInName);
        A_Building a_Building = foundTile.GetComponent<A_Building>();
        tilesHandler.canvasComponents.infoBoxText.text = a_Building.infoBox;
        tilesHandler.canvasComponents.infoBoxGo.GetComponent<Animator>().SetTrigger("Open_InfoBox");

        // Set Costs Values and Open that as well
        SetCosts(a_Building);
        tilesHandler.canvasComponents.costsBoxGo.SetActive(true);
    }


    public void SetCosts(A_Building a_Building)
    {
        tilesHandler.canvasComponents.mushLogCostsText.text = a_Building.mushLogCosts.ToString();
        tilesHandler.canvasComponents.soulCostsText.text = a_Building.soulCosts.ToString();
        tilesHandler.canvasComponents.foodCostsText.text = a_Building.foodCosts.ToString();
    }


    public void EmptyAndClosePrefabInfo()
    {
        tilesHandler.canvasComponents.infoBoxText.text = "";
        tilesHandler.canvasComponents.infoBoxGo.GetComponent<Animator>().SetTrigger("Close_InfoBox");
        // Empty Costs Values and Close that as well
        tilesHandler.canvasComponents.costsBoxGo.SetActive(false);
    }


    public void GetTent() => GetAndProcessPrefabData("Tent"); public void GetTent_InfoCosts() => GetAndProcessPrefabInfo("Tent");

    public void GetMushLogCabin() => GetAndProcessPrefabData("MushLogCabin"); public void GetMushLogCabin_InfoCosts() => GetAndProcessPrefabInfo("MushLogCabin");

    public void GetFarm() => GetAndProcessPrefabData("Farm"); public void GetFarm_InfoCosts() => GetAndProcessPrefabInfo("Farm");

    public void GetWall() => GetAndProcessPrefabData("Wall"); public void GetWall_InfoCosts() => GetAndProcessPrefabInfo("Wall");

    public void GetGate() => GetAndProcessPrefabData("Gate"); public void GetGate_InfoCosts() => GetAndProcessPrefabInfo("Gate");
    public void GetHall() => GetAndProcessPrefabData("Hall"); public void GetHall_InfoCosts() => GetAndProcessPrefabInfo("Hall");



    //public void GetTileByName(string tileName) => GetAndProcessPrefabData(tileName);
    ////{
    ////    GetComponent<TileHandling>().selectedTile = gate;
    ////    GetComponent<TileHandling>().tileSelectioning.
    ////        GetComponent<SpriteRenderer>().sprite = gate.m_DefaultSprite;
    ////}
}
