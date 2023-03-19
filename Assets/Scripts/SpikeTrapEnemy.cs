using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrapEnemy : Enemy
{
	private int stepLimit = 3;
	private int stepCount = 0;
	private Vector2Int[] spikeDistances = { Vector2Int.zero, Vector2Int.zero, Vector2Int.zero, Vector2Int.zero };

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
		if (stepCount < stepLimit - 1)
		{
			stepCount++;
		}
		else
		{
			stepCount = 0;

			for(int i = 0; i < spikeDistances.Length; i++)
			{
				if (CreateGrid.grid[currentTileCoords.x + spikeDistances[i].x, currentTileCoords.y + spikeDistances[i].y].GetComponent<Tile>().occupant != null)
				{
					if (CreateGrid.grid[currentTileCoords.x + spikeDistances[i].x, currentTileCoords.y + spikeDistances[i].y].GetComponent<Tile>().occupant.tag == "Player")
					{
						player.GetComponent<Player>().TakeDamage(1);
					}
					else if (CreateGrid.grid[currentTileCoords.x + spikeDistances[i].x, currentTileCoords.y + spikeDistances[i].y].GetComponent<Tile>().occupant.tag == "Enemy")
					{
						CreateGrid.grid[currentTileCoords.x + spikeDistances[i].x, currentTileCoords.y + spikeDistances[i].y].GetComponent<Tile>().occupant.GetComponent<Enemy>().stunned = true;
					}
				}
			}
		}

		TurnOrder.EndTurn(gameObject);
	}

    protected override bool IsPlayerInRange()
    {
		UpdateSpikeDistances();

		Vector2Int playerPos = player.GetComponent<MoveOnGrid>().currentTileCoords;
		return Mathf.Abs(playerPos.x - currentTileCoords.x) == Mathf.Abs(playerPos.y - currentTileCoords.y);
	}

    protected override void Move()
    {
        TurnOrder.EndTurn(gameObject);
    }

	private void UpdateSpikeDistances()
	{
		for (int i = 0; i < spikeDistances.Length; i++)
		{
			int nextX;
			int nextY;
			spikeDistances[i] = Vector2Int.zero;

			switch (i)
			{
				case 0:
					nextX = 1;
					nextY = 1;
					break;
				case 1:
					nextX = -1;
					nextY = 1;
					break;
				case 2:
					nextX = -1;
					nextY = -1;
					break;
				default:
					nextX = 1;
					nextY = -1;
					break;
			}

			while (CreateGrid.IsValidTile(currentTileCoords.x + nextX, currentTileCoords.y + nextY))
			{
				if (CreateGrid.grid[currentTileCoords.x + nextX, currentTileCoords.y + nextY].GetComponent<Tile>().occupant != null ||
					!CreateGrid.IsValidTile(currentTileCoords.x + nextX + (int)Mathf.Sign(nextX), currentTileCoords.y + nextY + (int)Mathf.Sign(nextY)))
				{
					spikeDistances[i] = new Vector2Int(nextX, nextY);
					break;
				}

				nextX += (int)Mathf.Sign(nextX);
				nextY += (int)Mathf.Sign(nextY);
			}
		}
	}
}
