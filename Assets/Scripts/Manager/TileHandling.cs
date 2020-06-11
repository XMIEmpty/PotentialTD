using System.Collections.Generic;
using System.Net.Http.Headers;
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

    public List<GameObject> selectedUnits;


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

        if (selectedTileToBuild) ClearSelectedUnitList();

        // Left_Shift - UP -> Remove Selected Tile
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            if (mouseTileHighlighter.GetComponent<SpriteRenderer>().sprite != defaultTileHighlighter) mouseTileHighlighter.GetComponent<SpriteRenderer>().sprite = defaultTileHighlighter;
            if (selectedTileToBuild) selectedTileToBuild = null;
        }


        // Left_Shift - HOLD DOWN -> Keep Building Selected
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if(selectionHighlighter.activeInHierarchy) selectionHighlighter.SetActive(false);
            
            if (Input.GetMouseButtonDown(0))
            { 
                // LMB - Down -> Select Tile on ground
                if (selectedTileToBuild == null) // Attempt to Select
                {
                    switch (hit.transform.gameObject.layer)
                    {
                        case 8: // Entity

                            break;
                        case 9: // Tile

                            /*if (hits[i].transform.gameObject != selectedUnit)*/
                            var tileHit = hit.transform.gameObject;
                            lastSelectedUnit = selectedUnit;
                            selectedUnit = tileHit;

                            // Is a Building
                            if (tileHit.TryGetComponent<A_Building>(out var aBuilding))
                            {
                                switch (tileHit.transform.GetChild(0).gameObject.activeInHierarchy)
                                {
                                    case true:
                                        
                                        // Remove from list
                                        tileHit.transform.GetChild(0).gameObject.SetActive(false);
                                        RemoveFromSelectedUnitList(GetSelectedUnitFromList(tileHit));
                                        
                                        break;
                                    case false:

                                        // If list contains at least one item
                                        if (selectedUnits.Count > 0)
                                        {
                                            // Has same name like first tile from Selection list
                                            if (aBuilding.tileName ==
                                                selectedUnits[0].GetComponent<A_Building>().tileName)
                                            {
                                                // Add to list
                                                tileHit.transform.GetChild(0).gameObject.SetActive(true);
                                                selectedUnits.Add(tileHit);
                                            }
                                        }
                                        else
                                        {
                                            tileHit.transform.GetChild(0).gameObject.SetActive(true);
                                            selectedUnits.Add(tileHit);
                                        }

                                        break;
                                }
                            }
                            

                            // if (hit.transform.parent == buildingsTm.transform)
                            // {
                            //     //var interactable = hits[i].transform.GetComponent<IInteractable>(); /*buildingsTilemap.GetInstantiatedObject(cellPosition).GetComponent<IInteractable>();*/
                            //     //if (interactable == null) return;
                            //     //interactable.Interact();
                            //     canvasComponents.tileActionControlGuiScript.CreateButtonList();
                            //     selectedUnit.GetComponent<A_Building>().SetBasicButtonsAttributes();
                            //     canvasComponents.OnClickChanges();
                            //
                            //     if (!selectionHighlighter.activeInHierarchy) selectionHighlighter.SetActive(true);
                            //     selectionHighlighter.GetComponent<SpriteRenderer>().color =
                            //         new Color(251f / 255f, 245f / 255f, 255f / 255f, 255f / 255f);
                            //     //Debug.Log("Color Updated To\n\r" + selectionHighlighter.GetComponent<SpriteRenderer>().color);
                            // }

                            break;
                        case 10: // Ground
                            
                            // /*if (hits[i].transform.gameObject != selectedUnit && selectedUnit != null)*/
                            // lastSelectedUnit = selectedUnit;
                            // selectedUnit = null;
                            //
                            // canvasComponents.OnClickChanges();
                            //
                            // if (selectionHighlighter.activeInHierarchy) selectionHighlighter.SetActive(false);
                            // Debug.Log("Ground has been hit");

                            break;
                    }
                }

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
            if (selectedUnits.Count > 0) ClearSelectedUnitList(); 

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
                        if (selectedUnits.Count > 0) ClearSelectedUnitList();
                        
                        lastSelectedUnit = selectedUnit;
                        selectedUnit = null;

                        canvasComponents.OnClickChanges();

                        if (selectionHighlighter.activeInHierarchy) selectionHighlighter.SetActive(false);
                        Debug.Log("Ground has been hit");

                        break;
                }
            }

            // LMB - Down -> Build One Tile
            if (selectedTileToBuild)   // Attempt to Build
            {
                if (hit)
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
                            Instantiate(selectedTileToBuild, grid.GetCellCenterLocal(cellPosition), Quaternion.identity)
                                .transform.SetParent(buildingsTm.transform);
                            mouseTileHighlighter.GetComponent<SpriteRenderer>().sprite = defaultTileHighlighter;

                            resourceBarManager.SubtractAll(selectedTileToBuild.GetComponent<A_Building>().mushLogCosts,
                                selectedTileToBuild.GetComponent<A_Building>().soulCosts,
                                selectedTileToBuild.GetComponent<A_Building>().foodCosts);
                            selectedTileToBuild = null;

                            break;
                    }
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
                    
                    // Hit is our selected tile
                    if (hit.transform.gameObject == selectedUnit)
                    { 
                        // Its a building
                        if (hit.transform.TryGetComponent<A_Building>(out var aBuilding))
                        {
                            var resources = aBuilding.allCosts;
                            resourceBarManager.AddAll(resources.x, resources.y, resources.z);

                            lastSelectedUnit = selectedUnit;
                            selectedUnit = null;
                            canvasComponents.OnClickChanges();


                            if (selectionHighlighter.activeInHierarchy) selectionHighlighter.SetActive(false);
                            Destroy(hit.transform.gameObject);
                        }
                    }

                    // Hit isn't our selected tile
                    if (hit.transform.gameObject != selectedUnit)
                    {
                        // Its a building
                        if (hit.transform.TryGetComponent<A_Building>(out var aBuilding))
                        {
                            var resources = aBuilding.allCosts;
                            resourceBarManager.AddAll(resources.x, resources.y, resources.z);
                            
                            Destroy(hit.transform.gameObject);
                        }
                    }

                    
                    // var resources = selectedUnit.GetComponent<A_Building>().allCosts;
                    // resourceBarManager.AddAll(resources.x, resources.y, resources.z);
                    Destroy(hit.transform.gameObject);

                    break;
                case 10: // Ground
                    break;
            }
            return;
        }
    }


    public GameObject GetSelectedUnitFromList(GameObject gameObjectToLookFor)
    {
        for (var i = selectedUnits.Count - 1; i >= 0; i--)
        {
            if (selectedUnits[i] == gameObjectToLookFor)
            {
                return selectedUnits[i];
            }
        }

        return null;
    }

    public void RemoveFromSelectedUnitList(GameObject gameObjectToRemove)
    {
        for (var i = selectedUnits.Count - 1; i >= 0; i--)
        {
            if (selectedUnits[i] == gameObjectToRemove)
            {
                selectedUnits.RemoveAt(i);
            } 
        }
    }

    public void ClearSelectedUnitList()
    {
        for (var i = selectedUnits.Count - 1; i >= 0; i--)
        {
            selectedUnits[i].transform.GetChild(0).gameObject.SetActive(false);
            selectedUnits.Remove(selectedUnits[i]);
        }
    }
}

/*if (Input.GetMouseButton(0))
{
    Future Feature... DRAG Click to select multiple

    // Interesting to remember that... buildingsTilemap.HasTile(cellPosition) == true

}
*/
