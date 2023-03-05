using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOrder : MonoBehaviour
{
    public static List<GameObject> turnOrder = new List<GameObject>();
    private static int currentTurn = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Return whether it is this object's turn
    /// </summary>
    /// <param name="obj">The object to check</param>
    public static bool IsObjectTurn(GameObject obj)
    {
        return turnOrder[currentTurn] == obj;
    }

    /// <summary>
    /// Move to the next object's turn
    /// </summary>
    public static void EndTurn()
    {
        currentTurn++;
        currentTurn %= turnOrder.Count;
    }

    /// <summary>
    /// Destroy all objects and reset the turn order
    /// </summary>
    public static void ResetTurnOrder()
    {
        while(turnOrder.Count > 0)
        {
            GameObject entity = turnOrder[0];
            turnOrder.RemoveAt(0);
            Destroy(entity);
        }

        currentTurn = 0;
    }
}
