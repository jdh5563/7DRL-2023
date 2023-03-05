using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateGrid : MonoBehaviour
{
    // Prefabs to instantiate
    [SerializeField] private GameObject playerPrefab;
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

		// Spawn the enemy on a random tile at the top of the grid
		randomStartCoords = new Vector2Int(gridHeight - 1, Random.Range(0, gridWidth));
		randomTile = grid[randomStartCoords.x, randomStartCoords.y];
		GameObject enemy = Instantiate(enemyPrefabs[0], randomTile.transform.position, Quaternion.identity);
		enemy.GetComponent<Enemy>().currentTileCoords = randomStartCoords;
		enemy.GetComponent<Enemy>().player = player;
		randomTile.GetComponent<Tile>().occupant = enemy;

		// Add objects to the turn order
		TurnOrder.turnOrder.Add(player);
		TurnOrder.turnOrder.Add(enemy);
	}
}
