using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardinalEnemy : Enemy
{
	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	protected override void Update()
	{
		base.Update();
	}

	protected override void FixedUpdate()
	{
		base.FixedUpdate();
	}

	/// <summary>
	/// Attack in one of the cardinal directions
	/// </summary>
	protected override void Attack()
	{
		player.GetComponent<Player>().TakeDamage(1);
	}

	/// <summary>
	/// Check if the player is in one of the cardinal directions
	/// </summary>
	protected override bool IsPlayerInRange()
	{
		Vector2Int playerPos = player.GetComponent<MoveOnGrid>().currentTileCoords;
		return (Mathf.Abs(playerPos.x - currentTileCoords.x) == 1 && playerPos.y == currentTileCoords.y) || (Mathf.Abs(playerPos.y - currentTileCoords.y) == 1 && playerPos.x == currentTileCoords.x);
	}

	/// <summary>
	/// Move in one of the cardinal directions
	/// </summary>
	protected override void Move()
	{
		newTileCoords = currentTileCoords;
		Vector2Int playerPos = player.GetComponent<MoveOnGrid>().currentTileCoords;

		if (playerPos.y != currentTileCoords.y)
		{
			int move = playerPos.y > currentTileCoords.y ? 1 : -1;

			if (move == 1)
			{
				GameObject rightTile = CreateGrid.grid[currentTileCoords.x, currentTileCoords.y + 1];
				if (rightTile.GetComponent<Tile>().occupant == null || rightTile.GetComponent<Tile>().occupant.tag != "Enemy")
				{
					newTileCoords.y += move;
					isMoving = true;
					return;
				}
			}
			else if (move == -1)
			{
				GameObject leftTile = CreateGrid.grid[currentTileCoords.x, currentTileCoords.y - 1];
				if (leftTile.GetComponent<Tile>().occupant == null || leftTile.GetComponent<Tile>().occupant.tag != "Enemy")
				{
					newTileCoords.y += move;
					isMoving = true;
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
				if (upTile.GetComponent<Tile>().occupant == null || upTile.GetComponent<Tile>().occupant.tag != "Enemy")
				{
					newTileCoords.x += move;
					isMoving = true;
					return;
				}
			}
			else if (move == -1) {
				GameObject downTile = CreateGrid.grid[currentTileCoords.x - 1, currentTileCoords.y];
				if (downTile.GetComponent<Tile>().occupant == null || downTile.GetComponent<Tile>().occupant.tag != "Enemy")
				{
					newTileCoords.x += move;
					isMoving = true;
					return;
				}
			}
		}

		isMoving = true;
	}
}
