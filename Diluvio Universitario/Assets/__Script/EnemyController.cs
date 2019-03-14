using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	Rigidbody2D cuerpo; 
	public float speed = 10;
	int x;
	Vector2 direccion;
	Vector2 posicionInicial;
	float lejania;
	bool siguiendo=false;
	bool ignorar=false;

	void Start () {

		cuerpo = GetComponent<Rigidbody2D> ();
		posicionInicial = transform.position;
		x = (int)Mathf.Round (new System.Random ().Next(1) );

		if (x == 0) {
			direccion = Vector2.right;
			cuerpo.velocity = direccion * speed;
		} else {
			direccion = Vector2.left;
			cuerpo.velocity = direccion * speed;
		}

	}


	void Update () {

		lejania = posicionInicial.x - transform.position.x;
		if (Mathf.Abs (lejania) > 20){
			cuerpo.velocity = new Vector2 (lejania/Mathf.Abs(lejania), 0f)*speed;
		}
		if (!siguiendo) {
			patrulla ();
		}
		Debug.Log (lejania);
	}

	void patrulla(){
		if (cuerpo.velocity == Vector2.zero) {
			cuerpo.velocity = -direccion * speed;
			direccion = -direccion;

		}
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.name.Equals("Player")){
			siguiendo = true;
		}
	}

	void OnTriggerStay2D(Collider2D col){
		if (col.gameObject.name.Equals("Player") && !ignorar){
			float enemigo = col.gameObject.transform.position.x - transform.position.x;
			cuerpo.velocity = new Vector2 (enemigo/Mathf.Abs(enemigo), 0f)*speed;
		}
	}

	void OnTriggerExit2D(Collider2D col){
		if (col.gameObject.name.Equals("Player")){
			siguiendo = false;
		}
	}

}
