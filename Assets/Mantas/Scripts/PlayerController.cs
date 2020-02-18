using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;

    [Header("Movement")]
    public float movementSpeed = 10f;
    public float maxSpeed = 5f;
    public Transform groundCheck;
    public float jumpForce = 4f;
    public bool isGrounded;
    public bool shouldJump;

    private Vector3 inputMovement;

    [Header("Flashlight")]
    public Light flashlight;
    public bool flashlightIsOn;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        GetMovementInput();
        GetItemInput();
        
    }

    private void FixedUpdate()
    {
        Move();
        Jump();

        Flash();

    }

    private void Flash()
    {
        if (flashlightIsOn)
            flashlight.enabled = true;
        else flashlight.enabled = false;
    }

    private void Jump()
    {
        if (isGrounded && shouldJump)
        {
            Debug.Log("Jump");
            rb.AddRelativeForce(Vector3.up * jumpForce * rb.mass, ForceMode.Impulse);
            shouldJump = false;
        }
    }

    private void Move()
    {
        if (!(rb.velocity.sqrMagnitude > maxSpeed) && isGrounded)
        {
            rb.AddRelativeForce(inputMovement.normalized * movementSpeed * rb.mass * Time.fixedDeltaTime, ForceMode.Force);
        }
        //rb.MovePosition(transform.position + (inputMovement.normalized * movementSpeed * rb.mass * Time.fixedDeltaTime));
    }

    private void GetMovementInput()
    {
        inputMovement.x = Input.GetAxisRaw("Horizontal");
        inputMovement.z = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            shouldJump = true;
        }
    }
    private void GetItemInput()
    {
        // Flashlight toggle
        if (Input.GetKeyDown(KeyCode.F))
        {
            flashlightIsOn = !flashlightIsOn;
        }
    }

}
