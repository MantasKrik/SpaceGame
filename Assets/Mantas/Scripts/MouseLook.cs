using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{

    public Camera camera;
    public Transform player;
    public float rotationSpeed;

    private float mouseX;
    private float mouseY;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {

        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        player.Rotate(new Vector3(0, player.rotation.x + mouseX * rotationSpeed * Time.deltaTime, 0));

        camera.transform.eulerAngles -= new Vector3(mouseY, 0, 0);

    }
}
