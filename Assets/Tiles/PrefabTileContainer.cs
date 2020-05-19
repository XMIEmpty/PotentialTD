using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New PrefabTileContainer", menuName = "TileRelated/PrefabTileContainer")]
public class PrefabTileContainer : ScriptableObject
{
    public Containers[] containers;

    [System.Serializable]
    public class Containers
    {
        public string containerName;
        public GameObject[] containerItems;
    }

    public GameObject GetTile(string tileName)
    {
        for (int i = 0; i < containers.Length; i++)
        {
            for (int j = 0; j < containers[i].containerItems.Length; j++)
            {
                if (containers[i].containerItems[j].name.Equals(tileName))
                {
                    return containers[i].containerItems[j];
                }
            }
        }
        Debug.LogError("Failed Get Tile on Container");

        if (GameManager.current == null) return null;

        return null;
    }
}
