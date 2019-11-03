using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{

    public GameObject[] tilePrefabs;    // Array of tiles

    private Transform playerTransform;  // Player position
    private float zSpawn = 0.0f;        // Z position to spawn tile
    //private float xSpawn = 2.5f;               // X position to spawn tile *to be added*
    private float tileLength = 19.0f;   // Length of tiles
    private int tileAmount = 10;         // Number of tiles on screen
    private int tileIndex = 0;
    private float safeZone = 15.0f;
    private int lastPrefabIndex = 0;


    private List<GameObject> activeTiles;

    // Use this for initialization
    void Start()
    {

        activeTiles = new List<GameObject>();

        // Find players position
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        // Create initial tiles
        for (int i = 0; i < tileAmount; i++)
        {
            if (i < 2)
                createTile(0);
            else
                createTile();
            createTile();
        }

    }

    // Update is called once per frame
    void Update()
    {

        // Creates new tiles as the player moves
        if (playerTransform.position.z - safeZone > (zSpawn - tileAmount * tileLength))
        {
            createTile();
            DeleteTile();
        }

    }

    // Used to create tiles
    private void createTile(int tileIndex = -1)
    {
        GameObject tile;
        if (tileIndex == -1)
            tile = Instantiate(tilePrefabs[RandomPrefabIndex()]) as GameObject;
        else
            tile = Instantiate(tilePrefabs[tileIndex]) as GameObject;


        tile.transform.SetParent(transform);
        // Sets Z position of tile infront of previous
        tile.transform.position = Vector3.forward * zSpawn;
        zSpawn += tileLength;
        activeTiles.Add(tile);
    }

    private void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }

    private int RandomPrefabIndex()
    {
        if (tilePrefabs.Length <= 1)
            return 0;

        int randomIndex = lastPrefabIndex;
        while (randomIndex == lastPrefabIndex)
        {
            randomIndex = Random.Range(0, tilePrefabs.Length);
        }

        lastPrefabIndex = randomIndex;
        return randomIndex;
    }


}
