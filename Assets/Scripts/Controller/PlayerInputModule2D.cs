using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Module for player controller input when using a PlatformController2D. Uses standart Horizontal and Vertical input axis as well as Jump button.
/// Also handles shooting.
/// </summary>
[RequireComponent(typeof(PlatformerController2D))]
public class PlayerInputModule2D : MonoBehaviour
{

    PlatformerController2D controller;

    private bool inputJump = false;

    void Start()
    {
        controller = GetComponent<PlatformerController2D>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
            inputJump = true;
    }

    void FixedUpdate()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Horizontal"));
        controller.input = input;
        controller.inputJump = inputJump;
        controller.inputItem = Input.GetKeyDown(KeyCode.X);
        controller.inputFire = Input.GetKeyDown(KeyCode.C);
        inputJump = false;
    }
}
