using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinotaurEnemy : Enemy
{
	// Start is called before the first frame update
	void Start()
	{
		maxTimer = 1;
		turnTimer = maxTimer;
	}

	// Update is called once per frame
	protected override void FixedUpdate()
	{
		base.FixedUpdate();

		// Fix any error in positioning from movement lerp
		if(isMoving || transform.position.x != (int)transform.position.x) transform.position = new Vector2(transform.position.x + 0.5f, transform.position.y + 0.5f);
	}

	protected override void Attack()
	{
		player.GetComponent<Player>().TakeDamage(1);
		TurnOrder.EndTurn(gameObject);
	}

	protected override bool IsPlayerInRange()
	{
		return false;
	}

	protected override void Move()
	{
		// Bottom-Left quadrant of the minotaur is the base tile for its movement
		newTileCoords = currentTileCoords;
		Vector2Int playerPos = player.GetComponent<MoveOnGrid>().currentTileCoords;

		if (playerPos.y != currentTileCoords.y && playerPos.y != currentTileCoords.y + 1)
		{
			int move = playerPos.y > currentTileCoords.y ? 1 : -1;

			if (move == 1)
			{
				GameObject rightLowTile = CreateGrid.grid[currentTileCoords.x, currentTileCoords.y + 2];
				GameObject rightHighTile = CreateGrid.grid[currentTileCoords.x + 1, currentTileCoords.y + 2];
				if (rightLowTile.GetComponent<Tile>().IsUnoccupied() && rightHighTile.GetComponent<Tile>().IsUnoccupied())
				{
					newTileCoords.y += move;
					isMoving = true;
					CreateGrid.grid[currentTileCoords.x, currentTileCoords.y].GetComponent<Tile>().occupant = null;
					CreateGrid.grid[currentTileCoords.x + 1, currentTileCoords.y].GetComponent<Tile>().occupant = null;
					CreateGrid.grid[newTileCoords.x, newTileCoords.y + 1].GetComponent<Tile>().occupant = gameObject;
					CreateGrid.grid[newTileCoords.x + 1, newTileCoords.y + 1].GetComponent<Tile>().occupant = gameObject;
					return;
				}
				else
				{
					if (rightLowTile.GetComponent<Tile>().occupant != null && rightLowTile.GetComponent<Tile>().occupant.tag == "Block" && CreateGrid.IsValidTile(currentTileCoords.x, currentTileCoords.y + 3) && CreateGrid.grid[currentTileCoords.x, currentTileCoords.y + 3].GetComponent<Tile>().IsUnoccupied())
					{
						rightLowTile.GetComponent<Tile>().occupant.GetComponent<Block>().Move(0, 1, gameObject);
						return;
					}
					else if (rightHighTile.GetComponent<Tile>().occupant != null && rightHighTile.GetComponent<Tile>().occupant.tag == "Block" && CreateGrid.IsValidTile(currentTileCoords.x + 1, currentTileCoords.y + 3) && CreateGrid.grid[currentTileCoords.x + 1, currentTileCoords.y + 3].GetComponent<Tile>().IsUnoccupied())
					{
						rightHighTile.GetComponent<Tile>().occupant.GetComponent<Block>().Move(0, 1, gameObject);
						return;
					}
				}
			}
			else if (move == -1)
			{
				GameObject leftLowTile = CreateGrid.grid[currentTileCoords.x, currentTileCoords.y - 1];
				GameObject leftHighTile = CreateGrid.grid[currentTileCoords.x + 1, currentTileCoords.y - 1];
				if (leftLowTile.GetComponent<Tile>().IsUnoccupied() && leftHighTile.GetComponent<Tile>().IsUnoccupied())
				{
					newTileCoords.y += move;
					isMoving = true;
					CreateGrid.grid[currentTileCoords.x, currentTileCoords.y + 1].GetComponent<Tile>().occupant = null;
					CreateGrid.grid[currentTileCoords.x + 1, currentTileCoords.y + 1].GetComponent<Tile>().occupant = null;
					CreateGrid.grid[newTileCoords.x, newTileCoords.y].GetComponent<Tile>().occupant = gameObject;
					CreateGrid.grid[newTileCoords.x + 1, newTileCoords.y].GetComponent<Tile>().occupant = gameObject;
					return;
				}
				else
				{
					if (leftLowTile.GetComponent<Tile>().occupant != null && leftLowTile.GetComponent<Tile>().occupant.tag == "Block" && CreateGrid.IsValidTile(currentTileCoords.x, currentTileCoords.y - 2) && CreateGrid.grid[currentTileCoords.x + 1, currentTileCoords.y - 2].GetComponent<Tile>().IsUnoccupied())
					{
						leftLowTile.GetComponent<Tile>().occupant.GetComponent<Block>().Move(0, 1, gameObject);
						return;
					}
					else if (leftHighTile.GetComponent<Tile>().occupant != null && leftHighTile.GetComponent<Tile>().occupant.tag == "Block" && CreateGrid.IsValidTile(currentTileCoords.x + 1, currentTileCoords.y - 2) && CreateGrid.grid[currentTileCoords.x + 1, currentTileCoords.y - 2].GetComponent<Tile>().IsUnoccupied())
					{
						leftHighTile.GetComponent<Tile>().occupant.GetComponent<Block>().Move(0, 1, gameObject);
						return;
					}
				}
			}
		}


		if (playerPos.x != currentTileCoords.x && playerPos.x != currentTileCoords.x + 1)
		{
			int move = playerPos.x > currentTileCoords.x ? 1 : -1;

			if (move == 1)
			{
				GameObject upLeftTile = CreateGrid.grid[currentTileCoords.x + 2, currentTileCoords.y];
				GameObject upRightTile = CreateGrid.grid[currentTileCoords.x + 2, currentTileCoords.y + 1];
				if (upLeftTile.GetComponent<Tile>().IsUnoccupied() && upRightTile.GetComponent<Tile>().IsUnoccupied())
				{
					newTileCoords.y += move;
					isMoving = true;
					CreateGrid.grid[currentTileCoords.x, currentTileCoords.y].GetComponent<Tile>().occupant = null;
					CreateGrid.grid[currentTileCoords.x, currentTileCoords.y + 1].GetComponent<Tile>().occupant = null;
					CreateGrid.grid[newTileCoords.x + 1, newTileCoords.y].GetComponent<Tile>().occupant = gameObject;
					CreateGrid.grid[newTileCoords.x + 1, newTileCoords.y + 1].GetComponent<Tile>().occupant = gameObject;
					return;
				}
				else
				{
					if (upLeftTile.GetComponent<Tile>().occupant != null && upLeftTile.GetComponent<Tile>().occupant.tag == "Block" && CreateGrid.IsValidTile(currentTileCoords.x + 3, currentTileCoords.y) && CreateGrid.grid[currentTileCoords.x + 3, currentTileCoords.y].GetComponent<Tile>().IsUnoccupied())
					{
						upLeftTile.GetComponent<Tile>().occupant.GetComponent<Block>().Move(0, 1, gameObject);
						return;
					}
					else if (upRightTile.GetComponent<Tile>().occupant != null && upRightTile.GetComponent<Tile>().occupant.tag == "Block" && CreateGrid.IsValidTile(currentTileCoords.x + 3, currentTileCoords.y + 1) && CreateGrid.grid[currentTileCoords.x + 3, currentTileCoords.y + 1].GetComponent<Tile>().IsUnoccupied())
					{
						upRightTile.GetComponent<Tile>().occupant.GetComponent<Block>().Move(0, 1, gameObject);
						return;
					}
				}
			}
			else if (move == -1)
			{
				GameObject downLeftTile = CreateGrid.grid[currentTileCoords.x - 1, currentTileCoords.y];
				GameObject downRightTile = CreateGrid.grid[currentTileCoords.x - 1, currentTileCoords.y + 1];
				if (downLeftTile.GetComponent<Tile>().IsUnoccupied() && downRightTile.GetComponent<Tile>().IsUnoccupied())
				{
					newTileCoords.y += move;
					isMoving = true;
					CreateGrid.grid[currentTileCoords.x + 1, currentTileCoords.y].GetComponent<Tile>().occupant = null;
					CreateGrid.grid[currentTileCoords.x + 1, currentTileCoords.y + 1].GetComponent<Tile>().occupant = null;
					CreateGrid.grid[newTileCoords.x, newTileCoords.y].GetComponent<Tile>().occupant = gameObject;
					CreateGrid.grid[newTileCoords.x, newTileCoords.y + 1].GetComponent<Tile>().occupant = gameObject;
					return;
				}
				else
				{
					if (downLeftTile.GetComponent<Tile>().occupant != null && downLeftTile.GetComponent<Tile>().occupant.tag == "Block" && CreateGrid.IsValidTile(currentTileCoords.x - 2, currentTileCoords.y) && CreateGrid.grid[currentTileCoords.x - 2, currentTileCoords.y].GetComponent<Tile>().IsUnoccupied())
					{
						downLeftTile.GetComponent<Tile>().occupant.GetComponent<Block>().Move(0, 1, gameObject);
						return;
					}
					else if (downRightTile.GetComponent<Tile>().occupant != null && downRightTile.GetComponent<Tile>().occupant.tag == "Block" && CreateGrid.IsValidTile(currentTileCoords.x - 2, currentTileCoords.y + 1) && CreateGrid.grid[currentTileCoords.x - 2, currentTileCoords.y + 1].GetComponent<Tile>().IsUnoccupied())
					{
						downRightTile.GetComponent<Tile>().occupant.GetComponent<Block>().Move(0, 1, gameObject);
						return;
					}
				}
			}
		}

		isMoving = true;
	}
}
