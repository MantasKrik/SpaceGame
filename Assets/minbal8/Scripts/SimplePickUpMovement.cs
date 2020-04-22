using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePickUpMovement : MonoBehaviour
{
    
    //adjust this to change speed
    float speed = 1f;
    //adjust this to change how high it goes
    float height = 0.2f;
    float offset;
    private void Start()
    {
        offset = transform.position.y;
    }
    void Update()
    {
        Vector3 pos = transform.position;
        pos.y = offset + Mathf.Sin(Time.time * speed) * height;
        transform.position = pos;

        transform.Rotate(Vector3.up);

    }
}
