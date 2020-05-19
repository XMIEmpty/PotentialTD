using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;


public class TileHandling : MonoBehaviour
{
    [SerializeField]
    Grid grid;
    public CanvasComponents canvasComponents;
    public List<Tilemap> alltilemaps;

    private Vector3Int cellPosition;

    public TilesContainer tilesContainer;
    public PrefabTileContainer prefabTileContainer;

    public Sprite defaultTileHighlighter;
    public TileBase selectedTileToBuild;
    public Tilemap buildingsTilemap;

    [SerializeField]
    public GameObject mouseTileHighlighter;
    public GameObject selectionHighlighter;

    /// <summary>
    /// Unit refers to any Entity, Tile or other that has been selected.
    /// </summary>
    public GameObject selectedUnit;
    public GameObject LastSelectedUnit;

    public GameObject[] selectedUnits, LastSelectedUnits;

    void Awake()
    {
        grid = FindObjectOfType<Grid>();
        foreach (Tilemap tmap in grid.transform.GetComponentsInChildren<Tilemap>())
            alltilemaps.Add(tmap);

        canvasComponents = GameObject.Find("Canvas").GetComponent<CanvasComponents>();
    }


    void Update()
    {
        //Processing
        Vector3 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 10));
        
        if (EventSystem.current.IsPointerOverGameObject()) return;
        
        cellPosition = grid.LocalToCell(clickPosition);
        //clickPosition = grid.GetCellCenterLocal(cellPosition);
        mouseTileHighlighter.transform.position = grid.GetCellCenterLocal(cellPosition);



        // Left_Shift - UP -> Remove Selected Tile
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            mouseTileHighlighter.GetComponent<SpriteRenderer>().sprite = defaultTileHighlighter;
            selectedTileToBuild = null;
        }

        // Left_Shift - HOLD DOWN -> Keep Building Selected
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit2D[] hits = Physics2D.RaycastAll(clickPosition, Vector2.zero);
                for (int i = 0; i < hits.Length; i++)
                {


                    if (hits[i])
                    {
                        Debug.Log("I can't build there");
                    }

                    if (!hits[i])
                    {
                        if (!selectedTileToBuild) return;

                        buildingsTilemap.SetTile(cellPosition, selectedTileToBuild);
                        //tileSelectioning.GetComponent<SpriteRenderer>().sprite = defaultTileHighlighter;
                    }


                }
            }
        }



        if (!Input.GetKey(KeyCode.LeftShift) &&  Input.GetMouseButtonDown(0))
        {
            RaycastHit2D[] hits = Physics2D.RaycastAll(clickPosition, Vector2.zero);
            for (int i = 0; i < hits.Length; i++)
            {

                // LMB - Down -> Select Tile on ground
                if (selectedTileToBuild == null)
                /*buildingsTilemap.HasTile(cellPosition) == true*/
                {
                    //switch (hits[i].transform.gameObject.layer)
                    //{
                    //    case 8: // Entity

                    //        break;
                    //    case 9: // Tile

                    //        break;
                    //}



                    if (hits[i].transform.parent == buildingsTilemap.transform)
                    {
                        LastSelectedUnit = selectedUnit;

                        var interactable = hits[i].transform.GetComponent<IInteractable>(); /*buildingsTilemap.GetInstantiatedObject(cellPosition).GetComponent<IInteractable>();*/
                        if (interactable == null) return;
                        interactable.Interact();
                        selectedUnit = hits[i].transform.gameObject;
                        canvasComponents.OnClickChanges();

                        if (!selectionHighlighter.activeInHierarchy) selectionHighlighter.SetActive(true);
                        selectionHighlighter.transform.position = selectedUnit.transform.position;
                    }
                }

                // LMB - Down -> Build One Tile
                if (selectedTileToBuild)
                {
                    if (hits[i])
                    {
                        Debug.Log("I can't build there");
                    }

                    if (!hits[i])
                    {
                        Instantiate(selectedTileToBuild, grid.GetCellCenterLocal(cellPosition), Quaternion.identity);
                        //buildingsTilemap.SetTile(cellPosition, selectedTileToBuild);
                        //Instantiate(SelectedObjToInstantiate, clickPosition, Quaternion.identity);
                        mouseTileHighlighter.GetComponent<SpriteRenderer>().sprite = defaultTileHighlighter;
                        selectedTileToBuild = null;
                    }
                }
            }
        }

        // Right-MouseClick -> Remove Existing Tile
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit2D[] hits = Physics2D.RaycastAll(clickPosition, Vector2.zero);
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i] && hits[i].transform.gameObject.layer == 9) // Is it a tile
                {
                    Destroy(hits[i].transform.gameObject);
                    break;
                }
            }
        }


        //RaycastHit2D[] hits = Physics2D.RaycastAll(clickPosition, Vector2.zero);
        //for (int i = 0; i < hits.Length; i++)
        //{
        //    if (hits[i].transform.parent == buildingsTilemap.transform)
        //    {
        //        Debug.LogError(hits[i].transform.name);
        //    }

        //    //if (hits[i].transform.parent == )
        //    //{

        //    //}
        //}
        

        //for (int i = 0; i < hits.Length; i++)
        //{
        //    switch (hits[i].transform.gameObject.layer)
        //    {
        //        case 5:
        //            break; // UI

        //        case 8:
        //            break; // Entity

        //        case 9:
        //            break; // Building

        //        default:
        //            break;
        //    }

        //    if (Input.GetMouseButtonDown(0))
        //    {
        //        if (hits[i].transform.GetComponent<Harvestable>())
        //        {

        //        }
        //    }

        //    if (hits[i].collider.name != "Ground")
        //    {

        //    }

        //    Debug.Log(hits[i].collider.name);
        //}

            

    }
}

//if (Input.GetMouseButton(0))
//{
//    // Future Feature... DRAG Click to select multiple
//}
