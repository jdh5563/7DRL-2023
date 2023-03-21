using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnGrid : MonoBehaviour
{
    private Vector2 basePosition;
    private Vector2 endPosition;
    public bool isMoving = false;
    private float percentDone = 0.05f;

    public GameObject block = null;

    public Vector2Int currentTileCoords;
    public Vector2Int oldTileCoords;

    // Start is called before the first frame update
    void Start()
    {
        basePosition = transform.position;
        endPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

	private void FixedUpdate()
	{
        // Move to the designated tile
		if (isMoving)
        {
            transform.position = Vector2.Lerp(basePosition, endPosition, percentDone);
            percentDone += 0.05f;

            if(percentDone >= 1f)
            {
				//CreateGrid.grid[oldTileCoords.x, oldTileCoords.y].GetComponent<Tile>().occupant = null;
				//CreateGrid.grid[currentTileCoords.x, currentTileCoords.y].GetComponent<Tile>().occupant = gameObject;

				transform.position = endPosition;
                basePosition = endPosition;
                percentDone = 0.05f;
                isMoving = false;
				TurnOrder.EndTurn(gameObject);
			}
        }
	}

    public void TakeAction()
    {
		// If it is the player's turn, move to the chosen tile
		GameObject tile;

		if (!isMoving && (block == null || !block.GetComponent<Block>().isMoving))
		{
			if (Input.GetAxisRaw("Horizontal") != 0 && CreateGrid.IsValidTile(currentTileCoords.x, currentTileCoords.y + (int)Input.GetAxisRaw("Horizontal")))
			{
				tile = CreateGrid.grid[currentTileCoords.x, currentTileCoords.y + (int)Input.GetAxisRaw("Horizontal")];

				if (tile.GetComponent<Tile>().IsUnoccupied())
				{
					isMoving = true;
					endPosition = new Vector2(basePosition.x + Input.GetAxisRaw("Horizontal"), basePosition.y);
					oldTileCoords = currentTileCoords;
					currentTileCoords.y += (int)Input.GetAxisRaw("Horizontal");
					CreateGrid.grid[oldTileCoords.x, oldTileCoords.y].GetComponent<Tile>().occupant = null;
					CreateGrid.grid[currentTileCoords.x, currentTileCoords.y].GetComponent<Tile>().occupant = gameObject;
				}
				else if (tile.GetComponent<Tile>().occupant.tag == "Block" && CreateGrid.IsValidTile(currentTileCoords.x, currentTileCoords.y + (int)Input.GetAxisRaw("Horizontal") * 2) && CreateGrid.grid[currentTileCoords.x, currentTileCoords.y + (int)Input.GetAxisRaw("Horizontal") * 2].GetComponent<Tile>().IsUnoccupied())
				{
					block.GetComponent<Block>().Move(0, 1, gameObject);
                }
			}
			else if (Input.GetAxisRaw("Vertical") != 0 && CreateGrid.IsValidTile(currentTileCoords.x + (int)Input.GetAxisRaw("Vertical"), currentTileCoords.y))
			{
				tile = CreateGrid.grid[currentTileCoords.x + (int)Input.GetAxisRaw("Vertical"), currentTileCoords.y];

				if (tile.GetComponent<Tile>().IsUnoccupied())
				{
					isMoving = true;
					endPosition = new Vector2(basePosition.x, basePosition.y + Input.GetAxisRaw("Vertical"));
					oldTileCoords = currentTileCoords;
					currentTileCoords.x += (int)Input.GetAxisRaw("Vertical");
					CreateGrid.grid[oldTileCoords.x, oldTileCoords.y].GetComponent<Tile>().occupant = null;
					CreateGrid.grid[currentTileCoords.x, currentTileCoords.y].GetComponent<Tile>().occupant = gameObject;
				}
				else if (tile.GetComponent<Tile>().occupant.tag == "Block" && CreateGrid.IsValidTile(currentTileCoords.x + (int)Input.GetAxisRaw("Vertical") * 2, currentTileCoords.y) && CreateGrid.grid[currentTileCoords.x + (int)Input.GetAxisRaw("Vertical") * 2, currentTileCoords.y].GetComponent<Tile>().IsUnoccupied())
				{
					block.GetComponent<Block>().Move(1, 0, gameObject);
                }
			}
		}
	}
}
