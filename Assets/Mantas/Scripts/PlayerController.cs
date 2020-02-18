using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
   

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        GetMovementInput();
        CheckGround();
        
    }

    private void FixedUpdate()
    {
        Move();

        if (isGrounded && shouldJump)
        {
            Debug.Log("Jump");
            rb.AddRelativeForce(Vector3.up * jumpForce * rb.mass, ForceMode.Impulse);
            shouldJump = false;
        }
            
    }


    private void CheckGround()
    {
        
    }

    private void Move()
    {
        if (!(rb.velocity.sqrMagnitude > maxSpeed))
        {
            rb.AddRelativeForce(inputMovement * movementSpeed * rb.mass * Time.fixedTime, ForceMode.Acceleration);
        }
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

}
