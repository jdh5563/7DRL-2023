using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiagonalEnemy : Enemy
{
	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

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



		isMoving = true;
	}
}
