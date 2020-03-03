using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public bool GravityEnabled = false;
    public float SpeedOutsideOfGravity = 5.0f;
    public float speed = 10.0f;
    private CharacterController controller;
    public float gravity = -10f;

    Vector3 velocity;
    public float jumpHeight = 2f;
    public Transform groundCheck;
    public float groundDistance = 0.1f;
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
        if (GravityEnabled)
        {
            if (Input.GetButtonDown("Jump") && isGrounded())
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
            velocity.y += gravity * Time.deltaTime;
        }
        else
        {
            velocity.y = 0;
            if (Input.GetButton("Jump"))
            {
                velocity.y = SpeedOutsideOfGravity;
            }
            if (Input.GetButton("Fire1"))
            {
                velocity.y = -SpeedOutsideOfGravity;
            }
        }

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

    void OnTriggerEnter(Collider theCollision)
    {
        if (theCollision.gameObject.tag == "GravitySphere")
        {
            print("Player entered gravity field");
            GravityEnabled = true;
        }
    }

    void OnTriggerExit(Collider theCollision)
    {
        if (theCollision.gameObject.tag == "GravitySphere")
        {
            print("Player exited gravity field");
            GravityEnabled = false;
        }
    }

}
