using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "New TileContainer", menuName = "TileRelated/Container")]
public class TilesContainer : ScriptableObject
{
    [SerializeField]
    private GameObject particlecase;




    public Containers[] containers;

    [System.Serializable]
    public class Containers
    {
        public string categoryName;
        public RuleTileSiblings[] categoryItems;
    }

    public RuleTileSiblings GetTile(string tileName)
    {
        for (int i = 0; i < containers.Length; i++)
        {
            for (int j = 0; j < containers[i].categoryItems.Length; j++)
            {
                if (containers[i].categoryItems[j].name.Equals(tileName))
                {
                    return containers[i].categoryItems[j];
                }
            }
        }
        Debug.LogError("Failed Get Tile on Container");
        //Instantiate(particlecase, GameManager.current.posToInstantiate.position, Quaternion.identity);

        if (GameManager.current == null) return null;

        return null;
    }
}
