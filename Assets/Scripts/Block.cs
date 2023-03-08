using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    
    public Vector2Int currentTileCoords;
    private Vector2Int newTileCoords;
    public bool isMoving = false;
    private float percentDone = 0.05f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        // Move to the designated point
        if (isMoving)
        {
            transform.position = Vector2.Lerp(CreateGrid.grid[currentTileCoords.x, currentTileCoords.y].transform.position, CreateGrid.grid[newTileCoords.x, newTileCoords.y].transform.position, percentDone);
            percentDone += 0.05f;

            if (percentDone >= 1f)
            {
                CreateGrid.grid[currentTileCoords.x, currentTileCoords.y].GetComponent<Tile>().occupant = null;
                CreateGrid.grid[newTileCoords.x, newTileCoords.y].GetComponent<Tile>().occupant = gameObject;

                transform.position = CreateGrid.grid[newTileCoords.x, newTileCoords.y].transform.position;
                currentTileCoords = newTileCoords;
                percentDone = 0.05f;
                isMoving = false;
                TurnOrder.EndTurn(TurnOrder.turnOrder[0]);
			}
        }
    }

    // Moves the block based on the direction it was pushed
    public void Move(int direction, bool vertical)
    {
        newTileCoords = currentTileCoords;
        if (vertical)
        {
            newTileCoords.x = currentTileCoords.x + direction;
            isMoving = true;
        }
        else
        {
            newTileCoords.y = currentTileCoords.y + direction;
            isMoving = true;
        }
    }
}
