using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;
using UnityEngine.UI;


public class TileHandling : MonoBehaviour
{
    [SerializeField]
    Grid grid;
    public CanvasComponents canvasComponents;

    private Vector3Int cellPosition;

    public TilesContainer tilesContainer;
    public SelectTile selectTile;
    public PrefabTileContainer prefabTileContainer;


    [SerializeField] private Sprite defaultTileHighlighter;
    public GameObject selectedTileToBuild;
    public GameObject buildingsTM, plantsTM, wallsTM, groundsTM;

    [SerializeField]
    public GameObject mouseTileHighlighter;
    public GameObject selectionHighlighter;

    /// <summary>
    /// Unit refers to any Entity, Tile or other that has been selected.
    /// </summary>
    public GameObject selectedUnit;
    public GameObject LastSelectedUnit;

    public GameObject[] selectedUnits, LastSelectedUnits;


    int x = Screen.width / 2;
    int y = Screen.height / 2;


    void Awake()
    {
        selectTile = GetComponent<SelectTile>();

        grid = FindObjectOfType<Grid>();
        buildingsTM = grid.transform.Find("Buildings").gameObject;
        plantsTM = grid.transform.Find("Plants").gameObject;
        wallsTM = grid.transform.Find("Walls").gameObject;
        groundsTM = grid.transform.Find("Grounds").gameObject;

        canvasComponents = GameObject.Find("Canvas").GetComponent<CanvasComponents>();
    }


    void Update()
    {
        //Processing
        Vector3 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 10));

        if (EventSystem.current.IsPointerOverGameObject()) // DON'T CONTINUE IF MOUSE OVER (G)UI 
        {
            mouseTileHighlighter.SetActive(false);
            //EventSystem.current.currentSelectedGameObject.GetComponent<sdasdasd0>();


            return;
        }

        cellPosition = grid.LocalToCell(clickPosition);
        if (!mouseTileHighlighter.activeInHierarchy) mouseTileHighlighter.SetActive(true);
        mouseTileHighlighter.transform.position = grid.GetCellCenterLocal(cellPosition);

        RaycastHit2D hit = Physics2D.Raycast(clickPosition, Vector2.zero);


        if (selectedUnit != null) selectionHighlighter.transform.position = selectedUnit.transform.position;



        // Left_Shift - UP -> Remove Selected Tile
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            if (mouseTileHighlighter.GetComponent<SpriteRenderer>().sprite != defaultTileHighlighter) mouseTileHighlighter.GetComponent<SpriteRenderer>().sprite = defaultTileHighlighter;
            if (selectedTileToBuild) selectedTileToBuild = null;
        }


        // Left_Shift - HOLD DOWN -> Keep Building Selected
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (selectedTileToBuild) // Attempt to Build
                {
                    switch (hit.transform.gameObject.layer)
                    {
                        case 8: // Entity



                            break;
                        case 9: // Tile

                            Debug.Log("Can't Build there [Shift],\n\r hit " + hit.transform.gameObject.name);

                            break;
                        case 10: // Ground

                            Debug.Log("Build [Shift],\n\r hit " + hit.transform.gameObject.name);
                            Instantiate(selectedTileToBuild, grid.GetCellCenterLocal(cellPosition), Quaternion.identity).transform.SetParent(buildingsTM.transform);

                            break;
                    }
                }
            }
            return;
        }


        if (!Input.GetKey(KeyCode.LeftShift) && Input.GetMouseButtonDown(0))
        {
            // LMB - Down -> Select Tile on ground
            if (selectedTileToBuild == null) // Attempt to Select
            {
                switch (hit.transform.gameObject.layer)
                {
                    case 8: // Entity

                        /*if (hits[i].transform.gameObject != selectedUnit)*/
                        LastSelectedUnit = selectedUnit;
                        selectedUnit = hit.transform.gameObject;

                        canvasComponents.OnClickChanges();
                        if (!selectionHighlighter.activeInHierarchy) selectionHighlighter.SetActive(true);
                        selectionHighlighter.GetComponent<SpriteRenderer>().color = new Color(242f / 255f, 211f / 255f, 171f / 255f, 255f / 255f);
                        //Debug.Log("Color Updated To\n\r" + selectionHighlighter.GetComponent<SpriteRenderer>().color);
                        //if (selectionHighlighter.activeInHierarchy) selectionHighlighter.SetActive(false);

                        break;
                    case 9: // Tile

                        /*if (hits[i].transform.gameObject != selectedUnit)*/
                        LastSelectedUnit = selectedUnit;
                        selectedUnit = hit.transform.gameObject;

                        if (hit.transform.parent == buildingsTM.transform)
                        {

                            //var interactable = hits[i].transform.GetComponent<IInteractable>(); /*buildingsTilemap.GetInstantiatedObject(cellPosition).GetComponent<IInteractable>();*/
                            //if (interactable == null) return;
                            //interactable.Interact();
                            canvasComponents.tileActionControlGuiScript.CreateButtonList();
                            canvasComponents.OnClickChanges();

                            if (!selectionHighlighter.activeInHierarchy) selectionHighlighter.SetActive(true);
                            selectionHighlighter.GetComponent<SpriteRenderer>().color = new Color(251f / 255f, 245f / 255f, 255f / 255f, 255f / 255f);
                            //Debug.Log("Color Updated To\n\r" + selectionHighlighter.GetComponent<SpriteRenderer>().color);
                        }

                        break;
                    case 10: // Ground

                        /*if (hits[i].transform.gameObject != selectedUnit && selectedUnit != null)*/
                        LastSelectedUnit = selectedUnit;
                        selectedUnit = null;

                        canvasComponents.OnClickChanges();

                        if (selectionHighlighter.activeInHierarchy) selectionHighlighter.SetActive(false);
                        Debug.Log("Ground has been hit");

                        break;
                }
            }

            // LMB - Down -> Build One Tile
            if (selectedTileToBuild && hit)   // Attempt to Build
            {
                switch (hit.transform.gameObject.layer)
                {
                    case 8: // Entity



                        break;
                    case 9: // Tile

                        Debug.Log("Can't Build there,\n\r hit " + hit.transform.gameObject.name);

                        break;
                    case 10: // Ground

                        Debug.Log("Build,\n\r hit " + hit.transform.gameObject.name);
                        Instantiate(selectedTileToBuild, grid.GetCellCenterLocal(cellPosition), Quaternion.identity).transform.SetParent(buildingsTM.transform);
                        mouseTileHighlighter.GetComponent<SpriteRenderer>().sprite = defaultTileHighlighter;
                        selectedTileToBuild = null;

                        break;
                }
            }
            return;
        }


        // Right-MouseClick -> Remove Existing Tile
        if (Input.GetMouseButtonDown(1))
        {
            switch (hit.transform.gameObject.layer)
            {
                case 8: // Entity
                    break;
                case 9: // Tile

                    Destroy(hit.transform.gameObject);

                    break;
                case 10: // Ground
                    break;
            }
            return;
        }
    }
}

/*if (Input.GetMouseButton(0))
{
    Future Feature... DRAG Click to select multiple

    // Interesting to remember that... buildingsTilemap.HasTile(cellPosition) == true

}
*/
