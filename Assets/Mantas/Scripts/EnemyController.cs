using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform target;
    public float movementSpeed = 5f;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target);
        rb.AddRelativeForce(Vector3.forward * movementSpeed * Time.deltaTime, ForceMode.Force);
    }
}
