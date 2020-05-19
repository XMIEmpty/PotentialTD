using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileHandling : MonoBehaviour
{
    [SerializeField]
    Grid grid;
    Tilemap[] tilemaps;
    Tile tile;
    public float buttonHoldDownTimer;

    [SerializeField]
    GameObject SelectedObjToInstantiate, OtherSelectedObjToInstantiate, tileSelectioning;

    void Start()
    {
        grid = FindObjectOfType<Grid>();
    }


    void Update()
    {
        Vector3 clickPosition = -Vector3.one;

        //Processing
        clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 10));
        Vector3 lastClickPos = Input.mousePosition;

        Vector3Int cellPosition = grid.LocalToCell(clickPosition);
        clickPosition = grid.GetCellCenterLocal(cellPosition);
        tileSelectioning.transform.position = clickPosition;

        //Debug.Log("Click" + clickPosition);

        ////tilemaps[0].GetTile(new Vector2Int(hit.point.x, hit.point.y));
        //if (hit.point != null)
        //{

        //}

        //Vector3Int cellPosition = grid.LocalToCell(hitPos);
        //hitPos = grid.GetCellCenterLocal(cellPosition);
        //tileSelectioning.transform.position = hitPos;
        //Vector3 mousePosOnClickDown = Input.mousePosition;
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(SelectedObjToInstantiate, clickPosition, Quaternion.identity);
        }
        if (Input.GetMouseButtonDown(1))
        {
            Instantiate(OtherSelectedObjToInstantiate, clickPosition, Quaternion.identity);
        }

    }


    public void DragSelection()
    {

    }
}
