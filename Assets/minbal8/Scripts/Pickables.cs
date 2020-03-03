using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pickables : MonoBehaviour
{
    public Text myText;

    int test = 0;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Pickable")
        {
            test++;
            myText.text = "Picked up " + test + " items";
            Debug.Log("PickedUp ");
            Destroy(other.gameObject);
        }
    }
}
