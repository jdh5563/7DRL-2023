using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienEnemy : Enemy
{
	// Start is called before the first frame update
	void Start()
	{
		maxTimer = 4;
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
		return (Mathf.Abs(playerPos.x - currentTileCoords.x) <= 3 && Mathf.Abs(playerPos.y - currentTileCoords.y) == 3) ||
			   (Mathf.Abs(playerPos.x - currentTileCoords.x) == 3 && Mathf.Abs(playerPos.y - currentTileCoords.y) <= 3);
	}

	protected override void Move()
	{
		TurnOrder.EndTurn(gameObject);
	}
}
