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

        GameObject tile = CreateGrid.grid[tileCoords.x, tileCoords.y];

        if (tile.GetComponent<Tile>().occupant != null && tile.GetComponent<Tile>().occupant.tag == "Player" && open > 0)
        {
            TurnOrder.ResetTurnOrder();
            gameManager.GetComponent<CreateGrid>().ResetGrid();
        }

        if ((lever == null && button == null) || (lever != null && lever.GetComponent<Lever>().active) || (button != null && button.GetComponent<Button>().active))
        {
            open = 2;
        }

    }
}
