using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Munición : MonoBehaviour {

	public float speed = 10f;
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
}
