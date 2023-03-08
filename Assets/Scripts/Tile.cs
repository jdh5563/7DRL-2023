using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public GameObject occupant = null;
    public GameObject type = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool IsUnoccupied()
    {
        return occupant == null || (occupant.tag != "Block" && occupant.tag != "Enemy" && occupant.tag != "Player");
	}
}
