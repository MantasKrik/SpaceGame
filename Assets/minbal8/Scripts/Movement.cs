using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 10.0f;
    private CharacterController controller;
    public float gravity = -10f;

    Vector3 velocity;
    public float jumpHeight = 2f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        controller = GetComponent<CharacterController>();

    }

    void Update()
    {
        float forward = Input.GetAxis("Vertical");
        float right = Input.GetAxis("Horizontal");

        Vector3 movementDir = transform.forward * forward + transform.right * right;
        

        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2;
        }

        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
        controller.Move(movementDir * speed * Time.deltaTime);

        if (Input.GetKeyDown("escape"))
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private bool isGrounded()
    {
        if (controller.isGrounded == true) return true;
        return Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

    }

}
