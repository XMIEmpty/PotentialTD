using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager current;


    private void Start()
    {
        current = this;
    }


    void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }
}
