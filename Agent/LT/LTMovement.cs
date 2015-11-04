using UnityEngine;
using System.Collections;

public class LTMovement : AgentMovement {
	
	public Type.TypeLT typeLT;
	public bool residus;
	public Type.TypeResidus typeResidus;

	public float timeToAnalize = 3f;

	GameObject residusGO;
	float time;

	void Start(){
		typeLT = Type.TypeLT.NONE;
		typeResidus = Type.TypeResidus.NONE;
		residus = false;

		time = 0f;
	}

	protected override void Update () {
		if(agent.state == Agent.WIGGLE){
			Wiggle();
		}else if(agent.state == Agent.GOTOENEMY){
			GoToEnemy();
		}else if(agent.state == LTAuxAgent.ANALYZE){
			Analize();
		}
	}

	protected override void Wiggle(){
		if(targets.Count > 0 && residus && typeResidus == Type.TypeResidus.BACTERIA){
			agent.state = Agent.GOTOENEMY;
			return;
		}else if(residus && typeResidus == Type.TypeResidus.VIRUS){
			//Bring info to LB
			return;
		}

		// Faire avancer l'agent
		agentRigidbody.rotation += Random.Range(-wiggle,wiggle);
		agentRigidbody.velocity = new Vector2(transform.right.x, transform.right.y) * speed * Time.deltaTime;
	}

	void Analize(){
		agentRigidbody.velocity = Vector2.zero;

		//AJOUT D'ANIM D'ANALISE

		time += Time.deltaTime;

		if(time > timeToAnalize){
			if(typeResidus == Type.TypeResidus.BACTERIA){
				typeLT = Type.TypeLT.CYTOTOXIQUE;
			}else if(typeResidus == Type.TypeResidus.VIRUS){
				typeLT = Type.TypeLT.AUXILIAIRE;
			}

			time = 0f;
			agent.state = Agent.WIGGLE;
		}
	}

	public bool TakeResidus(Type.TypeResidus _typeResidus){
		if(residus || _typeResidus == Type.TypeResidus.NONE){
			return false;
		}

		residus = true;
		typeResidus = _typeResidus;

		agent.state = LTAuxAgent.ANALYZE;

		return true;
	}
}
