using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudEnemy : Enemy
{
	private bool isBlowing = false;

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

		if (isBlowing) StartCoroutine(PushPlayer());
	}

	protected override void Attack()
	{
		// Move player
		Vector2Int playerDirection = new Vector2Int(
			player.GetComponent<MoveOnGrid>().currentTileCoords.x - currentTileCoords.x == 0 ? 0 : (int)Mathf.Sign(player.GetComponent<MoveOnGrid>().currentTileCoords.x - currentTileCoords.x),
			player.GetComponent<MoveOnGrid>().currentTileCoords.y - currentTileCoords.y == 0 ? 0 : (int)Mathf.Sign(player.GetComponent<MoveOnGrid>().currentTileCoords.y - currentTileCoords.y));

		if (CreateGrid.IsValidTile(player.GetComponent<MoveOnGrid>().currentTileCoords.x + playerDirection.x, player.GetComponent<MoveOnGrid>().currentTileCoords.y + playerDirection.y))
		{
			GameObject tile = CreateGrid.grid[player.GetComponent<MoveOnGrid>().currentTileCoords.x + playerDirection.x, player.GetComponent<MoveOnGrid>().currentTileCoords.y + playerDirection.y];

			if (!isBlowing)
			{
				if (tile.GetComponent<Tile>().IsUnoccupied())
				{
					player.GetComponent<MoveOnGrid>().oldTileCoords = player.GetComponent<MoveOnGrid>().currentTileCoords;
					player.GetComponent<MoveOnGrid>().currentTileCoords.x += playerDirection.x;
					player.GetComponent<MoveOnGrid>().currentTileCoords.y += playerDirection.y;
					CreateGrid.grid[player.GetComponent<MoveOnGrid>().oldTileCoords.x, player.GetComponent<MoveOnGrid>().oldTileCoords.y].GetComponent<Tile>().occupant = null;
					CreateGrid.grid[player.GetComponent<MoveOnGrid>().currentTileCoords.x, player.GetComponent<MoveOnGrid>().currentTileCoords.y].GetComponent<Tile>().occupant = gameObject;
					StartCoroutine(PushPlayer());
					isBlowing = true;
				}
				else
				{
					TurnOrder.EndTurn(gameObject);
				}
			}
		}
		else
		{
			TurnOrder.EndTurn(gameObject);
		}
	}

	protected override bool IsPlayerInRange()
	{
		Vector2Int playerPos = player.GetComponent<MoveOnGrid>().currentTileCoords;
		return playerPos.x == currentTileCoords.x || playerPos.y == currentTileCoords.y;
	}

	protected override void Move()
	{
		TurnOrder.EndTurn(gameObject);
	}

	private IEnumerator PushPlayer()
	{
		Vector2Int oldPlayerCoords = player.GetComponent<MoveOnGrid>().oldTileCoords;
		Vector2Int currentPlayerCoords = player.GetComponent<MoveOnGrid>().currentTileCoords;

		// Move to the designated tile
		player.transform.position = Vector2.Lerp(CreateGrid.grid[oldPlayerCoords.x, oldPlayerCoords.y].transform.position, CreateGrid.grid[currentPlayerCoords.x, currentPlayerCoords.y].transform.position, percentDone);
		percentDone += 0.05f;

		if (percentDone >= 1f)
		{
			player.transform.position = CreateGrid.grid[currentPlayerCoords.x, currentPlayerCoords.y].transform.position;
			percentDone = 0.05f;
			TurnOrder.EndTurn(gameObject);
			isBlowing = false;
		}

		yield return null;
	}
}
