using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MovingVertPlatform : MonoBehaviour
{
    public float minYPos;
    public float maxYPos;
    float dirX, moveSpeed = 3f;

    private bool moveUp = true;
    
    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < minYPos)
            moveUp = true;
        if (transform.position.y > maxYPos)
            moveUp = false;

        if (moveUp)
            transform.position = new Vector2(transform.position.x, transform.position.y + moveSpeed*Time.deltaTime);
        else
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - moveSpeed*Time.deltaTime);
        }
    }
}
