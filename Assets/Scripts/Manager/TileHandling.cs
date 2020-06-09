using UnityEngine;
using UnityEngine.EventSystems;


public class TileHandling : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Grid grid;

    public ResourceBarManager resourceBarManager;
    public CanvasComponents canvasComponents;

    private Vector3Int cellPosition;

    public PrefabTileContainer prefabTileContainer;

    [SerializeField] private Sprite defaultTileHighlighter;
    public GameObject selectedTileToBuild;
    public GameObject buildingsTm, plantsTm, wallsTm, groundsTm;

    [SerializeField]
    public GameObject mouseTileHighlighter;
    public GameObject selectionHighlighter;

    /// <summary>
    /// Unit refers to any Entity, Tile or other that has been selected.
    /// </summary>
    public GameObject selectedUnit;
    public GameObject lastSelectedUnit;

    public GameObject[] selectedUnits, lastSelectedUnits;


    void Awake()
    {
        grid = FindObjectOfType<Grid>();
        buildingsTm = grid.transform.Find("Buildings").gameObject;
        plantsTm = grid.transform.Find("Plants").gameObject;
        wallsTm = grid.transform.Find("Walls").gameObject;
        groundsTm = grid.transform.Find("Grounds").gameObject;

        resourceBarManager = GetComponent<ResourceBarManager>();
        canvasComponents = GameObject.Find("Canvas").GetComponent<CanvasComponents>();
    }
    
    
    public void OnPointerClick(PointerEventData eventData)
    {
        
        if (eventData.button == PointerEventData.InputButton.Left)
            Debug.Log("Left click");
        else if (eventData.button == PointerEventData.InputButton.Middle)
            Debug.Log("Middle click");
        else if (eventData.button == PointerEventData.InputButton.Right)
            Debug.Log("Right click");
    }

    
    void Update()
    {
        //Processing
        var clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 10));

        if (EventSystem.current.IsPointerOverGameObject()) // DON'T CONTINUE IF MOUSE OVER (G)UI 
        {
            // OnPointerClick();
                
            mouseTileHighlighter.SetActive(false);

            if (Input.GetMouseButtonDown(0)) { return; }
            if (Input.GetMouseButtonDown(1)) { return; }
            if (Input.GetMouseButtonDown(2)) { return; }

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

                            if (resourceBarManager.GetMushLogAmount() <
                                selectedTileToBuild.GetComponent<A_Building>().mushLogCosts ||
                                resourceBarManager.GetSoulAmount() <
                                selectedTileToBuild.GetComponent<A_Building>().soulCosts ||
                                resourceBarManager.GetFoodAmount() <
                                selectedTileToBuild.GetComponent<A_Building>().foodCosts)
                            {
                                if (mouseTileHighlighter.GetComponent<SpriteRenderer>().sprite != defaultTileHighlighter
                                ) mouseTileHighlighter.GetComponent<SpriteRenderer>().sprite = defaultTileHighlighter;
                                if (selectedTileToBuild) selectedTileToBuild = null;
                                return;
                            }


                            Debug.Log("Build [Shift],\n\r hit " + hit.transform.gameObject.name);
                            Instantiate(selectedTileToBuild, grid.GetCellCenterLocal(cellPosition), Quaternion.identity).transform.SetParent(buildingsTm.transform);
                            resourceBarManager.SubtractAll(selectedTileToBuild.GetComponent<A_Building>().mushLogCosts,
                                selectedTileToBuild.GetComponent<A_Building>().soulCosts,
                                selectedTileToBuild.GetComponent<A_Building>().foodCosts);
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
                        lastSelectedUnit = selectedUnit;
                        selectedUnit = hit.transform.gameObject;

                        canvasComponents.OnClickChanges();
                        if (!selectionHighlighter.activeInHierarchy) selectionHighlighter.SetActive(true);
                        selectionHighlighter.GetComponent<SpriteRenderer>().color = new Color(242f / 255f, 211f / 255f, 171f / 255f, 255f / 255f);
                        //Debug.Log("Color Updated To\n\r" + selectionHighlighter.GetComponent<SpriteRenderer>().color);
                        //if (selectionHighlighter.activeInHierarchy) selectionHighlighter.SetActive(false);

                        break;
                    case 9: // Tile

                        /*if (hits[i].transform.gameObject != selectedUnit)*/
                        lastSelectedUnit = selectedUnit;
                        selectedUnit = hit.transform.gameObject;

                        if (hit.transform.parent == buildingsTm.transform)
                        {

                            //var interactable = hits[i].transform.GetComponent<IInteractable>(); /*buildingsTilemap.GetInstantiatedObject(cellPosition).GetComponent<IInteractable>();*/
                            //if (interactable == null) return;
                            //interactable.Interact();
                            canvasComponents.tileActionControlGuiScript.CreateButtonList();
                            selectedUnit.GetComponent<A_Building>().SetBasicButtonsAttributes();
                            canvasComponents.OnClickChanges();

                            if (!selectionHighlighter.activeInHierarchy) selectionHighlighter.SetActive(true);
                            selectionHighlighter.GetComponent<SpriteRenderer>().color = new Color(251f / 255f, 245f / 255f, 255f / 255f, 255f / 255f);
                            //Debug.Log("Color Updated To\n\r" + selectionHighlighter.GetComponent<SpriteRenderer>().color);
                        }

                        break;
                    case 10: // Ground

                        /*if (hits[i].transform.gameObject != selectedUnit && selectedUnit != null)*/
                        lastSelectedUnit = selectedUnit;
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

                        // Debug.Log("Build,\n\r hit " + hit.transform.gameObject.name);
                        Instantiate(selectedTileToBuild, grid.GetCellCenterLocal(cellPosition), Quaternion.identity).transform.SetParent(buildingsTm.transform);
                        mouseTileHighlighter.GetComponent<SpriteRenderer>().sprite = defaultTileHighlighter;
                        
                        resourceBarManager.SubtractAll(selectedTileToBuild.GetComponent<A_Building>().mushLogCosts,
                            selectedTileToBuild.GetComponent<A_Building>().soulCosts,
                            selectedTileToBuild.GetComponent<A_Building>().foodCosts);
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
                    
                    if (hit.transform.gameObject == selectedUnit)
                    {                    
                        lastSelectedUnit = selectedUnit;
                        selectedUnit = null;
                        canvasComponents.OnClickChanges();
                    }



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
