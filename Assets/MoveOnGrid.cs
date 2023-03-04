using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnGrid : MonoBehaviour
{
    private Vector2 basePosition;
    private Vector2 endPosition;
    private bool isMoving = false;
    private float percentDone = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
        basePosition = transform.position;
        endPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isMoving && Input.GetAxisRaw("Horizontal") != 0)
        {
            isMoving = true;
            endPosition = new Vector2(basePosition.x + Input.GetAxisRaw("Horizontal"), basePosition.y);
        }
        else if(!isMoving && Input.GetAxisRaw("Vertical") != 0)
        {
			isMoving = true;
			endPosition = new Vector2(basePosition.x, basePosition.y + Input.GetAxisRaw("Vertical"));
		}
    }

	private void FixedUpdate()
	{
		if (isMoving)
        {
            transform.position = Vector2.Lerp(basePosition, endPosition, percentDone);
            percentDone += 0.05f;

            if(percentDone >= 1f)
            {
                transform.position = endPosition;
                basePosition = endPosition;
                percentDone = 0.05f;
                isMoving = false;
            }
        }
	}
}
