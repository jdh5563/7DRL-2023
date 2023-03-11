using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockRespawner : MonoBehaviour
{

    public Vector2Int tileCoords;

    private bool active = false;

    public GameObject block;
    private GameObject blockTile;
    private GameObject blockSpawnPoint;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //respawnTile = CreateGrid.grid[tileCoords.x, tileCoords.y];
        //blockTile = CreateGrid.grid[block.GetComponent<Block>().currentTileCoords.x, block.GetComponent<Block>().currentTileCoords.y];
        //blockStartTile = CreateGrid.grid[block.GetComponent<Block>().startCoords.x, block.GetComponent<Block>().startCoords.y];

        //CheckIfActive();
        //RespawnBlock();
    }

    private void CheckIfActive()
    {
        //if (!respawnerTile.GetComponent<Tile>().IsUnoccupied() && respawnerTile.GetComponent<Tile>().occupant.tag == "Player") active = true;
    }

    private void RespawnBlock()
    {
        //if (blockStartTile.GetComponent<Tile>().IsUnoccupied() && active)
        //{
        //    blockStartTile.GetComponent<Tile>().occupant = blockTile.GetComponent<Tile>().occupant;
        //    blockTile.GetComponent<Tile>().occupant = null;
        //    block.transform.position = blockStartTile.transform.position;
        //    block.GetComponent<Block>().currentTileCoords = block.GetComponent<Block>().startCoords;
        //    active = false;
        //}
    }
}
