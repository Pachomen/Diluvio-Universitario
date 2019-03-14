using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Munición5Pruebas : MonoBehaviour {

	public float speed = 10f;
	public float angularspd = 5f;
	float tiempoRecibido;
	bool sinCompletar = true;
	Rigidbody2D cuerpo;
	public GameObject personaje;

	void Awake () {
		cuerpo = GetComponent<Rigidbody2D>();
	}

	void Start () {

	}


	void FixedUpdate () {

		if (personaje.transform.rotation.eulerAngles.y != 180f) {
			cuerpo.velocity = (Vector2.right * speed);
		}
		if (personaje.transform.rotation.eulerAngles.y.Equals (180f)) {
			cuerpo.velocity = (Vector2.left * speed);
		}
		if (sinCompletar) {
			Invoke ("rotacion", tiempoRecibido);
		}
		StartCoroutine (Rotar());
		StartCoroutine (DeVuelta());

	}

	void GetTiempo (float tiempoD) {
		tiempoRecibido = tiempoD;
	}

	IEnumerator DeVuelta () {
		yield return new WaitForSeconds (tiempoRecibido*1.5f);

		if (cuerpo.velocity == (Vector2.left * speed)) {
			cuerpo.velocity = (Vector2.right * speed);
		}
		if (cuerpo.velocity == (Vector2.right * speed)) {
			cuerpo.velocity = (Vector2.left * speed);
		}
	}

	IEnumerator Rotar () {
		yield return new WaitForSeconds (tiempoRecibido);
		sinCompletar = false;
	}

	void rotacion(){

		cuerpo.velocity = Vector2.zero;
		transform.Rotate (new Vector3 (0, 0, angularspd));

	}

}
