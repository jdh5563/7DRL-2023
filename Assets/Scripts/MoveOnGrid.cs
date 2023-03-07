using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnGrid : MonoBehaviour
{
    private Vector2 basePosition;
    private Vector2 endPosition;
    private bool isMoving = false;
    private float percentDone = 0.05f;

    public GameObject block = null;

    public Vector2Int currentTileCoords;
    private Vector2Int oldTileCoords;

    // Start is called before the first frame update
    void Start()
    {
        basePosition = transform.position;
        endPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // If it is the player's turn, move to the chosen tile
        if (TurnOrder.IsObjectTurn(gameObject))
        {
            GameObject tile;

			if (!isMoving && Input.GetAxisRaw("Horizontal") != 0)
            {
                tile = CreateGrid.grid[currentTileCoords.x, currentTileCoords.y + (int)Input.GetAxisRaw("Horizontal")];

                if (tile.GetComponent<Tile>().occupant == null || (tile.GetComponent<Tile>().occupant.tag != "Block" && tile.GetComponent<Tile>().occupant.tag != "Enemy"))
                {
                    isMoving = true;
                    endPosition = new Vector2(basePosition.x + Input.GetAxisRaw("Horizontal"), basePosition.y);
                    oldTileCoords = currentTileCoords;
                    currentTileCoords.y += (int)Input.GetAxisRaw("Horizontal");
                }
                else if (tile.GetComponent<Tile>().occupant.tag == "Block")
                {
                    block.GetComponent<Block>().Move((int)Input.GetAxisRaw("Horizontal"), false);
                    TurnOrder.EndTurn();
                }
            }
            else if (!isMoving && Input.GetAxisRaw("Vertical") != 0)
            {
                tile = CreateGrid.grid[currentTileCoords.x + (int)Input.GetAxisRaw("Vertical"), currentTileCoords.y];

                if (tile.GetComponent<Tile>().occupant == null || (tile.GetComponent<Tile>().occupant.tag != "Block" && tile.GetComponent<Tile>().occupant.tag != "Enemy"))
                {
                    isMoving = true;
                    endPosition = new Vector2(basePosition.x, basePosition.y + Input.GetAxisRaw("Vertical"));
                    oldTileCoords = currentTileCoords;
                    currentTileCoords.x += (int)Input.GetAxisRaw("Vertical");
                }
                else if (tile.GetComponent<Tile>().occupant.tag == "Block")
                {
                    block.GetComponent<Block>().Move((int)Input.GetAxisRaw("Vertical"), true);
                    TurnOrder.EndTurn();
                }
            }
        }
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
				CreateGrid.grid[oldTileCoords.x, oldTileCoords.y].GetComponent<Tile>().occupant = null;
				CreateGrid.grid[currentTileCoords.x, currentTileCoords.y].GetComponent<Tile>().occupant = gameObject;

				transform.position = endPosition;
                basePosition = endPosition;
                percentDone = 0.05f;
                isMoving = false;
				TurnOrder.EndTurn();
			}
        }
	}
}
