using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpaceTile
{
    public int ID; // ID of the tile
    public Vector3 position;
    public GameObject spaceTileObject;
    public int tileSize = 150;
    public List<SpaceBody> spaceObjects;
    

    public SpaceTile()
    {
        
        spaceObjects = new List<SpaceBody>();
    }

    public SpaceTile(int _ID, Vector3 _position, GameObject _spaceTileObject, int _tileSize)
    {
        spaceObjects = new List<SpaceBody>();
        ID = _ID;
        position = _position;
        spaceTileObject = _spaceTileObject;
        tileSize = _tileSize;
    }



}
