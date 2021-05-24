using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;
using UnityEngine.SceneManagement;

public class CharacterA : MonoBehaviour
{
    public float moveSpeed;
    private float xInput, yInput;
    private Rigidbody2D rb2d;
    private SpriteRenderer sp;
    public float jumpForce;
    private bool isGrounded;
    public Transform groundCheck;
    public Transform groundCheck1;
    public Transform groundCheck2;
    public LayerMask groundLayer;
    public LayerMask iceLayer;
    public bool isIce;
    public bool isIce1;
    public bool isIce2;
    public Vector2 oldVelocity;

    public int maxHealth = 1000;
    public int currentHealth;
    public HealthBarA healthBar;
    public int coef = 1;
    private bool isEating = false;
    private int tempMaxHealth;
    public int satiety;
    public string DeathScene;
    
    public AudioSource jump;
    public AudioSource runOnGrass;
    public AudioSource eatingFood;
    public AudioSource teleportation;
    public AudioSource runOnIce;
    private bool isSecondSong;

    public KeyCode upCode;
    public KeyCode leftCode;
    public KeyCode rightCode;



    private bool canDoubleJump;
    

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();

        sp = GetComponent<SpriteRenderer>(); 
    }
    // Start is called before the first frame update
    void Start()
    {
        tempMaxHealth = maxHealth;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(upCode))
        {
            if (isGrounded||isIce)
            {
                Jump();
                canDoubleJump = true;
            }
            
            else if (canDoubleJump)
            {
                Jump();
                canDoubleJump = false;
            }
        }
    }

    private void FixedUpdate()
    {
        if ((Input.GetKey(leftCode) || Input.GetKey(rightCode)) && isGrounded)
        {
            if (!runOnGrass.isPlaying)
                runOnGrass.Play();
        }
        else runOnGrass.Stop();
        if ((Input.GetKey(leftCode) || Input.GetKey(rightCode)) && isIce)
        {
            if (!runOnIce.isPlaying)
                runOnIce.Play();
        }
        else runOnIce.Stop();
        
        xInput = Input.GetKey(leftCode) ? -1 :
            Input.GetKey(rightCode) ? 1 : 0;

        transform.Translate(xInput * moveSpeed, yInput * moveSpeed, 0);
        
        isIce = Physics2D.OverlapCircle(groundCheck.position, 0.2f, iceLayer);
        isIce1 = Physics2D.OverlapCircle(groundCheck1.position, 0.2f, iceLayer);
        isIce2 = Physics2D.OverlapCircle(groundCheck2.position, 0.2f, iceLayer);
        
        if ((isIce || isIce1 || isIce2) && xInput == 0)
        {
            rb2d.AddForce(oldVelocity);
            oldVelocity = new Vector2(moveSpeed * xInput, rb2d.velocity.y);
            //rb2d.velocity = new Vector2(oldVelocity.x/2, rb2d.velocity.y);

        }
        
        else if ((isIce || isIce1 || isIce2) && xInput != 0)
        {
            rb2d.velocity = new Vector2(moveSpeed * xInput*25, rb2d.velocity.y);
            oldVelocity = rb2d.velocity;
            FlipPlayer();

        }

        else
        {
            PlatformerMove();
            FlipPlayer();
        }

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        
        TakeDamage();
    }

    //public void TakeDamage()
    //{
     //   if (currentHealth == 0) return;
    //    currentHealth -= coef * (int)Time.timeAsDouble;
    //    healthBar.SetHealth(currentHealth);
    //    if(currentHealth <1e-9) SceneManager.LoadScene("DeathScene");
    //}
    
    public void TakeDamage() 
    { 
        if (isEating) return; 
        if (coef < 5 && tempMaxHealth <= 0) 
        { 
            coef++; 
            tempMaxHealth = maxHealth * coef; 
        } 
        currentHealth -= coef; 
        if(currentHealth <= 0) SceneManager.LoadScene(DeathScene); 
        if (coef < 5) tempMaxHealth -= coef; 
        healthBar.SetHealth(currentHealth); 
    } 

    public void OnTriggerEnter2D(Collider2D collision) 
    { 
        if (collision.gameObject.tag == "Food") 
        { 
            eatingFood.Play();
            isEating = true; 
            Eating(); 
            isEating = false; 
        } 
        
        else if (collision.gameObject.tag == "Teleport")
        {
            teleportation.Play();
        }
    } 

    public void Eating() 
    { 
        if (currentHealth + satiety > maxHealth) 
        { 
            currentHealth = maxHealth; 
            healthBar.SetHealth(maxHealth); 
        } 
        else 
        { 
            currentHealth += satiety; 
            healthBar.SetHealth(currentHealth); 
        } 
    }


    

    void PlatformerMove()
    {
        rb2d.velocity = new Vector2(moveSpeed * xInput, rb2d.velocity.y);
        oldVelocity = rb2d.velocity;
    }

    void FlipPlayer()
    {
        if (rb2d.velocity.x < 0f)
        {
            sp.flipX = true;
        }

        else if(rb2d.velocity.x > 0f)
        {
            sp.flipX = false;
        }
    }

    void Jump()
    {
        jump.Play();
        rb2d.velocity = Vector2.up * jumpForce;
    }
}
