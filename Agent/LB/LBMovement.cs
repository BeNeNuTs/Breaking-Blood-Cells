using UnityEngine;
using System.Collections;

public class LBMovement : AgentMovement {

	LBAttack agentAttack;

	void Start(){
		agentAttack = GetComponent<LBAttack>();
	}

	protected override void OnTriggerEnter2D(Collider2D other){
		if(other.name.Contains("Virus")){
			base.OnTriggerEnter2D(other);
		}
	}

	protected override void Wiggle(){
		if(targets.Count > 0 && agentAttack.CheckTimer()){
			agent.state = Agent.ATTACK;
			return;
		}

		agentRigidbody.rotation += Random.Range(-wiggle,wiggle);
		agentRigidbody.velocity = new Vector2(transform.right.x, transform.right.y) * speed * Time.deltaTime;
	}
}
