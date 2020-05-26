using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpaceBody
{
    public GameObject spaceObject;
    
    public Vector3 position;
    public Vector3 scale;
    public Quaternion quaternion;


    public SpaceBody()
    {
        quaternion = Quaternion.identity;
        position = Vector3.zero;
        scale = Vector3.one;
    }

}
