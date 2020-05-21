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


    private GameObject GetPrefabTile(string containsInName)
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


    private void GetAndProcessPrefabData(string containsInName)
    {
        GameObject foundTile = GetPrefabTile(containsInName);
        tilesHandler.selectedTileToBuild = foundTile;
        tilesHandler.mouseTileHighlighter.GetComponent<SpriteRenderer>().sprite = foundTile.GetComponent<SpriteRenderer>().sprite;
    }


    public void GetTent() => GetAndProcessPrefabData("Tent");
    public void GetMushLogCabin() => GetAndProcessPrefabData("MushLogCabin");
    public void GetFarm() => GetAndProcessPrefabData("Farm");
    public void GetWall() => GetAndProcessPrefabData("Wall");
    public void GetGate() => GetAndProcessPrefabData("Gate");


    //public void GetTileByName(string tileName) => GetAndProcessPrefabData(tileName);
    ////{
    ////    GetComponent<TileHandling>().selectedTile = gate;
    ////    GetComponent<TileHandling>().tileSelectioning.
    ////        GetComponent<SpriteRenderer>().sprite = gate.m_DefaultSprite;
    ////}
}
