using UnityEngine;
using UnityEngine.Tilemaps;

public class Gate : MonoBehaviour, IInteractable
{
    public int health;

    public bool isOpen;

    [SerializeField]
    private TileHandling tileHandler;


    void Start()
    {
        tileHandler = GameObject.Find("GameManager").GetComponent<TileHandling>();
        //isOpen = false;
        Debug.LogError("Start  " + tileHandler);
        
    }


    public void Interact()
    {
        //Switch();
    }


    //public void Switch()
    //{
    //    var tilePos = tileHandler.cellPosition;

    //    if (isOpen /*&& tile != null*/)
    //    {
    //        Debug.LogError(closedGate.name);
    //        tileHandler.buildingsTilemap.SetTile(tilePos, closedGate);

    //        isOpen = false;
    //    }
    //    else
    //    {
    //        Debug.LogError(openedGate.name);
    //        tileHandler.buildingsTilemap.SetTile(tilePos, openedGate);

    //        isOpen = true;
    //    }


        //if (currentTile != null && !currentTile.name.Equals("Gate") /*&& tile != null*/)
        //{
        //    //Debug.LogError("ClosedGateTile: BEFORE SET " + currentTile.name);
        //    currentTile = tileHandler.tilesContainer.GetTile("Gate");

        //    GetComponent<Tilemap>().SetTile(tilePos, currentTile);
        //    //tileHandler.buildingsTilemap.SetTile(tilePos, currentTile);
        //    //Debug.LogError("ClosedGateTile: AFTER SET " + currentTile.name);
        //    //Instantiate(particleSpawned, transform.position + new Vector3(0f, 3f, 0f), Quaternion.identity);

        //    isOpen = false;
        //}
        //else
        //{
        //    //if (currentTile) Debug.LogError("OpenGateTile: Before SET " + currentTile.name);
        //    currentTile = tileHandler.tilesContainer.GetTile("OpenGate");

        //    GetComponent<Tilemap>().SetTile(tilePos, currentTile);
        //    //tileHandler.buildingsTilemap.SetTile(tilePos, currentTile);
        //    //Debug.LogError("OpenGateTile: AFTER SET " + currentTile.name);
        //    //Instantiate(particleSpawned, transform.position, Quaternion.identity);

        //    isOpen = true;
        //}

        //tileHandler.buildingsTilemap.RefreshTile(tileHandler.cellPosition);
        //tileHandler.buildingsTilemap.RefreshTile(tileHandler.cellPosition + new Vector3Int(1, 1, 0)); // Top Left
        //tileHandler.buildingsTilemap.RefreshTile(tileHandler.cellPosition + new Vector3Int(0, 1, 0));
        //tileHandler.buildingsTilemap.RefreshTile(tileHandler.cellPosition + new Vector3Int(-1, 1, 0));
        //tileHandler.buildingsTilemap.RefreshTile(tileHandler.cellPosition + new Vector3Int(1, 0, 0));
        //tileHandler.buildingsTilemap.RefreshTile(tileHandler.cellPosition + new Vector3Int(-1, 0, 0));
        //tileHandler.buildingsTilemap.RefreshTile(tileHandler.cellPosition + new Vector3Int(-1, -1, 0));
        //tileHandler.buildingsTilemap.RefreshTile(tileHandler.cellPosition + new Vector3Int(0, -1, 0));
        //tileHandler.buildingsTilemap.RefreshTile(tileHandler.cellPosition + new Vector3Int(-1, -1, 0)); // Buttom Right
    //}
}

//{
//TileBase tile = tileHandler.buildingsTilemap.GetTile(tilePos);
//var somethin = tileHandler.buildingsTilemap.SetTile(tileHandler.cellPosition, tileHandler.tilesContainer.GetTile("Gate"));
//Vector3Int tilePos = tileHandler.buildingsTilemap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
//}