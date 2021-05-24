using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MovingHorPlatform : MonoBehaviour
{
    public float minXPos;
    public float maxXPos;
    float dirX, moveSpeed = 3f;

    private bool moveLeft = true;
    
    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < minXPos)
            moveLeft = true;
        if (transform.position.x > maxXPos)
            moveLeft = false;

        if (moveLeft)
            transform.position = new Vector2(transform.position.x + moveSpeed*Time.deltaTime, transform.position.y);
        else
        {
            transform.position = new Vector2(transform.position.x - moveSpeed*Time.deltaTime, transform.position.y);
        }
    }
}