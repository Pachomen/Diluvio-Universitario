using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoNaranja : MonoBehaviour
{
    [Header("Set in Inspector")]
    public GameObject player;
    public float acceleration;
    public float jump;
    public float maxSpeed;
    bool onGround;
    public Vector2 actualSpeed;

    private Rigidbody2D playerRigidbody;

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
        Vector2 force = Vector2.zero;
        if (Input.GetKey(KeyCode.Space) && onGround)
        {
            force.y = jump;
            playerRigidbody.AddForce(force);
            onGround = false;
        }
        if (Input.anyKey)
        {
            //TODO: Desactivar friccion mientras esta en movimiento, volver a activar cuando se deja de presionar (Antes de agregar la fuerza)
            if (Input.GetKey(KeyCode.A))
            {
                if (actualSpeed.x > -maxSpeed)
                {
                    force.x = -acceleration;
                }
            }
            if (Input.GetKey(KeyCode.D))
            {
                if (actualSpeed.x < maxSpeed)
                {
                    force.x = acceleration;
                }
            }
            playerRigidbody.AddForce(force);

        }
        actualSpeed = playerRigidbody.GetComponent<Rigidbody2D>().velocity;
    }


}
