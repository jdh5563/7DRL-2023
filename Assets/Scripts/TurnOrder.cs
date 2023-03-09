using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOrder : MonoBehaviour
{
    public static List<GameObject> turnOrder = new List<GameObject>();
    public static Exit exit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if (IsPlayerTurn())
		{
            turnOrder[0].GetComponent<MoveOnGrid>().TakeAction();
		}
        else
        {
            for (int i = 1; i < turnOrder.Count; i++)
            {
                if (turnOrder[i].GetComponent<Enemy>().turnTimer == 0)
                {
                    turnOrder[i].GetComponent<Enemy>().TakeAction();
				}
            }
        }
	}

    /// <summary>
    /// Return whether it is this object's turn
    /// </summary>
    /// <param name="obj">The object to check</param>
    public static bool IsPlayerTurn()
    {
		for (int i = 1; i < turnOrder.Count; i++)
		{
			if (turnOrder[i].GetComponent<Enemy>().turnTimer == 0)
			{
                return false;
			}
		}

        return true;
	}

    /// <summary>
    /// Move to the next object's turn
    /// </summary>
    public static void EndTurn(GameObject obj)
    {
        if (obj == turnOrder[0])
		{
            for(int i = 1; i < turnOrder.Count; i++)
            {
                if (turnOrder[i].GetComponent<Enemy>().turnTimer != 0)
                {
                    turnOrder[i].GetComponent<Enemy>().turnTimer--;
                }
            }

			if (exit.open - 1 >= 0) exit.open--;
            Debug.Log(exit.open);
		}
        else
        {
			obj.GetComponent<Enemy>().turnTimer = obj.GetComponent<Enemy>().maxTimer;
		}
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
    }
}
