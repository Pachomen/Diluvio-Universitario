using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movimiento : MonoBehaviour
{
    [Header("Set in Inspector")]
    GameObject player;
    GameObject touchReference;
    public float xSpeed;
    public float joyX;
    public float joyY;
    public float joyMaxScale;
    public int walkDirection;
    public bool onGround;
    public Vector2 jumpForce;
    public Text gt;
    Vector2 left;
    Vector2 x;
    float angulo;
    bool moving;


    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject coll = collision.gameObject;
        if (coll.tag == "Ground")
        {
            onGround = true;
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        GameObject coll = collision.gameObject;
        if (coll.tag == "Ground")
        {
            onGround = false;
        }
    }
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        touchReference= GameObject.FindGameObjectWithTag("touchReference");
        touchReference.SetActive(false);
    } 
    void Update()
    {
        if(Input.touchCount>0){
            for (int i = 0; i < Input.touchCount; i++)
            {
                touchReference.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(left.x,left.y, 1f));
                switch (Input.GetTouch(i).phase)
                {
                    case TouchPhase.Began:
                        touchReference.transform.localScale = Vector2.one;
                        left = Input.GetTouch(i).position;
                        touchReference.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.GetTouch(i).position.x, Input.GetTouch(i).position.y, 1f));
                        touchReference.SetActive(true);
                        break;
                    case TouchPhase.Moved:
                        moving = true;
                        x = Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position);
                        x-= new Vector2(touchReference.transform.position.x, touchReference.transform.position.y);
                        //x.Normalize();
                        angulo = Mathf.Atan2(x.y, x.x) * Mathf.Rad2Deg;
                        //gt.text = "angulo: " + angulo.ToString() + "°";
                        //gt.text = "Vector: " + x.ToString();
                        if ((Mathf.Abs(x.x) > Mathf.Abs(x.y)) && Mathf.Abs(x.x) < joyMaxScale )
                        { 
                        touchReference.transform.localScale = new Vector2(x.x*15, x.x*15);
                        }else if(((Mathf.Abs(x.x) < Mathf.Abs(x.y)) && Mathf.Abs(x.y) < joyMaxScale))
                        {
                        touchReference.transform.localScale = new Vector2(x.y*15, x.y*15);
                        }
                        //gt.text = "Escala" + touchReference.transform.localScale.ToString();
                        break;
                    case TouchPhase.Ended:
                        moving = false;
                        touchReference.SetActive(false);
                        break;
                }
            }
        }

        //Movimiento con JoyStick : ANGULO
        //if ((angulo > 45f && angulo < 135f) && onGround && moving)
        //{
        //    player.GetComponent<Rigidbody2D>().AddForce(jumpForce);
        //    onGround = false;
        //}
        //else if ((angulo > 135f || angulo < -135f) && moving)
        //{
        //    player.GetComponent<Rigidbody2D>().velocity = new Vector2(-xSpeed, player.GetComponent<Rigidbody2D>().velocity.y);
        //    walkDirection = -1;
        //}
        //else if ((angulo < 45f || angulo < -45f) && moving)
        //{
        //    player.GetComponent<Rigidbody2D>().velocity = new Vector2(xSpeed, player.GetComponent<Rigidbody2D>().velocity.y);
        //    walkDirection = 1;
        //}
        //if (!moving)
        //{
        //    player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, player.GetComponent<Rigidbody2D>().velocity.y);
        //}

        //Movimiento con JoyStick : MAGNITUD !NORMALIZADO
        //if ((x.y > 0.4f) && onGround && moving)
        //{
        //    player.GetComponent<Rigidbody2D>().AddForce(jumpForce);
        //    onGround = false;
        //}
        //else if ((x.x < -0.4f) && moving)
        //{
        //    player.GetComponent<Rigidbody2D>().velocity = new Vector2(-xSpeed, player.GetComponent<Rigidbody2D>().velocity.y);
        //    walkDirection = -1;
        //}
        //else if ((x.x > 0.4f) && moving)
        //{
        //    player.GetComponent<Rigidbody2D>().velocity = new Vector2(xSpeed, player.GetComponent<Rigidbody2D>().velocity.y);
        //    walkDirection = 1;
        //}
        //if (!moving)
        //{
        //    player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, player.GetComponent<Rigidbody2D>().velocity.y);
        //}

        //Movimiento con JoyStick : MAGNITUD !NO NORMALIZADO
        if ((x.y > joyY) && onGround && moving && (player.GetComponent<Rigidbody2D>().velocity.y==0))
        {
            player.GetComponent<Rigidbody2D>().AddForce(jumpForce);
            onGround = false;
        }
        else if ((x.x < -joyX) && moving)
        {
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(-xSpeed, player.GetComponent<Rigidbody2D>().velocity.y);
            walkDirection = -1;
        }
        else if ((x.x > joyX) && moving)
        {
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(xSpeed, player.GetComponent<Rigidbody2D>().velocity.y);
            walkDirection = 1;
        }
        if (!moving)
        {
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, player.GetComponent<Rigidbody2D>().velocity.y);
        }

        //Reset
        if (player.transform.position.y<-10)
        {
            player.transform.position = new Vector2(0,1);
        }
    }
}
