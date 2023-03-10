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
    }

    // Update is called once per frame
    void Update()
    {

        IsExiting();
        OpenExit();
        DoorSpriteManager();

    }

    private void FixedUpdate()
    {
        
    }

    public bool IsExiting()
    {
        GameObject tile = CreateGrid.grid[tileCoords.x, tileCoords.y];

        if (tile.GetComponent<Tile>().occupant != null && tile.GetComponent<Tile>().occupant.tag == "Player" && open > 0)
        {
            TurnOrder.ResetTurnOrder();
            gameManager.GetComponent<CreateGrid>().ResetGrid();
            return true;
        }

        else
        {
            return false;
        }
    }

    private void OpenExit()
    {
        if (lever.GetComponent<Lever>().active || button.GetComponent<Button>().active)
        {
            open = 3;
        }
    }

    private void DoorSpriteManager()
    {
        if (open <= 1) GetComponent<SpriteRenderer>().color = new Color(.1686275f, .6313726f, .5529412f, 1);
        else if (open == 3) GetComponent<SpriteRenderer>().color = new Color(.007843138f, .8627451f, 1, 1);
        else if (open == 2) GetComponent<SpriteRenderer>().color = new Color(.9294118f, 1, .003921569f, 1);
    }
}
