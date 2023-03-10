using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Exit : MonoBehaviour
{

    public Vector2Int tileCoords;
    public int open = 0;

    private GameObject gameManager;
    public GameObject button = null;
    public GameObject lever = null;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager");

        OpenExit();
    }

    // Update is called once per frame
    void Update()
    {

        IsExiting();
        OpenExit();

    }

    private void FixedUpdate()
    {
        
    }

    public bool IsExiting()
    {
        GameObject tile = CreateGrid.grid[tileCoords.x, tileCoords.y];

        if (tile.GetComponent<Tile>().occupant != null && tile.GetComponent<Tile>().occupant.tag == "Player" && !tile.GetComponent<Tile>().occupant.GetComponent<MoveOnGrid>().isMoving && open > 0)
        {
            TurnOrder.ResetTurnOrder();
            gameManager.GetComponent<CreateGrid>().ResetGrid();
            return true;
        }

        else return false;
    }

    public bool OpenExit()
    {
        if ((lever == null && button == null) || (lever != null && lever.GetComponent<Lever>().active) || (button != null && button.GetComponent<Button>().active))
        {
            open = 2;
            return true;
        }

        return false;
    }
}
