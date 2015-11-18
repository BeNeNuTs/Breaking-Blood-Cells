using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MacrophageMovement : AgentMovement {

	MacrophageAttack agentAttack;
	GameObject LTAux;

	void Start(){
		agentAttack = GetComponent<MacrophageAttack>();
		LTAux = GameObject.FindGameObjectWithTag("LTAux");
	}

	protected override void Update () {
		base.Update();

		if(agent.state == MacrophageAgent.BRING_RESIDUS){
			BringResidus();
		}
	}

	public void GoToAntibody(Vector3 antibody_position){
		if(agent.state != Agent.WIGGLE){
			return;
		}

		Vector3 diff = antibody_position - transform.position;
		float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
		
		if(Vector3.Distance(antibody_position, transform.position) < stoppingDistance * 2){
			agent.state = Agent.GOTOENEMY;
			return;
		}

		transform.rotation = Quaternion.Euler(0f, 0f, rot_z);
		agentRigidbody.velocity = new Vector2(transform.right.x, transform.right.y) * speed * Time.deltaTime;
	}

	void BringResidus(){
		if(LTAux == null){
			agentAttack.RemoveResidus();
			agent.state = Agent.WIGGLE;
			return;
		}

		Vector3 diff = LTAux.transform.position - transform.position;
		float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;

		if(Vector3.Distance(LTAux.transform.position, transform.position) < stoppingDistance){
			//DONNER RESIDUS
			GiveResidus();

			transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 180f);
			agentRigidbody.velocity = new Vector2(transform.right.x, transform.right.y) * speed * Time.deltaTime;
			agent.state = Agent.WIGGLE;
			return;
		}

		transform.rotation = Quaternion.Euler(0f, 0f, rot_z);
		agentRigidbody.velocity = new Vector2(transform.right.x, transform.right.y) * speed * Time.deltaTime;
	}

	void GiveResidus(){
		if(LTAux.GetComponent<LTAuxMovement>().TakeResidus(agentAttack.typeResidus)){
			MacrophageAttack.residuesDone = true;
			agentAttack.RemoveResidus();
		}
	}

}
