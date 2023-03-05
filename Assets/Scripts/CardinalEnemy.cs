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
			newTileCoords.y += playerPos.y > currentTileCoords.y ? 1 : -1;
		}
		else if(playerPos.x != currentTileCoords.x)
		{
			newTileCoords.x += playerPos.x > currentTileCoords.x ? 1 : -1;
		}

		isMoving = true;
	}
}
