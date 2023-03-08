using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
	public Vector2Int currentTileCoords;
	public GameObject player = null;
	protected Vector2Int newTileCoords;
	protected bool isMoving = false;
    protected float percentDone = 0.05f;

	public int maxTimer;
	public int turnTimer;
	public bool canAct = false;

	// Start is called before the first frame update
	void Start()
    {
		
	}

	// Update is called once per frame
	protected virtual void Update()
    {
		// If it is this enemy's turn, try to attack or move
		if (canAct && !player.GetComponent<MoveOnGrid>().block.GetComponent<Block>().isMoving) {
			if (IsPlayerInRange()) Attack();
			else if (!isMoving) Move();
		}
    }

	protected virtual void FixedUpdate()
	{
		// Move to the designated point
		if (isMoving)
		{
			transform.position = Vector2.Lerp(CreateGrid.grid[currentTileCoords.x, currentTileCoords.y].transform.position, CreateGrid.grid[newTileCoords.x, newTileCoords.y].transform.position, percentDone);
			percentDone += 0.05f;

			if (percentDone >= 1f)
			{
				CreateGrid.grid[currentTileCoords.x, currentTileCoords.y].GetComponent<Tile>().occupant = null;
				CreateGrid.grid[newTileCoords.x, newTileCoords.y].GetComponent<Tile>().occupant = gameObject;

				transform.position = CreateGrid.grid[newTileCoords.x, newTileCoords.y].transform.position;
				currentTileCoords = newTileCoords;
				percentDone = 0.05f;
				isMoving = false;
				//TurnOrder.EndTurn(gameObject);
			}
		}
	}

	protected abstract void Move();
    protected abstract void Attack();
    protected abstract bool IsPlayerInRange();
}
