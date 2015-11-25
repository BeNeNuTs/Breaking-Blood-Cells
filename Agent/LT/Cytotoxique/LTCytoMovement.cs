using UnityEngine;
using System.Collections;

public class LTCytoMovement : AgentMovement {

	protected override void OnTriggerEnter2D(Collider2D other){
		if(other.name.Contains("Bacteria")){
			base.OnTriggerEnter2D(other);
		}
	}
}
