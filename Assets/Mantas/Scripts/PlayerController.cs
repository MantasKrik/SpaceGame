using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;

    [Header("Vitals")]
    public float health = 100f;
    public float healthMax = 100f;
    public float oxygen = 75f;
    public float oxygenMax = 100f;
    public float food = 50f;
    public float foodMax = 100f;

    [Header("Movement")]
    public float movementSpeed = 10f;
    public float maxSpeed = 5f;
    public Transform groundCheck;
    public float jumpForce = 4f;
    public bool isGrounded;
    public bool shouldJump;

    private Vector3 inputMovement;

    [Header("Inventory")]
    //private Inventory inventory;
    //[SerializeField] private UI_Inventory uiInventory;
    //private Item activeItem;
    public InventoryObject inventory;
    public MouseItem mouseItem = new MouseItem();

    private bool isLookingAt = false;
    private RaycastHit lookingAt;
    public Animator crosshairAnimator;


    [Header("Weapon")]
    public Animator weaponAnimator;

    [Header("Flashlight")]
    public Light flashlight;
    public bool flashlightIsOn;

    public float score = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //inventory = new Inventory();
        //uiInventory.SetInventory(inventory);
        //activeItem = inventory.GetItemList()[0];
    }

    // Update is called once per frame
    void Update()
    {

        GetMovementInput();
        GetRaycastLook();
        //GetItemInput();

        //TEST SAVING AND LOADING INVENTORY
        if (Input.GetKeyDown(KeyCode.Z))
        {
            inventory.Save();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            inventory.Load();
        }

    }

    private void FixedUpdate()
    {
        Move();
        Jump();
        Interact();
        weaponAnimator.SetFloat("yVelocity", rb.velocity.y);

    }

    private void Interact()
    {
        if (isLookingAt)
            switch (lookingAt.transform.gameObject.tag)
            {
                case "Item":
                    crosshairAnimator.SetBool("onInteractable", true);

                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        GroundItem item = lookingAt.transform.gameObject.GetComponent<GroundItem>();
                        inventory.AddItem(new Item(item.item), item.amount);
                        Destroy(item.gameObject);
                    }

                    break;

                case "Machine":
                    break;

                default:
                    crosshairAnimator.SetBool("onInteractable", false);
                    break;
            }
        else crosshairAnimator.SetBool("onInteractable", false);
    }

    private void Use()
    {
        if (Input.GetMouseButton(0))
        {
            //activeItem.Use();
            weaponAnimator.SetBool("isShooting", true);
        }
        else
        {
            //activeItem.Cancel();
            weaponAnimator.SetBool("isShooting", false);
        }
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

        if (inputMovement != Vector3.zero)
            weaponAnimator.SetBool("isWalking", true);
        else weaponAnimator.SetBool("isWalking", false);


        if (Input.GetKeyDown(KeyCode.Space))
            shouldJump = true;
            
        weaponAnimator.SetBool("isGrounded", isGrounded);
    }
    //private void GetItemInput()
    //{
    //    // Flashlight toggle
    //    if (Input.GetKeyDown(KeyCode.F))
    //    {
    //        flashlightIsOn = !flashlightIsOn;
    //    }
    //}

    public void Eat(float restoreHealth, float restoreFood)
    {
        health = Mathf.Clamp(health + restoreHealth, 0, healthMax);
        food = Mathf.Clamp(food + restoreFood, 0, foodMax);
    }

    public void GetRaycastLook()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        isLookingAt = Physics.Raycast(ray, out hit, 2f);
        lookingAt = hit;
    }

    private void OnApplicationQuit()
    {
        inventory.Container.Items = new InventorySlot[28]; // Clears all items in the inventory
    }

}
