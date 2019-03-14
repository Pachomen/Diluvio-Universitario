using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Munición4 : MonoBehaviour {

	public float speed = 10f;
	public float angularspd = 20f;
	Rigidbody2D cuerpo;

	void Awake () {
		cuerpo = GetComponent<Rigidbody2D>();
	}

	void Start () {

	}

	void Update () {
		
		if (transform.rotation.eulerAngles.y != 180f) {
			cuerpo.velocity = (Vector2.right * speed);
		}
		if (transform.rotation.eulerAngles.y.Equals (180f)) {
			cuerpo.velocity = (Vector2.left * speed);
		}

	}

	void FixedUpdate () {

		transform.Rotate (new Vector3 (0, 0, angularspd));

	}
}
