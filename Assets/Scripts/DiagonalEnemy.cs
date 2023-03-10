using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiagonalEnemy : Enemy
{
	// Start is called before the first frame update
	void Start()
	{
		maxTimer = 3;
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
		Vector2Int playerPos = player.GetComponent<MoveOnGrid>().currentTileCoords;
		return Mathf.Abs(playerPos.x - currentTileCoords.x) == 1 && Mathf.Abs(playerPos.y - currentTileCoords.y) == 1;
	}

	protected override void Move()
	{
		newTileCoords = currentTileCoords;
		Vector2Int playerPos = player.GetComponent<MoveOnGrid>().currentTileCoords;

		if(playerPos.y != currentTileCoords.y)
		{
			int moveHorizontal = playerPos.y > currentTileCoords.y ? 1 : -1;
			int moveVertical;

			if (playerPos.x != currentTileCoords.x)
			{
				moveVertical = playerPos.x > currentTileCoords.x ? 1 : -1;
			}
			else
			{
				moveVertical = Random.Range(0f, 1f) < 0.5f ? 1 : -1;
			}

			GameObject chosenTile = CreateGrid.grid[currentTileCoords.x + moveVertical, currentTileCoords.y + moveHorizontal];
			if (chosenTile.GetComponent<Tile>().IsUnoccupied())
			{
				newTileCoords.x += moveVertical;
				newTileCoords.y += moveHorizontal;
				isMoving = true;
				CreateGrid.grid[currentTileCoords.x, currentTileCoords.y].GetComponent<Tile>().occupant = null;
				CreateGrid.grid[newTileCoords.x, newTileCoords.y].GetComponent<Tile>().occupant = gameObject;
				return;
			}
			else if (chosenTile.GetComponent<Tile>().occupant.tag == "Block" && CreateGrid.IsValidTile(currentTileCoords.x + moveVertical * 2, currentTileCoords.y + moveHorizontal * 2) && CreateGrid.grid[currentTileCoords.x + moveVertical * 2, currentTileCoords.y + moveHorizontal * 2].GetComponent<Tile>().IsUnoccupied())
			{
				chosenTile.GetComponent<Tile>().occupant.GetComponent<Block>().Move(1, false, gameObject);
				return;
			}
		}

		if(playerPos.x != currentTileCoords.x)
		{
			int moveVertical = playerPos.x > currentTileCoords.x ? 1 : -1;
			int moveHorizontal;

			if (playerPos.y != currentTileCoords.y)
			{
				moveHorizontal = playerPos.y > currentTileCoords.y ? 1 : -1;
			}
			else
			{
				moveHorizontal = Random.Range(0f, 1f) < 0.5f ? 1 : -1;
			}

			GameObject chosenTile = CreateGrid.grid[currentTileCoords.x + moveVertical, currentTileCoords.y + moveHorizontal];
			if (chosenTile.GetComponent<Tile>().IsUnoccupied())
			{
				newTileCoords.x += moveVertical;
				newTileCoords.y += moveHorizontal;
				isMoving = true;
				CreateGrid.grid[currentTileCoords.x, currentTileCoords.y].GetComponent<Tile>().occupant = null;
				CreateGrid.grid[newTileCoords.x, newTileCoords.y].GetComponent<Tile>().occupant = gameObject;
				return;
			}
			else if (chosenTile.GetComponent<Tile>().occupant.tag == "Block" && CreateGrid.IsValidTile(currentTileCoords.x + moveVertical * 2, currentTileCoords.y + moveHorizontal * 2) && CreateGrid.grid[currentTileCoords.x + moveVertical * 2, currentTileCoords.y + moveHorizontal * 2].GetComponent<Tile>().IsUnoccupied())
			{
				chosenTile.GetComponent<Tile>().occupant.GetComponent<Block>().Move(1, false, gameObject);
				return;
			}
		}

		isMoving = true;
	}
}
