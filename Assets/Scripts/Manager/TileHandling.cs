using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;


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

    [SerializeField] public GameObject mouseTileHighlighter;
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
            mouseTileHighlighter.SetActive(false);
            return;
        }

        cellPosition = grid.LocalToCell(clickPosition);
        if (!mouseTileHighlighter.activeInHierarchy) mouseTileHighlighter.SetActive(true);
        mouseTileHighlighter.transform.position = grid.GetCellCenterLocal(cellPosition);

        var hit = Physics2D.Raycast(clickPosition, Vector2.zero);

        if (selectedTileToBuild)
        {
            ClearSelectedUnitList();
            
            // Update Canvas
            canvasComponents.OnClickChanges();
        }

        // Left_Shift - UP -> Remove Selected Tile
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            if (mouseTileHighlighter.GetComponent<SpriteRenderer>().sprite != defaultTileHighlighter)
                mouseTileHighlighter.GetComponent<SpriteRenderer>().sprite = defaultTileHighlighter;
            if (selectedTileToBuild) selectedTileToBuild = null;
        }


        // Left_Shift - HOLD DOWN -> Keep Building Selected
        if (Input.GetKey(KeyCode.LeftShift))
        {
            // if (selectedUnit)
            // {
            //     if (selectedUnit.transform.GetChild(0).gameObject.activeInHierarchy)
            //         selectedUnit.transform.GetChild(0).gameObject.SetActive(false);
            //     lastSelectedUnit = selectedUnit;
            //     selectedUnit = null;
            //     canvasComponents.OnClickChanges();
            // }

            if (Input.GetMouseButtonDown(0))
            {
                // LMB - Down -> Select Tile on ground
                if (selectedTileToBuild == null) // Attempt to Select
                {
                    GameObject unitHit;
                    switch (hit.transform.gameObject.layer)
                    {
                        case 8: // Entity

                            unitHit = hit.transform.gameObject;

                            break;
                        case 9: // Tile
                            
                            unitHit = hit.transform.gameObject;

                            // Is a Building
                            if (unitHit.TryGetComponent<A_Building>(out var aBuilding))
                            {
                                switch (unitHit.transform.GetChild(0).gameObject.activeInHierarchy)
                                {
                                    case true:

                                        unitHit.transform.GetChild(0).gameObject.SetActive(false);
                                        if (selectedUnits.Count > 0)
                                        {
                                            // Remove from list
                                            lastSelectedUnit = selectedUnits[selectedUnits.Count - 1];
                                            selectedUnits.Remove(unitHit);
                                        }
                                        
                                        // Update Canvas
                                        canvasComponents.OnClickChanges();


                                        break;
                                    case false:
                                        
                                        // If list contains at least one item
                                        if (selectedUnits.Count > 0 && selectedUnits[0].layer == 9 /*Tile ... THIS IS ONLY TEMPORAL FIX*/)
                                        {
                                            // Has same name like first tile from Selection list
                                            if (aBuilding.tileName ==
                                                selectedUnits[0].GetComponent<A_Building>().tileName)
                                            {
                                                // Add to list
                                                unitHit.transform.GetChild(0).gameObject.SetActive(true);
                                                unitHit.transform.GetChild(0).GetComponent<SpriteRenderer>().color =
                                                    new Color(251f / 255f, 245f / 255f, 255f / 255f, 255f / 255f);
                                                lastSelectedUnit = selectedUnits[selectedUnits.Count - 1];
                                                selectedUnits.Add(unitHit);
                                            }
                                        }
                                        else if (selectedUnits.Count == 0)
                                        {
                                            unitHit.transform.GetChild(0).gameObject.SetActive(true);
                                            unitHit.transform.GetChild(0).GetComponent<SpriteRenderer>().color =
                                                new Color(251f / 255f, 245f / 255f, 255f / 255f, 255f / 255f);
                                            selectedUnits.Add(unitHit);
                                            lastSelectedUnit = null;
                                        }
                                        
                                        // Update Canvas
                                        canvasComponents.OnClickChanges();

                                        break;
                                }
                            }


                            // if (hit.transform.parent == buildingsTm.transform)
                            // {
                            //     //var interactable = hits[i].transform.GetComponent<IInteractable>(); /*buildingsTileMap.GetInstantiatedObject(cellPosition).GetComponent<IInteractable>();*/
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
                            Instantiate(selectedTileToBuild, grid.GetCellCenterLocal(cellPosition), Quaternion.identity)
                                .transform.SetParent(buildingsTm.transform);
                            resourceBarManager.SubtractAll(selectedTileToBuild.GetComponent<A_Building>().mushLogCosts,
                                selectedTileToBuild.GetComponent<A_Building>().soulCosts,
                                selectedTileToBuild.GetComponent<A_Building>().foodCosts);
                            break;
                    }
                }
            }

            return;
        }


        // LMB - Down (No Shift)
        if (!Input.GetKey(KeyCode.LeftShift) && Input.GetMouseButtonDown(0))
        {
            // Remove all possibly selected items expect the first one 
            if (selectedUnits.Count > 1)
            {
                for (var i = selectedUnits.Count - 1; i >= 1; i--)
                {
                    selectedUnits[i].transform.GetChild(0).gameObject.SetActive(false);
                    selectedUnits.Remove(selectedUnits[i]);
                }
            }

            // LMB - Down -> Select Tile on ground
            if (selectedTileToBuild == null) // Attempt to Select
            {
                GameObject unitHit;
                switch (hit.transform.gameObject.layer)
                {
                    case 8: // Entity

                        unitHit = hit.transform.gameObject;
                        // lastSelectedUnit = selectedUnits[0];

                        // Is tileHit selected
                        switch (unitHit.transform.GetChild(0).gameObject.activeInHierarchy)
                        {
                            // Yes
                            case true:

                                unitHit.transform.GetChild(0).gameObject.SetActive(false);
                                if (selectedUnits.Count > 0)
                                {
                                    // Remove from list
                                    lastSelectedUnit = selectedUnits[selectedUnits.Count - 1];
                                    selectedUnits.Remove(unitHit);
                                }
                                else if (selectedUnits.Count == 0)
                                {
                                    unitHit.transform.GetChild(0).gameObject.SetActive(false);
                                }

                                // Update Canvas
                                canvasComponents.OnClickChanges();

                                break;
                            // No
                            case false:

                                unitHit.transform.GetChild(0).gameObject.SetActive(true);
                                unitHit.transform.GetChild(0).GetComponent<SpriteRenderer>().color =
                                    new Color(242f / 255f, 211f / 255f, 171f / 255f, 255f / 255f);
                                if (selectedUnits.Count > 0)
                                {
                                    lastSelectedUnit = selectedUnits[0];
                                    selectedUnits[0].transform.GetChild(0).gameObject.SetActive(false);
                                    selectedUnits.Remove(selectedUnits[0]);
                                    selectedUnits.Add(unitHit);
                                }
                                else if (selectedUnits.Count == 0)
                                {
                                    selectedUnits.Add(unitHit);
                                    lastSelectedUnit = null;
                                }

                                // Update Canvas
                                canvasComponents.OnClickChanges();

                                break;
                        }

                        break;
                    case 9: // Tile

                        unitHit = hit.transform.gameObject;
                        // lastSelectedUnit = selectedUnits[0];

                        // Is child of buildings TileMap
                        if (unitHit.transform.parent == buildingsTm.transform)
                        {
                            // Contains Class A_Building
                            if (unitHit.TryGetComponent<A_Building>(out var aBuilding))
                            {
                                
                                // Is tileHit selected
                                switch (unitHit.transform.GetChild(0).gameObject.activeInHierarchy)
                                {
                                    // Yes
                                    case true:
                                        
                                        unitHit.transform.GetChild(0).gameObject.SetActive(false);
                                        if (selectedUnits.Count > 0)
                                        {
                                            // Remove from list
                                            lastSelectedUnit = selectedUnits[selectedUnits.Count - 1];
                                            selectedUnits.Remove(unitHit);
                                        }
                                        else if (selectedUnits.Count == 0)
                                        {
                                            unitHit.transform.GetChild(0).gameObject.SetActive(false);
                                        }
                                        
                                        // Update Canvas
                                        canvasComponents.OnClickChanges();
                                        
                                        break;
                                    // No
                                    case false:

                                        unitHit.transform.GetChild(0).gameObject.SetActive(true);
                                        unitHit.transform.GetChild(0).GetComponent<SpriteRenderer>().color =
                                            new Color(251f / 255f, 245f / 255f, 255f / 255f, 255f / 255f);
                                        if (selectedUnits.Count > 0)
                                        {
                                            lastSelectedUnit = selectedUnits[0];
                                            selectedUnits[0].transform.GetChild(0).gameObject.SetActive(false);
                                            selectedUnits.Remove(selectedUnits[0]);
                                            selectedUnits.Add(unitHit);
                                        }
                                        else if (selectedUnits.Count == 0)
                                        {
                                            selectedUnits.Add(unitHit);
                                            lastSelectedUnit = null;
                                        }
                                        
                                        // Set Up Tile Controller Buttons
                                        canvasComponents.tileActionControlGuiScript.CreateButtonList();
                                        aBuilding.SetBasicButtonsAttributes();

                                        // Update Canvas
                                        canvasComponents.OnClickChanges();
                                        
                                        break;
                                }

                                //var interactable = hits[i].transform.GetComponent<IInteractable>(); /*buildingsTileMap.GetInstantiatedObject(cellPosition).GetComponent<IInteractable>();*/
                                //if (interactable == null) return;
                                //interactable.Interact();
                            }
                        }

                        break;
                    case 10: // Ground

                        unitHit = hit.transform.gameObject;
                        
                        // Unselect all
                        if (selectedUnits.Count > 0)
                        {
                            lastSelectedUnit = selectedUnits[0];
                            ClearSelectedUnitList();
                            
                            // Update Canvas
                            canvasComponents.OnClickChanges();
                            Debug.Log("Ground has been hit");
                        }

                        break;
                }
            }

            // LMB - Down -> Build One Tile
            if (selectedTileToBuild) // Attempt to Build
            {
                switch (hit.transform.gameObject.layer)
                {
                    case 8: // Entity

                        break;
                    case 9: // Tile

                        Debug.Log("Can't Build there,\n\r hit " + hit.transform.gameObject.name);

                        break;
                    case 10: // Ground

                        // Create Tile on Mouse Tile Position and parent it to Buildings TileMap
                        Instantiate(selectedTileToBuild, grid.GetCellCenterLocal(cellPosition), Quaternion.identity)
                            .transform.SetParent(buildingsTm.transform);
                        mouseTileHighlighter.GetComponent<SpriteRenderer>().sprite = defaultTileHighlighter;

                        // Subtract Tile building Costs from Resource Bar
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
            GameObject unitHit;
            switch (hit.transform.gameObject.layer)
            {
                case 8: // Entity

                    unitHit = hit.transform.gameObject;

                    break;
                case 9: // Tile

                    unitHit = hit.transform.gameObject;

                    if (selectedUnits.Count > 1)
                    {
                        Vector3Int resources = Vector3Int.zero;
                        for (var i = selectedUnits.Count - 1; i >= 0; i--)
                        {
                            // Hit is our selected tile
                            if (unitHit == selectedUnits[i])
                            {
                                // Its a building
                                if (unitHit.TryGetComponent<A_Building>(out var aBuilding))
                                {
                                    // Get back the resources it cost
                                    resources = aBuilding.allCosts;

                                    lastSelectedUnit = selectedUnits[i];
                                    selectedUnits.Remove(unitHit);

                                    // Update Canvas
                                    canvasComponents.OnClickChanges();

                                    Destroy(unitHit);
                                }
                            }
                            // Hit isn't our selected tile
                            else if (unitHit != selectedUnits[i])
                            {
                                // Its a building
                                if (unitHit.TryGetComponent<A_Building>(out var aBuilding))
                                {
                                    // Get back the resources it cost
                                    resources = aBuilding.allCosts;

                                    Destroy(unitHit);
                                }
                            }
                        }
                        
                        resourceBarManager.AddAll(resources.x, resources.y, resources.z);

                    }
                    else if (selectedUnits.Count == 1)
                    {
                        // Hit is our selected tile
                        if (unitHit == selectedUnits[0])
                        {
                            // Its a building
                            if (unitHit.TryGetComponent<A_Building>(out var aBuilding))
                            {
                                // Get back the resources it cost
                                var resources = aBuilding.allCosts;
                                resourceBarManager.AddAll(resources.x, resources.y, resources.z);

                                lastSelectedUnit = selectedUnits[0];
                                selectedUnits.Remove(unitHit);

                                // Update Canvas
                                canvasComponents.OnClickChanges();

                                Destroy(unitHit);
                            }
                        }
                        // Hit isn't our selected tile
                        else if (unitHit != selectedUnits[0])
                        {
                            // Its a building
                            if (unitHit.TryGetComponent<A_Building>(out var aBuilding))
                            {
                                // Get back the resources it cost
                                var resources = aBuilding.allCosts;
                                resourceBarManager.AddAll(resources.x, resources.y, resources.z);

                                // Update Canvas
                                canvasComponents.OnClickChanges();
                                
                                Destroy(unitHit);
                            }
                        }
                    }
                    else
                    {
                        if (unitHit.TryGetComponent<A_Building>(out var aBuilding))
                        {
                            // Get back the resources it cost
                            var resources = aBuilding.allCosts;
                            resourceBarManager.AddAll(resources.x, resources.y, resources.z);

                            // Update Canvas
                            canvasComponents.OnClickChanges();
                            
                            Destroy(unitHit);
                        }
                    }


                    // var resources = selectedUnit.GetComponent<A_Building>().allCosts;
                    // resourceBarManager.AddAll(resources.x, resources.y, resources.z);
                    Destroy(unitHit);

                    break;
                case 10: // Ground

                    break;
            }

            return;
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
}
*/