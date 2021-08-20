using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;
    private Camera playerCam;
    public float jumpHeight = 30;
    public float speed = 100;
    public float GRAVITY = -9.8f;

    private Vector3 playerVelocity = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        characterController = GameObject.FindObjectOfType<CharacterController>();
        playerCam = GameObject.FindObjectOfType<Camera>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        move();
        jump();
    }

    private void move()
    {
        float horizontal = InputListener.getHorizontalAxis();
        float vertical = InputListener.getVerticalAxis();
        
        
        Vector3 forward = playerCam.transform.forward;
        Vector3 right = playerCam.transform.right;
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        Vector3 movementVector = (forward * vertical) + (right * horizontal);
        characterController.SimpleMove(movementVector * (Time.deltaTime * speed));
    }

    private void jump()
    {
        if (characterController.isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
        
        if (InputListener.isJump() && characterController.isGrounded)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * GRAVITY);
        }
        
        playerVelocity.y += GRAVITY * Time.deltaTime;
        characterController.Move(playerVelocity);
    }
}
