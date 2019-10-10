using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour {

    public GameObject[] tilePrefabs;    // Array of tiles

    private Transform playerTransform;  // Player position
    private float zSpawn = 0.0f;        // Z position to spawn tile
    //private float xSpawn = 2.5f;               // X position to spawn tile *to be added*
    private float tileLength = 10.0f;   // Length of tiles
    private int tileAmount = 7;         // Number of tiles on screen
    private int tileIndex = 0; 

	// Use this for initialization
	void Start () {

        // Find players position
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        
        // Create initial tiles
        for (int i = 0; i < tileAmount; i++) {
            createTile();
        }

	}
	
	// Update is called once per frame
	void Update () {
		
        // Creates new tiles as the player moves
        if(playerTransform.position.z > (zSpawn - tileAmount * tileLength)) {
            createTile();
        }

	}

    // Used to create tiles
    private void createTile(int tileIndex = -1) {
        GameObject tile;
        tileIndex = Random.Range(0, tilePrefabs.Length);
        tile = Instantiate(tilePrefabs[0]) as GameObject;
        tile.transform.SetParent(transform);
        // Sets Z position of tile infront of previous
        tile.transform.position = Vector3.forward * zSpawn;
        zSpawn += tileLength;
    }
}
