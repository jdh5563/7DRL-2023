using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudEnemy : Enemy
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
	}

	protected override void Attack()
	{
		// Move player
		GameObject tile = CreateGrid.grid[currentTileCoords.x, currentTileCoords.y + (int)Input.GetAxisRaw("Horizontal")];

		if (tile.GetComponent<Tile>().IsUnoccupied())
		{
			player.GetComponent<MoveOnGrid>().oldTileCoords = player.GetComponent<MoveOnGrid>().currentTileCoords;
			player.GetComponent<MoveOnGrid>().currentTileCoords.x += (int)Mathf.Sign(player.GetComponent<MoveOnGrid>().currentTileCoords.x - currentTileCoords.x);
			player.GetComponent<MoveOnGrid>().currentTileCoords.y += (int)Mathf.Sign(player.GetComponent<MoveOnGrid>().currentTileCoords.y - currentTileCoords.y);
			CreateGrid.grid[player.GetComponent<MoveOnGrid>().oldTileCoords.x, player.GetComponent<MoveOnGrid>().oldTileCoords.y].GetComponent<Tile>().occupant = null;
			CreateGrid.grid[player.GetComponent<MoveOnGrid>().currentTileCoords.x, player.GetComponent<MoveOnGrid>().currentTileCoords.y].GetComponent<Tile>().occupant = gameObject;
			StartCoroutine(PushPlayer()); // Throw this into an update loop
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
		transform.position = Vector2.Lerp(CreateGrid.grid[oldPlayerCoords.x, oldPlayerCoords.y].transform.position, CreateGrid.grid[currentPlayerCoords.x, currentPlayerCoords.y].transform.position, percentDone);
		percentDone += 0.05f;

		if (percentDone >= 1f)
		{
			transform.position = CreateGrid.grid[currentPlayerCoords.x, currentPlayerCoords.y].transform.position;
			percentDone = 0.05f;
			isMoving = false;
			TurnOrder.EndTurn(gameObject);
		}

		yield return null;
	}
}
