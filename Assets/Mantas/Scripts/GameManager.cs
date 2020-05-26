using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public PlayerController player;
    public Transform playerTransform;
    public GameObject meteorPrefab;
    public Vector3 currentPlayerTilePosition;


    public GameObject spaceTilePrefab;
    public Dictionary<Vector3, SpaceTile> spaceTiles; // Map tiles 
    public List<Vector3> activeSpaceTiles;
    public Dictionary<Vector3, GameObject> spaceTileObjects;


    const int spaceTileSize = 150;

    public int numberOfTiles = 0;


    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject); // Instance exists somewhere else
    }

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();

        spaceTiles = new Dictionary<Vector3, SpaceTile>();
        spaceTileObjects = new Dictionary<Vector3, GameObject>();

        currentPlayerTilePosition = Vector3.zero;
        GenerateTile(Vector3.zero, 0);
        SpawnTile(Vector3.zero);

        activeSpaceTiles.Add(Vector3.zero);

    }

    // Update is called once per frame
    void Update()
    {
        UpdatePlayerTilePosition();
        
        CheckTileSpawning();
        CheckTileDespawning();
    }

    void GenerateSpaceObjects(SpaceTile spaceTile, int spaceObjCount)
    {
        for (int i = 0; i < spaceObjCount; i++)
        {
            SpaceBody spaceBody = new SpaceBody();

            // Position
            float radius = Random.Range(10, spaceTile.tileSize/2 - 10); // distance radius (Min, Max)
            float vertical = Random.Range(-spaceTile.tileSize/2, spaceTile.tileSize/2);
            float angle = Random.Range(-Mathf.PI, Mathf.PI);

            Vector3 spawnPosition = spaceTile.position + new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius;
            spawnPosition.y = spaceTile.position.y + vertical;
            spaceBody.position = spawnPosition;

            // GameObject
            spaceBody.spaceObject = MeteorSettings.instance.PickMeteorByProbability();   // Should be a random GameObject based on probability

            if (spaceBody.spaceObject.tag == "Meteor")
            {
                Vector3 rotation = new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
                spaceBody.quaternion = Quaternion.Euler(rotation);

                float scaleMultiplier = Random.Range(0.3f, 5f);
                Vector3 scale = new Vector3(scaleMultiplier, scaleMultiplier, scaleMultiplier);
                spaceBody.scale = scale;
            }

            spaceTile.spaceBodies.Add(spaceBody);
        }
    }

    void GenerateTile(Vector3 pos, int spaceObjCount)
    {
        SpaceTile tile = new SpaceTile();
        Debug.Log(tile.spaceBodies);

        tile.ID = numberOfTiles++;
        tile.position = pos;
        tile.spaceTileObject = spaceTilePrefab;

        GenerateSpaceObjects(tile, spaceObjCount);
        spaceTiles.Add(tile.position, tile);
    }

    void SpawnTile(Vector3 pos)
    {
        SpaceTile tile = spaceTiles[pos];
        GameObject spaceTileObject = Instantiate(tile.spaceTileObject, tile.position, Quaternion.identity);


        spaceTileObjects.Add(tile.position, spaceTileObject);
        SpawnSpaceObjects(tile, spaceTileObject);
    }

    void SpawnSpaceObjects(SpaceTile tile, GameObject tileObj)
    {

        foreach (var obj in tile.spaceBodies)
        {
            Transform t = Instantiate(obj.spaceObject, obj.position, obj.quaternion, tileObj.transform).GetComponent<Transform>();
            t.localScale = obj.scale;

            if (obj.spaceObject.tag == "Meteor")
            {
                Meteor m = t.GetComponent<Meteor>();
                m.resourceCount = (int)t.localScale.x + 1;
            }
        }
        
    }

    private void CheckTileSpawning()
    {

        for (int x = -spaceTileSize; x <= spaceTileSize; x += spaceTileSize)
        {
            for (int y = -spaceTileSize; y <= spaceTileSize; y += spaceTileSize)
            {
                for (int z = -spaceTileSize; z <= spaceTileSize; z += spaceTileSize)
                {
                    if (!activeSpaceTiles.Contains(currentPlayerTilePosition + new Vector3(x, y, z)))
                    {
                        if (Vector3.Distance(playerTransform.position, currentPlayerTilePosition + new Vector3(x, y, z)) <= spaceTileSize * Mathf.Sqrt(2) + 50)
                        {
                            Vector3 nextTile = currentPlayerTilePosition + new Vector3(x, y, z);
                            if (spaceTiles.ContainsKey(nextTile))
                            {
                                SpawnTile(nextTile);
                                activeSpaceTiles.Add(nextTile);

                            }
                            else
                            {
                                GenerateTile(nextTile, Random.Range(0, 3));
                                SpawnTile(nextTile);
                                activeSpaceTiles.Add(nextTile);
                            }
                        }
                    }
                }
            }
        }
    }

    private void CheckTileDespawning()
    {
        foreach (var tile in activeSpaceTiles)
        {
            if (Vector3.Distance(playerTransform.position, tile) >= 270)
            {
                DespawnTile(tile);                // Despawn
                activeSpaceTiles.Remove(tile);    // Remove active item
                break;
            }
        }
       

    }

    private void DespawnTile(Vector3 pos)
    {

        UpdateSpaceBodyList(pos); //TRYING TO ACCESS DESTROYED GAMEOBJECT on line 118 (Transform t)
        Destroy(spaceTileObjects[pos], 0.1f);

        spaceTileObjects.Remove(pos);

    }

    private void UpdateSpaceBodyList(Vector3 tilePos)
    {
        spaceTiles[tilePos].spaceBodies.Clear();
        foreach (Transform childT in spaceTileObjects[tilePos].transform)
        {
            SpaceBody newBody = new SpaceBody();

            if (childT.gameObject.tag == "Meteor")
            {
                MeteorSettings.MeteorType meteorType = childT.GetComponent<Meteor>().meteorType;
                newBody.spaceObject = MeteorSettings.instance.meteorPrefabs[(int)(meteorType)];
            }

            newBody.position = childT.position;
            newBody.quaternion = childT.localRotation;
            newBody.scale = childT.localScale;

            spaceTiles[tilePos].spaceBodies.Add(newBody);
        }
    }

    private void UpdatePlayerTilePosition()
    {
        if(Vector3.Distance(playerTransform.position, currentPlayerTilePosition) > 75)
        {
            foreach(var item in activeSpaceTiles)
            {
                if(Vector3.Distance(playerTransform.position, item) < 75)
                {
                    currentPlayerTilePosition = item;
                }
            }
        }
    }
}
