using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    public int currentHealth;

    private GameObject gameManager;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        gameManager = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponentInChildren<TextMeshPro>().SetText(currentHealth.ToString());
    }

    /// <summary>
    /// Receive a given amount of damage, and reset if the damage is fatal
    /// </summary>
    /// <param name="damage">The amount of damage to receive</param>
    public void TakeDamage(int damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, int.MaxValue);
        Debug.Log(currentHealth);

        // Reset if the player died. Otherwise end the turn
        if(currentHealth == 0)
        {
			TurnOrder.ResetTurnOrder();
			gameManager.GetComponent<CreateGrid>().ResetGrid();
        }
    }
}
