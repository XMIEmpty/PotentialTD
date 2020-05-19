using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private float cameraMovementSpeed = 3;
    [SerializeField]
    private Rigidbody2D characterRB;

    // Start is called before the first frame update
    void Start()
    {
        characterRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        characterRB.velocity = new Vector2(moveHorizontal, moveVertical) * cameraMovementSpeed * 1000 * Time.fixedDeltaTime;

        SpeedHandler();
    }

    private void SpeedHandler()
    {
        if (Input.GetKeyDown(KeyCode.Plus) || Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            cameraMovementSpeed++;
        }
        if (Input.GetKeyDown(KeyCode.Minus) || Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            cameraMovementSpeed--;
        }
    }
}
