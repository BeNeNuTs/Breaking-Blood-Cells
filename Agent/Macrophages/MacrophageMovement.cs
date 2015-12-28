using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// La classe MacrophageMovement hérite de la classe AgentMovement
/// et permet de redéfinir les méthodes de mouvements des macrophages.
/// </summary>
public class MacrophageMovement : AgentMovement {

	MacrophageAttack agentAttack;
	List<GameObject> LTAux;

	void Start(){
		agentAttack = GetComponent<MacrophageAttack>();
	}

	/// <summary>
	/// Vérifie l'état de l'agent.
	/// </summary>
	protected override void Update () {
		base.Update();

		if(agent.state == MacrophageAgent.BRING_RESIDUS){
			BringResidus();
		}
	}

	/// <summary>
	/// Permet à l'agent d'aller vers les anticorps.
	/// </summary>
	/// <param name="antibody_position">Antibody_position.</param>
	public void GoToAntibody(Vector3 antibody_position){
		if(agent.state != Agent.WIGGLE){
			return;
		}

		Vector3 diff = antibody_position - transform.position;
		
		if(Vector3.Distance(antibody_position, transform.position) < stoppingDistance * 2){

			float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.Euler(0f, 0f, rot_z);
			agentRigidbody.velocity = new Vector2(transform.right.x, transform.right.y) * speed * Time.deltaTime;

			agent.state = Agent.GOTOENEMY;
			return;
		}


	}

	/// <summary>
	/// Permet de rapporter les résidus aux lymphocytes T Auxiliaire.
	/// </summary>
	void BringResidus(){

		GameObject[] LT = GameObject.FindGameObjectsWithTag("LTAux");
		LTAux = new List<GameObject>();
		foreach(GameObject GO in LT){
			LTAux.Add(GO);
		}

		if(LTAux == null){
			agentAttack.RemoveResidus();
			agent.state = Agent.WIGGLE;
			return;
		}

		GameObject closest = GetClosestTarget(LTAux);

		if(closest == null){
			agentAttack.RemoveResidus();
			agent.state = Agent.WIGGLE;
			return;
		}

		Vector3 diff = closest.transform.position - transform.position;
		float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;

		if(Vector3.Distance(closest.transform.position, transform.position) < stoppingDistance){
			//DONNER RESIDUS
			GiveResidus(closest);

			transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 180f);
			agentRigidbody.velocity = new Vector2(transform.right.x, transform.right.y) * speed * Time.deltaTime;
			agent.state = Agent.WIGGLE;
			return;
		}

		transform.rotation = Quaternion.Euler(0f, 0f, rot_z);
		agentRigidbody.velocity = new Vector2(transform.right.x, transform.right.y) * speed * Time.deltaTime;
	}

	/// <summary>
	/// Permet de donner les résidus à un lymphocyte T Auxiliaire.
	/// </summary>
	/// <param name="LT">L.</param>
	void GiveResidus(GameObject LT){
		if(LT.GetComponent<LTAuxMovement>().TakeResidus(agentAttack.typeResidus)){
			MacrophageAttack.residuesDone = true;
			agentAttack.RemoveResidus();
		}
	}

}
