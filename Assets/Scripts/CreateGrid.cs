using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateGrid : MonoBehaviour
{
    // Prefabs to instantiate
    [SerializeField] private GameObject playerPrefab;
	[SerializeField] private GameObject blockPrefab;
	[SerializeField] private GameObject buttonPrefab;
    [SerializeField] private GameObject leverPrefab;
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private GameObject tilePrefab;

    // Fields to modify grid size
    [SerializeField] private static int gridWidth = 5;
    [SerializeField] private static int gridHeight = 5;

    // The grid itself. This will be referenced anytime an object needs the grid
    public static GameObject[,] grid = new GameObject[gridHeight, gridWidth];

    // Start is called before the first frame update
    void Start()
    {
        ResetGrid();
	}

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetGrid()
    {
        // Create the grid
        for (int i = 0; i < gridHeight; i++)
        {
            for (int j = 0; j < gridWidth; j++)
            {
                Destroy(grid[i, j]);
                grid[i, j] = Instantiate(tilePrefab, new Vector2(j - (gridWidth / 2f), i - (gridHeight / 2f)), Quaternion.identity);
            }
        }

        // Spawn the player on a random tile at the bottom of the grid
        Vector2Int randomStartCoords = new Vector2Int(0, Random.Range(0, gridWidth));
        GameObject randomTile = grid[randomStartCoords.x, randomStartCoords.y];
        GameObject player = Instantiate(playerPrefab, randomTile.transform.position, Quaternion.identity);
        player.GetComponent<MoveOnGrid>().currentTileCoords = randomStartCoords;
        randomTile.GetComponent<Tile>().occupant = player;

        TurnOrder.turnOrder.Add(player);

        for (int i = 0; i < gridWidth; i++)
        {
            if (Random.Range(0f, 1f) < 0.5f)
            {
                // Spawn the enemy on a random tile at the top of the grid
                randomStartCoords = new Vector2Int(gridHeight - 1, i);
                randomTile = grid[randomStartCoords.x, randomStartCoords.y];
                GameObject enemy = Instantiate(enemyPrefabs[0], randomTile.transform.position, Quaternion.identity);
                enemy.GetComponent<Enemy>().currentTileCoords = randomStartCoords;
                enemy.GetComponent<Enemy>().player = player;
                randomTile.GetComponent<Tile>().occupant = enemy;
                TurnOrder.turnOrder.Add(enemy);
            }
        }

        // Spawn a block on the second row not on the rim
        randomStartCoords = new Vector2Int(1, Random.Range(1, gridWidth - 2));
        randomTile = grid[randomStartCoords.x, randomStartCoords.y];
        GameObject block = Instantiate(blockPrefab, randomTile.transform.position, Quaternion.identity);
        block.GetComponent<Block>().currentTileCoords = randomStartCoords;
        player.GetComponent<MoveOnGrid>().block = block;
        randomTile.GetComponent<Tile>().occupant = block;

        // Spawn a button in a random location not on the rim
        randomStartCoords = new Vector2Int(Random.Range(1, gridHeight - 2), Random.Range(1, gridWidth - 2));
        randomTile = grid[randomStartCoords.x, randomStartCoords.y];
        GameObject button = Instantiate(buttonPrefab, randomTile.transform.position, Quaternion.identity);
        button.GetComponent<Button>().tileCoords = randomStartCoords;
        randomTile.GetComponent<Tile>().type = button;

        //Spawn a lever in a random location
        randomStartCoords = new Vector2Int(Random.Range(0, gridHeight - 1), Random.Range(0, gridWidth - 1));
        randomTile = grid[randomStartCoords.x, randomStartCoords.y];
        GameObject lever = Instantiate(leverPrefab, randomTile.transform.position, Quaternion.identity);
        lever.GetComponent<Lever>().tileCoords = randomStartCoords;
        randomTile.GetComponent<Tile>().type = lever;
    }
}
