using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationaryCardinalEnemy : Enemy
{

    private bool shotPrepared = false;

    // Start is called before the first frame update
    void Start()
    {
        maxTimer = 1;
        turnTimer = maxTimer;
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
    protected override void Attack()
    {
        if (shotPrepared)
        {
            if (IsPlayerInRange())
            {
                player.GetComponent<Player>().TakeDamage(1);
                TurnOrder.EndTurn(gameObject);
            }

            else if (IsObstructed())
            {

                TurnOrder.EndTurn(gameObject);
            }

            shotPrepared = false;
        }

        else
        {
            shotPrepared = true;
            TurnOrder.EndTurn(gameObject);
        }
    }

    protected override bool IsPlayerInRange()
    {

        Vector2Int playerPos = player.GetComponent<MoveOnGrid>().currentTileCoords;

        if (shotPrepared && )
        {

        }

        return (playerPos.x == currentTileCoords.x || playerPos.y == currentTileCoords.y);
    }

    protected override void Move()
    {
        TurnOrder.EndTurn(gameObject);
    }
}
