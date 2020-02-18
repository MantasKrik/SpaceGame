using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public PlayerController player;

    void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            player.isGrounded = true;
        }
    }

    void OnTriggerExit(Collider collider)
    {

        player.isGrounded = false;
       
    }
}
