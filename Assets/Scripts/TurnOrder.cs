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
		if (IsObjectTurn(turnOrder[0]))
		{
            turnOrder[0].GetComponent<MoveOnGrid>().TakeAction();
		}

        if (!turnOrder[0].GetComponent<MoveOnGrid>().canAct)
        {
            for (int i = 1; i < turnOrder.Count; i++)
            {
                if (IsObjectTurn(turnOrder[i]))
                {
					turnOrder[i].GetComponent<Enemy>().canAct = true;
				}
            }
        }
	}

    /// <summary>
    /// Return whether it is this object's turn
    /// </summary>
    /// <param name="obj">The object to check</param>
    public static bool IsObjectTurn(GameObject obj)
    {
        return obj == turnOrder[0] ? obj.GetComponent<MoveOnGrid>().turnTimer == 0 : obj.GetComponent<Enemy>().turnTimer == 0;

        //return turnOrder[currentTurn] == obj;
    }

    /// <summary>
    /// Move to the next object's turn
    /// </summary>
    public static void EndTurn(GameObject obj)
    {
		if (turnOrder[0].GetComponent<MoveOnGrid>().turnTimer != 0)
		{
			turnOrder[0].GetComponent<MoveOnGrid>().turnTimer--;
		}

		for (int i = 1; i < turnOrder.Count; i++)
        {
			if (turnOrder[i].GetComponent<Enemy>().turnTimer != 0)
			{
				turnOrder[i].GetComponent<Enemy>().turnTimer--;
			}
        }

		if (obj == turnOrder[0])
		{
            obj.GetComponent<MoveOnGrid>().turnTimer = obj.GetComponent<MoveOnGrid>().maxTimer;
		}
        else
        {
			obj.GetComponent<Enemy>().turnTimer -= obj.GetComponent<Enemy>().maxTimer;
		}

		//currentTurn++;
		//currentTurn %= turnOrder.Count;
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
