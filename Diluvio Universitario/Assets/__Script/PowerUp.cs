﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.name.Equals("Player")){
			Destroy (gameObject);
		}
	}
}
