using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportation : MonoBehaviour
{
    public GameObject Portal;
    public GameObject Player1;
    public GameObject Player2;
    public float offsetX;
    public float offsetY;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag=="CharacterA")
        {
            StartCoroutine(Teleport(Player1));
        }
        
        else if (collision.gameObject.tag=="CharacterB")
        {
            StartCoroutine(Teleport(Player2));
        }
    }
    
    IEnumerator Teleport(GameObject player)
    {
        yield return new WaitForSeconds(0.2f);
        player.transform.position = new Vector2(Portal.transform.position.x + offsetX, Portal.transform.position.y + offsetY);
    }
}
