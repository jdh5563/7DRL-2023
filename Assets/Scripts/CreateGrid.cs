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
    [SerializeField] private GameObject exitPrefab;

    // Fields to modify grid size
    private static int gridWidth = 0;
    private static int gridHeight = 0;

    // Fields related to difficulty
    private static int maxDifficulty = 0;
    private static int level = 0;
    [SerializeField] private int[] enemyDifficultyLevels;

    // The grid itself. This will be referenced anytime an object needs the grid
    public static GameObject[,] grid;

    // Start is called before the first frame update
    void Start()
    {;
        ResetGrid();
	}

    // Update is called once per frame
    void Update()
    {
        
    }

    public static bool IsValidTile(int x, int y)
    {
        return x >= 0 && x < gridHeight && y >= 0 && y < gridWidth;
    }

    public void ResetGrid()
    {
		// Destroy the old grid
		for (int i = 0; i < gridHeight; i++)
		{
			for (int j = 0; j < gridWidth; j++)
			{
				if (grid[i, j] != null)
				{
					if (grid[i, j].GetComponent<Tile>().occupant != null) Destroy(grid[i, j].GetComponent<Tile>().occupant);
					if (grid[i, j].GetComponent<Tile>().type != null) Destroy(grid[i, j].GetComponent<Tile>().type);
					Destroy(grid[i, j]);
				}
			}
		}

		level++;
        maxDifficulty = level * 3 + 20;

        Debug.Log("Max Difficulty: " + maxDifficulty);

		// Choose a number of each type of entity allowed in the level so their total adds up to maxDifficulty
		// 1. Enemy 2. Puzzle 3. Grid Dimensions
		int currentDifficulty = 0;
		int numEnemies = Random.Range(level + 3, level + 6);
        int[] numEnemiesOfType = new int[enemyPrefabs.Length];

        for(int i = 0; i < numEnemiesOfType.Length; i++)
        {
            numEnemiesOfType[i] = i == numEnemiesOfType.Length - 1 ? numEnemies : Random.Range(0, numEnemies);
            numEnemies -= numEnemiesOfType[i];
            currentDifficulty += numEnemiesOfType[i] * enemyDifficultyLevels[i];

            if (numEnemies == 0) break;
        }

        if (numEnemies != 0)
        {
            numEnemiesOfType[0] += numEnemies;
            currentDifficulty += numEnemies * enemyDifficultyLevels[0];
        }

        Debug.Log("Enemy Difficulty: " + currentDifficulty);

		//bool hasPuzzle = Random.Range(0f, 1f) < 0.75f;
		//bool blockPuzzle = Random.Range(0f, 1f) < 0.5f;

		//if (hasPuzzle)
  //      {
  //          if (blockPuzzle)
  //          {
  //              currentDifficulty += 6;
		//		Debug.Log("Puzzle Difficulty: " + 6);
		//	}
  //          else
  //          {
  //              currentDifficulty += 3;
		//		Debug.Log("Puzzle Difficulty: " + 3);
		//	}
		//}

        int x = maxDifficulty - currentDifficulty;
		gridWidth = 5;
		gridHeight = 5;

		if (x < 0)
        {
            for(int i = 0; i < -x; i++)
            {
                if(Random.Range(0f, 1f) < 0.5f)
                {
                    gridWidth += 1;
                }
                else
                {
                    gridHeight += 1;
                }
            }
        }

		//gridWidth = Random.Range((maxDifficulty - currentDifficulty) / 2, maxDifficulty - currentDifficulty - 5);
		//      currentDifficulty += gridWidth;
		//gridHeight = maxDifficulty - currentDifficulty;

		Debug.Log("Grid Difficulty: (X: " + gridWidth + ", Y: " + gridHeight + ")");

		grid = new GameObject[gridHeight, gridWidth];

		// Create the new grid
		for (int i = 0; i < gridHeight; i++)
        {
            for (int j = 0; j < gridWidth; j++)
            {
                grid[i, j] = Instantiate(tilePrefab, new Vector2(j - (gridWidth / 2f), i - (gridHeight / 2f)), Quaternion.identity);
            }
        }

        // Spawn the player on a random tile at the bottom of the grid
        bool northSouth = Random.Range(0f, 1f) < 0.5f;
        Vector2Int randomStartCoords = northSouth ? new Vector2Int(0, Random.Range(0, gridWidth)) : new Vector2Int(Random.Range(0, gridHeight), 0);
        GameObject randomTile = grid[randomStartCoords.x, randomStartCoords.y];
        GameObject player = Instantiate(playerPrefab, randomTile.transform.position, Quaternion.identity);
        player.GetComponent<MoveOnGrid>().currentTileCoords = randomStartCoords;
        randomTile.GetComponent<Tile>().occupant = player;

        TurnOrder.turnOrder.Add(player);

		// Spawn the exit in a random location
		randomStartCoords = northSouth ? new Vector2Int(gridHeight - 1, Random.Range(0, gridWidth)) : new Vector2Int(Random.Range(0, gridHeight), gridWidth - 1);
		randomTile = grid[randomStartCoords.x, randomStartCoords.y];
		GameObject exit = Instantiate(exitPrefab, randomTile.transform.position, Quaternion.identity);
		exit.GetComponent<Exit>().tileCoords = randomStartCoords;
		randomTile.GetComponent<Tile>().type = exit;
		TurnOrder.exit = exit.GetComponent<Exit>();

        //if (hasPuzzle)
        //{
        //    if (blockPuzzle)
        //    {
        //        // Spawn a block on the second row not on the rim
        //        randomStartCoords = new Vector2Int(1, Random.Range(1, gridWidth - 2));
        //        randomTile = grid[randomStartCoords.x, randomStartCoords.y];
        //        GameObject block = Instantiate(blockPrefab, randomTile.transform.position, Quaternion.identity);
        //        block.GetComponent<Block>().currentTileCoords = randomStartCoords;
        //        player.GetComponent<MoveOnGrid>().block = block;
        //        randomTile.GetComponent<Tile>().occupant = block;

        //        // Spawn a button in a random location not on the rim or on the exit
        //        do
        //        {
        //            randomStartCoords = new Vector2Int(Random.Range(1, gridHeight - 2), Random.Range(1, gridWidth - 2));
        //        } while (grid[randomStartCoords.x, randomStartCoords.y].GetComponent<Tile>().type == exit);
        //        randomTile = grid[randomStartCoords.x, randomStartCoords.y];
        //        GameObject button = Instantiate(buttonPrefab, randomTile.transform.position, Quaternion.identity);
        //        button.GetComponent<Button>().tileCoords = randomStartCoords;
        //        randomTile.GetComponent<Tile>().type = button;
        //        exit.GetComponent<Exit>().button = button;
        //    }
        //    else
        //    {
        //        // Spawn a lever in a random location not on the exit
        //        do
        //        {
        //            randomStartCoords = new Vector2Int(Random.Range(0, gridHeight - 1), Random.Range(0, gridWidth - 1));
        //        } while (grid[randomStartCoords.x, randomStartCoords.y].GetComponent<Tile>().type == exit);
        //        randomTile = grid[randomStartCoords.x, randomStartCoords.y];
        //        GameObject lever = Instantiate(leverPrefab, randomTile.transform.position, Quaternion.identity);
        //        lever.GetComponent<Lever>().tileCoords = randomStartCoords;
        //        randomTile.GetComponent<Tile>().type = lever;
        //        exit.GetComponent<Exit>().lever = lever;
        //    }
        //}

		for (int i = 0; i < numEnemiesOfType.Length; i++)
        {
            for (int j = 0; j < numEnemiesOfType[i]; j++)
            {
                // Spawn the enemy on a random tile
                do
                {
                    randomStartCoords = new Vector2Int(Random.Range(0, gridHeight), Random.Range(0, gridWidth));
                } while (grid[randomStartCoords.x, randomStartCoords.y].GetComponent<Tile>().occupant != null);

                randomTile = grid[randomStartCoords.x, randomStartCoords.y];
                GameObject enemy = Instantiate(enemyPrefabs[i], randomTile.transform.position, Quaternion.identity);
                enemy.GetComponent<Enemy>().currentTileCoords = randomStartCoords;
                enemy.GetComponent<Enemy>().player = player;
                randomTile.GetComponent<Tile>().occupant = enemy;
                TurnOrder.turnOrder.Add(enemy);
            }
        }
	}
}
