using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float health = 100f;

    void OnCollisionEnter(Collision collision)
    {

        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("Player touched me");

            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            player.score += 1f;

            Destroy(gameObject);
        }
    }
}
