using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{

    public Vector2Int tileCoords;
    public bool active = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        GameObject tile = CreateGrid.grid[tileCoords.x, tileCoords.y];

        // Checks if an entity is on the button
        if (tile.GetComponent<Tile>().occupant != null) active = true; 
        else active = false;
    }
}
