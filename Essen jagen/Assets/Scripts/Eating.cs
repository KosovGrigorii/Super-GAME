using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Eating : MonoBehaviour
{
    //[SerializeField] private Tilemap lalala;
    //public Collider2D foodCollider;
    public GameObject Food;
    public GameObject Player1;
    public Rigidbody2D rb2d;
    public float distanceFood;
    public float minX; 
    public float maxX;
    public float minY;
    public float maxY;

    private bool isGrounded;
    private bool isIced;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public Transform iceCheck;
    public LayerMask iceLayer;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        isIced = Physics2D.OverlapCircle(iceCheck.position, 0.2f, iceLayer);
        if (isGrounded || isIced) SpawnNewFood();;
        //if (Physics2D.IsTouchingLayers(foodCollider, 8)) 
            //SpawnNewFood();
        //else if (firstTeleportation) rb2d.bodyType = RigidbodyType2D.Dynamic;
        
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "CharacterA")
        {
            transform.position = new Vector3(0, 4);
            SpawnNewFood();
        }
        else if (collision.tag == "CharacterB")
        {
            transform.position = new Vector3(0, 4);
            SpawnNewFood();
        }
    }

    public void SpawnNewFood()
    {
        var newX = UnityEngine.Random.Range(minX, maxX);
        var newY = UnityEngine.Random.Range(minY, maxY);
        if (CheckingPoint(newX, newY, Player1.transform.position.x, Player1.transform.position.y))
            transform.position = new Vector3(newX, newY, -60);
        else SpawnNewFood();
    }

    public bool CheckingPoint(float x, float y,  float x0, float y0)
    {
        var dictOfWays = new Dictionary<Tuple<float, float>, bool>();
        var queue = new Queue<Tuple<float, float>>();
        for (float i = -distanceFood; i <= distanceFood; i += 0.5f)
            for (float j = -distanceFood; j <= distanceFood; j += 0.5f)
            {
                if (x + i <= minX || x + i >= maxX || y + j <= minY || y + j >= maxY) continue;
                dictOfWays[Tuple.Create(x + i, y + j)] = false;
            }
        
        queue.Enqueue(Tuple.Create(x, y));
        while (queue.Count != 0)
        {
            var point = queue.Dequeue();
            if (dictOfWays.ContainsKey(point) && dictOfWays[point]) continue;
            if ((point.Item1 - x0) * (point.Item1 - x0) + (point.Item2 - y0) * (point.Item2 - y0) < distanceFood * distanceFood)
                return false;
            dictOfWays[point] = true;
            for (var x1 = -0.5f; x1 <= 0.5f; x1+=0.5f)
                for (var y1 = -0.5f; y1 <= 0.5f; y1+=0.5f)
                {
                    if (x1 != 0 && y1 != 0) continue;
                    var newPoint = Tuple.Create(point.Item1 + x1, point.Item2 + y1);
                    if (!dictOfWays.ContainsKey(newPoint)) continue;
                    queue.Enqueue(newPoint);
                }
        }
        return true;
    }
}