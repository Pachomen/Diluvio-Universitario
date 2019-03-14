using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Munición2 : MonoBehaviour {
	
	public float speed = 10f;
	Rigidbody2D cuerpo;
	void Awake () {
		cuerpo = GetComponent<Rigidbody2D>();
	}
	void Start () {

	}
	void Update () {
		if (transform.rotation.eulerAngles.y != 180f && transform.rotation.eulerAngles.z.Equals(25f)) {
			cuerpo.velocity = (new Vector2(1f,1f) * speed);
		}
		if (transform.rotation.eulerAngles.y != 180f && transform.rotation.eulerAngles.z.Equals(335f)) {
			cuerpo.velocity = (new Vector2(1f,-1f) * speed);
		}
		if (transform.rotation.eulerAngles.y.Equals (180f) && transform.rotation.eulerAngles.z.Equals(25f)) {
			cuerpo.velocity = (new Vector2(-1f,1f) * speed);
		}
		if (transform.rotation.eulerAngles.y.Equals (180f) && transform.rotation.eulerAngles.z.Equals(335f)) {
			cuerpo.velocity = (new Vector2(-1f,-1f) * speed);
		}

	}
}
