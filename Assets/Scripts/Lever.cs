using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Lever : MonoBehaviour
{

    public Vector2Int tileCoords;
    public bool active = false;

    private bool justFlipped = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        GameObject tile = CreateGrid.grid[tileCoords.x, tileCoords.y];

        if (tile.GetComponent<Tile>().occupant != null && !justFlipped)
        {
            active = !active;
            justFlipped = true;
        }
        else if (tile.GetComponent<Tile>().occupant == null && justFlipped)
        {
            justFlipped = false;
        }
    }
}
