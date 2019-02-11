using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimiento : MonoBehaviour
{
    [Header("Set in Inspector")]
    GameObject player;
    public int walkDirection;
    public float xSpeed;
    public Vector2 dashVector;
    public bool onGround;
    public bool dash;
    public Vector2 jumpForce;
    public Vector2 velocityVector;

    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject coll = collision.gameObject;
        if (coll.tag == "Ground")
        {
            onGround = true;
            dash = true;
        }
    }
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {  
        if (Input.GetKey(KeyCode.W) && onGround){
            player.GetComponent<Rigidbody2D>().AddForce(jumpForce);
            onGround = false;
        }
        if(Input.GetKey(KeyCode.A) && dash)
        {
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(-xSpeed, player.GetComponent<Rigidbody2D>().velocity.y);
            walkDirection = -1;
        }
        if(Input.GetKey(KeyCode.D) )
        {
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(xSpeed, player.GetComponent<Rigidbody2D>().velocity.y);
            walkDirection = 1;
        }
        if(Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A)){
        player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, player.GetComponent<Rigidbody2D>().velocity.y);
        }
        if (Input.GetKeyDown(KeyCode.E) && dash && (player.GetComponent<Rigidbody2D>().velocity.x == 0)){
            dashVector.x *= walkDirection;
            player.GetComponent<Rigidbody2D>().AddForce(dashVector);
            dash = false;
        }
        velocityVector = player.GetComponent<Rigidbody2D>().velocity;
    }
}
