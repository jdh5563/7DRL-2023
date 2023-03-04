using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateGrid : MonoBehaviour
{
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private int gridWidth;
    [SerializeField] private int gridHeight;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < gridWidth; i++)
        {
            for(int j = 0; j < gridHeight; j++)
            {
                Instantiate(tilePrefab, new Vector2(i, j), Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
