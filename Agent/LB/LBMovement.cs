using UnityEngine;
using System.Collections;

/// <summary>
/// La classe LBMovement hérite de la classe AgentMovement
/// et permet de redéfinir les méthodes de mouvements pour les lymphocytes B.
/// </summary>
public class LBMovement : AgentMovement {

	LBAttack agentAttack;

	void Start(){
		agentAttack = GetComponent<LBAttack>();
	}

	/// <summary>
	/// Vérifie ce qui entre dans le percepts de l'agent.
	/// </summary>
	/// <param name="other">Other.</param>
	protected override void OnTriggerEnter2D(Collider2D other){
		if(other.name.Contains("Virus")){
			base.OnTriggerEnter2D(other);
		}
	}

	/// <summary>
	/// Permet de faire avancer l'agent.
	/// </summary>
	protected override void Wiggle(){
		if(targets.Count > 0 && agentAttack.CheckTimer()){
			agent.state = Agent.ATTACK;
			return;
		}

		agentRigidbody.rotation += Random.Range(-wiggle,wiggle);
		agentRigidbody.velocity = new Vector2(transform.right.x, transform.right.y) * speed * Time.deltaTime;
	}
}
