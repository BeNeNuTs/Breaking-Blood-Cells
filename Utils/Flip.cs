﻿using UnityEngine;
using System.Collections;

public class Flip : MonoBehaviour {

	public enum Axe { X, Y }

	public Axe axeFlip;
	float flip = 0.7f;


	// Update is called once per frame
	void Update () {
		if(transform.parent.GetComponent<Rigidbody2D> ().velocity.normalized.x < -flip){
			if(axeFlip == Axe.X){
				transform.localScale = new Vector3(-1,1,1);
			}else if(axeFlip == Axe.Y){
				transform.localScale = new Vector3(1,-1,1);
			}
		}else if (transform.parent.GetComponent<Rigidbody2D> ().velocity.normalized.x > flip){
			transform.localScale = new Vector3(1,1,1);
		}

	}
}
