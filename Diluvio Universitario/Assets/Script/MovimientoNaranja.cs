using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoNaranja : MonoBehaviour
{
    [Header("Set in Inspector")]
    public GameObject player;
    public float acceleration;
    public float jump;
    public Vector2 actualSpeed;
    public float maxSpeed;
    bool onGround;
    private Rigidbody2D playerRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = player.GetComponent<Rigidbody2D>();

    }
    void OnCollisionStay2D(Collision2D collision)
    {
        GameObject coll = collision.gameObject;
        if (coll.tag == "Ground")
        {
            onGround = true;
        }
    }
    void Update()
    {
        playerRigidbody = player.GetComponent<Rigidbody2D>();
        Vector2 pos = Vector2.zero;
        if (Input.GetKey(KeyCode.Space) && onGround)
        {
            pos.y = jump;
            playerRigidbody.AddForce(pos);
            onGround = false;
        }
        if (Input.GetKey(KeyCode.A))
        {
            if (actualSpeed.x > -maxSpeed)
            {
                pos.x = -acceleration;
                playerRigidbody.AddForce(pos);
            }
        }
        if (Input.GetKey(KeyCode.D))
        {
            if (actualSpeed.x < maxSpeed)
            {
                pos.x = acceleration;
                playerRigidbody.AddForce(pos);
            }
        }
        actualSpeed = playerRigidbody.GetComponent<Rigidbody2D>().velocity;
    }
}
