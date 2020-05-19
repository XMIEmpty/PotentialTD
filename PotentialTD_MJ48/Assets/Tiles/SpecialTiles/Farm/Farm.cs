using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farm : MonoBehaviour
{
    public Grid grid;

    [SerializeField]
    private GameObject FertileLand;
    public List<FertileLand> lands;

    void Awake()
    {
        grid = FindObjectOfType<Grid>();

        Vector3Int cellPosition = grid.LocalToCell(transform.localPosition);
        transform.localPosition = grid.GetCellCenterLocal(cellPosition);

        lands.Add(Instantiate(FertileLand, new Vector3(transform.position.x - 1.0f, transform.position.y + 1.0f), Quaternion.identity, transform).GetComponent<FertileLand>());
        lands[0].gameObject.name = FertileLand.name.Insert(0, "UL");
        lands.Add(Instantiate(FertileLand, new Vector3(transform.position.x, transform.position.y + 1.0f), Quaternion.identity, transform).GetComponent<FertileLand>());
        lands[1].gameObject.name = FertileLand.name.Insert(0, "UC");
        lands.Add(Instantiate(FertileLand, new Vector3(transform.position.x + 1.0f, transform.position.y + 1.0f), Quaternion.identity, transform).GetComponent<FertileLand>());
        lands[2].gameObject.name = FertileLand.name.Insert(0, "UR");
        lands.Add(Instantiate(FertileLand, new Vector3(transform.position.x - 1.0f, transform.position.y), Quaternion.identity, transform).GetComponent<FertileLand>());
        lands[3].gameObject.name = FertileLand.name.Insert(0, "LC");
        lands.Add(Instantiate(FertileLand, new Vector3(transform.position.x + 1.0f, transform.position.y), Quaternion.identity, transform).GetComponent<FertileLand>());
        lands[4].gameObject.name = FertileLand.name.Insert(0, "RC");
        lands.Add(Instantiate(FertileLand, new Vector3(transform.position.x - 1.0f, transform.position.y - 1.0f), Quaternion.identity, transform).GetComponent<FertileLand>());
        lands[5].gameObject.name = FertileLand.name.Insert(0, "DL");
        lands.Add(Instantiate(FertileLand, new Vector3(transform.position.x, transform.position.y - 1.0f), Quaternion.identity, transform).GetComponent<FertileLand>());
        lands[6].gameObject.name = FertileLand.name.Insert(0, "DC");
        lands.Add(Instantiate(FertileLand, new Vector3(transform.position.x + 1.0f, transform.position.y - 1.0f), Quaternion.identity, transform).GetComponent<FertileLand>());
        lands[7].gameObject.name = FertileLand.name.Insert(0, "DR");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            //Vector3Int cellPosition = grid.LocalToCell(transform.localPosition);
            //Debug.Log(cellPosition + " ___ " + transform.localPosition);
            //transform.localPosition = grid.GetCellCenterLocal(cellPosition);
            //Debug.Log(transform.localPosition + " ___ " + cellPosition);
        }
    }
}
