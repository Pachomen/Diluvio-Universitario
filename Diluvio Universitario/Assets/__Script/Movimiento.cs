using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movimiento : MonoBehaviour
{
    [Header("Set in Inspector")]
    GameObject player;
    GameObject touchReference;
    //Velocidad de movimiento
    public float xSpeed;
    //Distancia minima de joystick para realizar una accion
    public float joyX;
    public float joyY;
    //Tamaño maximo de el joystick
    public float joyMaxScale;
    //Toca el suelo
    public bool onGround;
    //Potencia del salto
    public Vector2 jumpForce;
    public Text gt;
    //Lugar donde se toca a la pantalla por primera vez en un toque
    Vector2 left;
    //Vector entre el primer toque y la posicion actual del dedo
    Vector2 x;
    float angulo;
    bool moving;

    //Reset del salto
    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject coll = collision.gameObject;
        if (coll.tag == "Ground")
        {
            onGround = true;
        }
    }
    //Salto hecho
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
                //Reinicio de la posicion de la marca del primer toque a la posicion actual en la pantalla
                touchReference.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(left.x,left.y, 1f));
                switch (Input.GetTouch(i).phase)
                {
                    case TouchPhase.Began:
                        //Reinicio del tamaño de la marca
                        touchReference.transform.localScale = Vector2.one;
                        //Se guarda la posicion del primer toque
                        left = Input.GetTouch(i).position;
                        //Se posiciona la marca del primer toque y se activa
                        touchReference.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.GetTouch(i).position.x, Input.GetTouch(i).position.y, 1f));
                        touchReference.SetActive(true);
                        break;

                    case TouchPhase.Moved:
                        //Se esta moviendo
                        moving = true;
                        //Se guarda la posicion del movimiento en el toque 
                        x = Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position);
                        //Se calcula el vector desde el centro de la marca hasta la posicion del toque actual para medir el vector resultante
                        x-= new Vector2(touchReference.transform.position.x, touchReference.transform.position.y);

                        //Normalizacion del vector X
                        //x.Normalize();

                        //Calculo del angulo del vector X
                        //angulo = Mathf.Atan2(x.y, x.x) * Mathf.Rad2Deg;

                        //Muestra el angulo del vector
                        //gt.text = "angulo: " + angulo.ToString() + "°";
                        //gt.text = "Vector: " + x.ToString();

                        //Cambio del tamaño del circulo que marca el movimiento
                        if ((Mathf.Abs(x.x) > Mathf.Abs(x.y)) && Mathf.Abs(x.x) < joyMaxScale )
                        { 
                        touchReference.transform.localScale = new Vector2(x.x*15, x.x*15);
                        }else if(((Mathf.Abs(x.x) < Mathf.Abs(x.y)) && Mathf.Abs(x.y) < joyMaxScale))
                        {
                        touchReference.transform.localScale = new Vector2(x.y*15, x.y*15);
                        }
                        //Muestra la escala del circulo que marca el movimiento !Falta cambiar la textura del circulo para mejor visual
                        //gt.text = "Escala" + touchReference.transform.localScale.ToString();
                        break;
                    case TouchPhase.Ended:
                        //Termina el comando de movimiento
                        moving = false;
                        touchReference.SetActive(false);
                        break;
                }
            }
        }

        //Movimiento con JoyStick : ANGULO  !!No es tan confiable !Se realiza la activacion el el angulo, falta agregar una condicion de magnitud minima del vector para eliminar falsos activos.
        //if ((angulo > 45f && angulo < 135f) && onGround && moving)
        //{
        //    player.GetComponent<Rigidbody2D>().AddForce(jumpForce);
        //    onGround = false;
        //}
        //else if ((angulo > 135f || angulo < -135f) && moving)
        //{
        //    player.GetComponent<Rigidbody2D>().velocity = new Vector2(-xSpeed, player.GetComponent<Rigidbody2D>().velocity.y);
        //}
        //else if ((angulo < 45f || angulo < -45f) && moving)
        //{
        //    player.GetComponent<Rigidbody2D>().velocity = new Vector2(xSpeed, player.GetComponent<Rigidbody2D>().velocity.y);
        //}
        //if (!moving)
        //{
        //    player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, player.GetComponent<Rigidbody2D>().velocity.y);
        //}

        //Movimiento con JoyStick : MAGNITUD !NORMALIZADO !Funciona de la misma manera que el que se encuentra abajo, solo cambia los valores de activacion de la accion ya que se encuentra normalizado el vector
        //if ((x.y > 0.4f) && onGround && moving)
        //{
        //    player.GetComponent<Rigidbody2D>().AddForce(jumpForce);
        //    onGround = false;
        //}
        //else if ((x.x < -0.4f) && moving)
        //{
        //    player.GetComponent<Rigidbody2D>().velocity = new Vector2(-xSpeed, player.GetComponent<Rigidbody2D>().velocity.y;
        //}
        //else if ((x.x > 0.4f) && moving)
        //{
        //    player.GetComponent<Rigidbody2D>().velocity = new Vector2(xSpeed, player.GetComponent<Rigidbody2D>().velocity.y);
        //}
        //if (!moving)
        //{
        //    player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, player.GetComponent<Rigidbody2D>().velocity.y);
        //}

        //Movimiento con JoyStick : MAGNITUD !NO NORMALIZADO
        //Salto !Comprueba el estado del vector "X" en Y, si se esta moviendo y la velocidad en Y del jugador es 0
        if ((x.y > joyY) && onGround && moving && (player.GetComponent<Rigidbody2D>().velocity.y==0))
        {
            player.GetComponent<Rigidbody2D>().AddForce(jumpForce);
            onGround = false;
        }
        //Movimiento izquierda
        else if ((x.x < -joyX) && moving)
        {
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(-xSpeed, player.GetComponent<Rigidbody2D>().velocity.y);
        }
        //Movimiento derecha
        else if ((x.x > joyX) && moving)
        {
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(xSpeed, player.GetComponent<Rigidbody2D>().velocity.y);
        }
        //Calcelar movimiento horizontal si no hay entrada
        if (!moving)
        {
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, player.GetComponent<Rigidbody2D>().velocity.y);
        }
        //Reset, si se cae el jugador lo regresa a la plataforma en (0,1)
        if (player.transform.position.y<-10)
        {
            player.transform.position = new Vector2(0,1);
        }
    }
}
