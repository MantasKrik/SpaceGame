using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pickables : MonoBehaviour
{
    public enum Effect { Destroy, PlaySound, MoveAway, Teleports };
    public Effect effect;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("enter");
        if (other.gameObject.tag == "Player")
        {
            
            switch (effect)
            {
                case Effect.Destroy:
                    EffectAsDestroy(other);
                    break;

                case Effect.PlaySound:
                    EffectAsPlaySound(other);
                    break;

                case Effect.MoveAway:
                    EffectAsMoveAway(other);
                    break;

                case Effect.Teleports:
                    EffectAsTeleports(other);
                    break;

                default:
                    {
                        Debug.Log("Error on OnTriggerEnter, Effect"); 
                        break;
                    }
            }
        }
    }

    private void EffectAsDestroy(Collider other)
    {


        var o = other.GetComponent<ScreenText>();
        o.AddCount();

        Destroy(gameObject);
    }
    private void EffectAsPlaySound(Collider other)
    {

    }
    private void EffectAsMoveAway(Collider other)
    {

    }
    private void EffectAsTeleports(Collider other)
    {

    }
}
