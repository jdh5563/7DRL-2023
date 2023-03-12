using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinotaurEnemy : Enemy
{
	// Start is called before the first frame update
	void Start()
	{
		maxTimer = 6;
		turnTimer = maxTimer;
	}

	// Update is called once per frame
	protected override void FixedUpdate()
	{
		base.FixedUpdate();
	}

	protected override void Attack()
	{
		player.GetComponent<Player>().TakeDamage(1);
		TurnOrder.EndTurn(gameObject);
	}

	protected override bool IsPlayerInRange()
	{
		throw new System.NotImplementedException();
	}

	protected override void Move()
	{
		// Bottom-Left quadrant of the minotaur is the base tile for its movement
		newTileCoords = currentTileCoords;
		Vector2Int playerPos = player.GetComponent<MoveOnGrid>().currentTileCoords;

		if (playerPos.y != currentTileCoords.y)
		{
			int move = playerPos.y > currentTileCoords.y ? 1 : -1;

			if (move == 1)
			{
				GameObject rightTile = CreateGrid.grid[currentTileCoords.x, currentTileCoords.y + 1];
				if (rightTile.GetComponent<Tile>().IsUnoccupied())
				{
					newTileCoords.y += move;
					isMoving = true;
					CreateGrid.grid[currentTileCoords.x, currentTileCoords.y].GetComponent<Tile>().occupant = null;
					CreateGrid.grid[newTileCoords.x, newTileCoords.y].GetComponent<Tile>().occupant = gameObject;
					return;
				}
				else if (rightTile.GetComponent<Tile>().occupant.tag == "Block" && CreateGrid.IsValidTile(currentTileCoords.x, currentTileCoords.y + 2) && CreateGrid.grid[currentTileCoords.x, currentTileCoords.y + 2].GetComponent<Tile>().IsUnoccupied())
				{
					rightTile.GetComponent<Tile>().occupant.GetComponent<Block>().Move(1, false, gameObject);
					return;
				}
			}
			else if (move == -1)
			{
				GameObject leftTile = CreateGrid.grid[currentTileCoords.x, currentTileCoords.y - 1];
				if (leftTile.GetComponent<Tile>().IsUnoccupied())
				{
					newTileCoords.y += move;
					isMoving = true;
					CreateGrid.grid[currentTileCoords.x, currentTileCoords.y].GetComponent<Tile>().occupant = null;
					CreateGrid.grid[newTileCoords.x, newTileCoords.y].GetComponent<Tile>().occupant = gameObject;
					return;
				}
				else if (leftTile.GetComponent<Tile>().occupant.tag == "Block" && CreateGrid.IsValidTile(currentTileCoords.x, currentTileCoords.y - 2) && CreateGrid.grid[currentTileCoords.x, currentTileCoords.y - 2].GetComponent<Tile>().IsUnoccupied())
				{
					leftTile.GetComponent<Tile>().occupant.GetComponent<Block>().Move(-1, false, gameObject);
					return;
				}
			}
		}


		if (playerPos.x != currentTileCoords.x)
		{
			int move = playerPos.x > currentTileCoords.x ? 1 : -1;

			if (move == 1)
			{
				GameObject upTile = CreateGrid.grid[currentTileCoords.x + 1, currentTileCoords.y];
				if (upTile.GetComponent<Tile>().IsUnoccupied())
				{
					newTileCoords.x += move;
					isMoving = true;
					CreateGrid.grid[currentTileCoords.x, currentTileCoords.y].GetComponent<Tile>().occupant = null;
					CreateGrid.grid[newTileCoords.x, newTileCoords.y].GetComponent<Tile>().occupant = gameObject;
					return;
				}
				else if (upTile.GetComponent<Tile>().occupant.tag == "Block" && CreateGrid.IsValidTile(currentTileCoords.x + 2, currentTileCoords.y) && CreateGrid.grid[currentTileCoords.x + 2, currentTileCoords.y].GetComponent<Tile>().IsUnoccupied())
				{
					upTile.GetComponent<Tile>().occupant.GetComponent<Block>().Move(1, true, gameObject);
					return;
				}
			}
			else if (move == -1)
			{
				GameObject downTile = CreateGrid.grid[currentTileCoords.x - 1, currentTileCoords.y];
				if (downTile.GetComponent<Tile>().IsUnoccupied())
				{
					newTileCoords.x += move;
					isMoving = true;
					CreateGrid.grid[currentTileCoords.x, currentTileCoords.y].GetComponent<Tile>().occupant = null;
					CreateGrid.grid[newTileCoords.x, newTileCoords.y].GetComponent<Tile>().occupant = gameObject;
					return;
				}
				else if (downTile.GetComponent<Tile>().occupant.tag == "Block" && CreateGrid.IsValidTile(currentTileCoords.x - 2, currentTileCoords.y) && CreateGrid.grid[currentTileCoords.x - 2, currentTileCoords.y].GetComponent<Tile>().IsUnoccupied())
				{
					downTile.GetComponent<Tile>().occupant.GetComponent<Block>().Move(-1, true, gameObject);
					return;
				}
			}
		}

		isMoving = true;
	}
}