using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchPad : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            var player = collision.gameObject.GetComponent<PlayerMovement>();
            player.LaunchIntoAir(30);
        }
    }
}
