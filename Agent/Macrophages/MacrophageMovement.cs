using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MacrophageMovement : AgentMovement {

	[HideInInspector]
	public List<GameObject> LT;

	MacrophageAttack agentAttack;

	void Start(){
		agentAttack = GetComponent<MacrophageAttack>();
	}

	protected override void OnTriggerEnter2D(Collider2D other){
		base.OnTriggerEnter2D(other);
		
		if(other.CompareTag(gameObject.tag) && other.name.Contains("LT") && other.GetType() == typeof(BoxCollider2D)){
			if(!LT.Contains(other.gameObject)){
				LT.Add(other.gameObject);
			}
		}
	}
	
	protected override void OnTriggerExit2D(Collider2D other){
		base.OnTriggerExit2D(other);
		
		if(other.CompareTag(gameObject.tag) && other.name.Contains("LT") && other.GetType() == typeof(BoxCollider2D)){
			LT.Remove(other.gameObject);
		}
	}

	protected override void Wiggle(){
		base.Wiggle();

		if(agentAttack.residus){
			UpdateList(LT);
			if(LT.Count > 0){
				//DONNER RESIDUS
				agentAttack.RemoveResidus();
			}
		}
	}
}
